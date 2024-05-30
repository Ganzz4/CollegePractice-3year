using System.Windows;
using System.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using Lab11.Forms;

namespace Lab11
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static string selectedTable = "";
        private string login;
        private string role;
        public MainWindow(string userRole, string userLogin)
        {
            InitializeComponent();
            login = userLogin;
            role = userRole;
        }


        private void addInfo_Click(object sender, RoutedEventArgs e)
        {
            string nameOfTable = "";
            if (selectedTable.Length >= 4)
            {
                nameOfTable = selectedTable.Substring(0, selectedTable.Length - 4);
            }
            AddNewInfoForm addNewInfoForm = new AddNewInfoForm(nameOfTable);
            addNewInfoForm.ShowDialog();

            if (selectedTable != "")
                DatabaseActions.CRUDInDB('R', $"SELECT * FROM {selectedTable}", dataGrid);
        }

        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = e.OriginalSource as MenuItem;
            if (menuItem != null)
            {
                selectedTable = menuItem.Name;
                if (FilterPanel.Visibility == Visibility.Visible) FilterPanel.Visibility = Visibility.Collapsed;
                DatabaseActions.CRUDInDB('R', $"SELECT * FROM {selectedTable}", dataGrid);
            }
        }


        private void dataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string header = e.Column.Header.ToString();

            if (header.Contains("Дата поставки")) e.Column.ClipboardContentBinding.StringFormat = "yyyy-MM-dd";
            else if (header.Contains("Дата")) e.Column.ClipboardContentBinding.StringFormat = "yyyy-MM-dd HH:mm:ss";
        }

        private void Image_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                TextBox textBoxFind = (TextBox)findMenuItem.Template.FindName("textBoxFind", findMenuItem);
                if (textBoxFind != null && selectedTable != "")
                    DatabaseActions.SearchInDB(dataGrid, textBoxFind.Text, selectedTable);
                else throw new Exception();
            }
            catch
            {
                MessageBox.Show("Выберите таблицу или критерий для поиска!", "Ошибка!");
            }
        }


        private void deleteInfo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dataGrid.SelectedIndex == -1)
                {
                    MessageBox.Show("Выберите строку в DataGrid.", "Ошибка!");
                    return;
                }

                MessageBoxResult mb = MessageBox.Show("Вы уверены, что хотите удалить данные?","Подтверждение", MessageBoxButton.YesNo,MessageBoxImage.Question);
                if (mb == MessageBoxResult.No) return;

                string nameOfTable = "";
                if (selectedTable.Length >= 4)
                {
                    nameOfTable = selectedTable.Substring(0, selectedTable.Length - 4);
                }
                switch (nameOfTable)
                {
                    case "Countries":
                        DatabaseActions.CRUDInDB('D', $"EXEC DeleteCountry @country_code = '{Helper.returnCellText(dataGrid, dataGrid.Columns.Count - 1)}'");
                        break;
                    case "StoreAddress":
                        DatabaseActions.CRUDInDB('D', $"EXEC DeleteStoreAddress @store_address_code = '{Helper.returnCellText(dataGrid, dataGrid.Columns.Count - 1)}'");
                        break;
                    case "VAT":
                        DatabaseActions.CRUDInDB('D', $"EXEC DeleteVAT @vat_code = '{Helper.returnCellText(dataGrid, dataGrid.Columns.Count - 1)}'");
                        break;
                    case "ProductType":
                        DatabaseActions.CRUDInDB('D', $"EXEC DeleteProductType @product_type_code = '{Helper.returnCellText(dataGrid, dataGrid.Columns.Count - 1)}'");
                        break;
                    case "ProductSample":
                        DatabaseActions.CRUDInDB('D', $"EXEC DeleteProductSample @sample_code = '{Helper.returnCellText(dataGrid, dataGrid.Columns.Count - 1)}'");
                        break;
                    case "ProviderAddress":
                        DatabaseActions.CRUDInDB('D', $"EXEC DeleteProviderAddress @provider_address_code = '{Helper.returnCellText(dataGrid, dataGrid.Columns.Count - 1)}'");
                        break;
                    case "ProviderName":
                        DatabaseActions.CRUDInDB('D', $"EXEC DeleteProviderName @provider_code = '{Helper.returnCellText(dataGrid, dataGrid.Columns.Count - 1)}'");
                        break;
                    case "TaxInvoice":
                         ComboBox cb = new ComboBox();
                         DatabaseActions.FillComboBox(cb, "ProductType", "product_type_code", "product_type_name");
                         DatabaseActions.CRUDInDB('D', $"EXEC DeleteTaxInvoice @tax_invoice_number = '{Helper.returnCellText(dataGrid, 1)}' , @product_type_code = '{InputValidators.returnValueFromComboBox(cb, Helper.returnCellText(dataGrid, 4))}'");
                        break;
                    case "ProductProvider":
                        DatabaseActions.CRUDInDB('D', $"EXEC DeleteProductProvider @fiscal_code = '{Helper.returnCellText(dataGrid, 2)}'");
                        break;
                    case "Cheque":
                        DatabaseActions.CRUDInDB('D', $"EXEC DeleteCheque @number_in_order = '{Helper.returnCellText(dataGrid, dataGrid.Columns.Count - 1)}'");
                        break;
                        
                }
                DatabaseActions.CRUDInDB('R', $"SELECT * FROM {selectedTable}", dataGrid);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Возникла ошибка!" + ex.Message);
            }

        }

        private void redact_Click(object sender, RoutedEventArgs e)
        {
            if (selectedTable != "")
            {
                UpdateInfoForm updateInfoForm = new UpdateInfoForm(dataGrid.ItemsSource, selectedTable);
                updateInfoForm.ShowDialog();
                DatabaseActions.CRUDInDB('R', $"SELECT * FROM {selectedTable}", dataGrid);
            }
            else MessageBox.Show("Сначала выберите таблицу!");
            
        }

        private void filterDataOk_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Dictionary<string, object> filterParams = new Dictionary<string, object>();
                string nameOfTable = selectedTable.Length >= 4 ? selectedTable.Substring(0, selectedTable.Length - 4) : "";

                // Универсальный сбор фильтров
                switch (nameOfTable)
                {
                    case "TaxInvoice":
                        if (textBox1.Text != "")
                            filterParams.Add(dataGrid.Columns[1].Header.ToString(), textBox1.Text);
                        if (textBox2.Text != "")
                            filterParams.Add(dataGrid.Columns[2].Header.ToString(), textBox2.Text + "%");
                        if (comboBoxFilter1.Text != "")
                            filterParams.Add(dataGrid.Columns[3].Header.ToString(), comboBoxFilter1.Text);
                        if (comboBoxFilter2.Text != "")
                            filterParams.Add(dataGrid.Columns[4].Header.ToString(), comboBoxFilter2.Text);

                        ApplyFilters(dataGrid, selectedTable, filterParams, dataGrid.Columns[dataGrid.Columns.Count - 1].Header.ToString());
                        break;

                    case "ProductProvider":
                        if (comboBoxFilter1.Text != "")
                            filterParams.Add(dataGrid.Columns[3].Header.ToString(), comboBoxFilter1.Text);
                        if (comboBoxFilter2.Text != "")
                            filterParams.Add(dataGrid.Columns[4].Header.ToString(), comboBoxFilter2.Text);

                        ApplyFilters(dataGrid, selectedTable, filterParams, null);
                        break;

                    case "Cheque":
                        if (comboBoxFilter1.Text != "")
                            filterParams.Add(dataGrid.Columns[2].Header.ToString(), comboBoxFilter1.Text);
                        if (comboBoxFilter2.Text != "")
                            filterParams.Add(dataGrid.Columns[3].Header.ToString(), comboBoxFilter2.Text);
                        if (comboBoxFilter3.Text != "")
                            filterParams.Add(dataGrid.Columns[5].Header.ToString(), comboBoxFilter3.Text);
                        if (textBox1.Text != "")
                            filterParams.Add("DateFilter", $"CONVERT(date, [{dataGrid.Columns[1].Header.ToString()}]) LIKE '{textBox1.Text}%'");

                        ApplyFilters(dataGrid, selectedTable, filterParams, dataGrid.Columns[dataGrid.Columns.Count - 2].Header.ToString());
                        break;

                    case "InfoForClient":
                        if (comboBoxFilter1.Text != "")
                            filterParams.Add(dataGrid.Columns[1].Header.ToString(), comboBoxFilter1.Text);
                        if (comboBoxFilter2.Text != "")
                            filterParams.Add(dataGrid.Columns[2].Header.ToString(), comboBoxFilter2.Text);

                        ApplyFilters(dataGrid, selectedTable, filterParams, dataGrid.Columns[dataGrid.Columns.Count - 1].Header.ToString());
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Возникла ошибка!" + ex.Message);
            }
        }

        // Универсальная функция для применения фильтров
        private void ApplyFilters(DataGrid dataGrid, string selectedTable, Dictionary<string, object> filterParams, string priceColumn)
        {
            // Условия для фильтрации по цене
            string priceFilter = "";
            if (!string.IsNullOrEmpty(priceColumn))
            {
                if (textBoxMin.Text != "" && textBoxMax.Text != "")
                {
                    priceFilter = $"[{priceColumn}] >= {textBoxMin.Text} AND [{priceColumn}] <= {textBoxMax.Text}";
                }
                else if (textBoxMin.Text != "" && textBoxMax.Text == "")
                {
                    priceFilter = $"[{priceColumn}] >= {textBoxMin.Text}";
                }
                else if (textBoxMax.Text != "" && textBoxMin.Text == "")
                {
                    priceFilter = $"[{priceColumn}] <= {textBoxMax.Text}";
                }
            }

            // Универсальный вызов фильтрации
            DatabaseActions.FilterData(dataGrid, selectedTable, filterParams, priceFilter);
        }


        private void menuFilter_Click(object sender, RoutedEventArgs e)
        {
            if(dataGrid.ItemsSource == null)
            {
                MessageBox.Show("Сначала выберите таблицу!", "Ошибка!");
                return;
            }
            if (FilterPanel.Visibility == Visibility.Collapsed)
            {
                FilterPanel.Visibility = Visibility.Visible;
                fillTextBoxes();
            }else FilterPanel.Visibility = Visibility.Collapsed;
        }

        void fillTextBoxes()
        {
            Helper.visibleValuesFalse(FilterPanel, textBox1,textBox2, comboBoxFilter1, comboBoxFilter2, comboBoxFilter3, textBoxMin, textBoxMax);
            try
            {
                string nameOfTable = selectedTable.Substring(0, selectedTable.Length - 4);

                switch (nameOfTable)
                {
                    case "TaxInvoice":


                        Helper.SetTextBoxProperties(textBox1, InputValidators.onlyDigits_PreviewTextInput, 10, "Номер");
                        Helper.SetTextBoxProperties(textBox2, InputValidators.date_PreviewTextInput, 10, "Дата(yyyy-mm-dd)");
                        Helper.setHelperText(comboBoxFilter1, "Название поставщика");
                        
                        DatabaseActions.FillComboBox(comboBoxFilter1, "ProviderName", "provider_code", "provider_name");
                        Helper.setHelperText(comboBoxFilter2, "Тип изделия");
                        DatabaseActions.FillComboBox(comboBoxFilter2, "ProductType", "product_type_code", "product_type_name");

                        Helper.SetTextBoxProperties(textBoxMin, InputValidators.onlyDigits_PreviewTextInput, 10, "От");
                        Helper.SetTextBoxProperties(textBoxMax, InputValidators.onlyDigits_PreviewTextInput, 10, "До");
                        
                        Helper.visibleValuesTrue(textBox1, textBox2, comboBoxFilter1,textBoxMin,textBoxMax, FilterPanel);
                        break;
                        
                    case "ProductProvider":
                        Helper.setHelperText(comboBoxFilter1, "Страна поставщика");
                        Helper.setHelperText(comboBoxFilter2, "Город поставщика");
                        DatabaseActions.FillComboBox(comboBoxFilter1, "Countries", "country_code", "country_name");
                        DatabaseActions.FillComboBox(comboBoxFilter2, "ProviderAddress", "provider_city", "provider_city");
                        Helper.visibleValuesTrue(comboBoxFilter1, comboBoxFilter2, FilterPanel); 


                        break;
                    
                    case "Cheque":
                        Helper.SetTextBoxProperties(textBox1, InputValidators.dateTime_PreviewTextInput, 19, "Дата");
                        DatabaseActions.FillComboBox(comboBoxFilter1, "Countries", "country_code", "country_name");
                        DatabaseActions.FillComboBox(comboBoxFilter2, "StoreAddress", "store_city", "store_city");
                        DatabaseActions.FillComboBox(comboBoxFilter3, "ProductType", "product_type_code", "product_type_name");
                        Helper.setHelperText(comboBoxFilter1, "Страна");
                        Helper.setHelperText(comboBoxFilter2, "Адрес");
                        Helper.setHelperText(comboBoxFilter3, "Тип продукта");

                        Helper.SetTextBoxProperties(textBoxMin, InputValidators.onlyDigits_PreviewTextInput, 10, "От");
                        Helper.SetTextBoxProperties(textBoxMax, InputValidators.onlyDigits_PreviewTextInput, 10, "До");
                        
                        Helper.visibleValuesTrue(textBox1, comboBoxFilter1, comboBoxFilter2, comboBoxFilter3, textBoxMin,textBoxMax, FilterPanel);
                        break;

                    case "InfoForClient":
                        DatabaseActions.FillComboBox(comboBoxFilter1, "ProviderName", "provider_code", "provider_name");
                        DatabaseActions.FillComboBox(comboBoxFilter2, "ProductType", "product_type_code", "product_type_name");
                        Helper.setHelperText(comboBoxFilter1, "Название фирмы");
                        Helper.setHelperText(comboBoxFilter2, "Тип продукта");

                        Helper.SetTextBoxProperties(textBoxMin, InputValidators.onlyDigits_PreviewTextInput, 10, "От");
                        Helper.SetTextBoxProperties(textBoxMax, InputValidators.onlyDigits_PreviewTextInput, 10, "До");

                        Helper.visibleValuesTrue(comboBoxFilter1, comboBoxFilter2, textBoxMin, textBoxMax, FilterPanel);
                        break;
                    default:
                        MessageBox.Show("Для этой таблицы нет фильтров!");
                        return;
                        
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при редактировании!" + ex.Message);
            }
        }

        private void filterDataReset_Click(object sender, RoutedEventArgs e)
        {
            Helper.clearControls(textBox1,textBox2,comboBoxFilter1,comboBoxFilter2,comboBoxFilter3,textBoxMin,textBoxMax);
            DatabaseActions.CRUDInDB('R', $"SELECT * FROM {selectedTable}", dataGrid);
        }

        private void openReport_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = e.OriginalSource as MenuItem;
            if (menuItem != null)
            {
                ReportWindow reportWindow;
                switch (menuItem.Header)
                {
                    case "Поставщик":
                        reportWindow = new ReportWindow(0);
                        reportWindow.ShowDialog();
                        break;
                    case "Накладная":
                        reportWindow = new ReportWindow(1);
                        reportWindow.ShowDialog();
                        break;
                    case "Чек":
                        reportWindow = new ReportWindow(2);
                        reportWindow.ShowDialog();
                        break;
                }
            }

            
        }

        private void dataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            if(role == "user")
            {
                selectedTable = "InfoForClientView";
                foreach (var item in mainMenu.Items.OfType<MenuItem>())
                {
                    item.Visibility = Visibility.Collapsed;
                }
                menuFilter.Visibility = Visibility.Visible;
                findMenuItem.Visibility = Visibility.Visible;
                refreshInfo.Visibility = Visibility.Visible;

                DatabaseActions.CRUDInDB('R', $"SELECT * FROM {selectedTable}", dataGrid);
            }
        }

        private void adminPanel_Click(object sender, RoutedEventArgs e)
        {
            AdminPanelForm adminPanelForm = new AdminPanelForm(login);
            adminPanelForm.ShowDialog();
        }

        private void refreshInfo_Click(object sender, RoutedEventArgs e)
        {
            DatabaseActions.CRUDInDB('R', $"SELECT * FROM {selectedTable}", dataGrid);
            TextBox textBoxFind = (TextBox)findMenuItem.Template.FindName("textBoxFind", findMenuItem);
            if (textBoxFind != null && textBoxFind.Text != "" && selectedTable != "")
                textBoxFind.Clear();
        }
    }
}