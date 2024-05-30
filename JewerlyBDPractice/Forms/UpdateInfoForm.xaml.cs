using System;
using System.Collections;
using System.Data;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace Lab11
{
    /// <summary>
    /// Логика взаимодействия для UpdateInfoForm.xaml
    /// </summary>
    public partial class UpdateInfoForm : Window
    {
        private string selectedTable = "";

        public UpdateInfoForm(IEnumerable items,string selectedTable)
        {
            InitializeComponent();
            this.selectedTable = selectedTable;
            dataGrid.ItemsSource = items;
        }

        void fillTextBoxes()
        {
            Helper.visibleValuesFalse(value1, value2, value3, value4, value5,comboBox1, comboBox2, comboBox3, comboBox4);
            try
            {
                string nameOfTable = selectedTable.Substring(0, selectedTable.Length - 4);
               
                switch (nameOfTable)
                {
                    case "Countries":
                        Helper.SetTextBoxProperties(value1, InputValidators.place_PreviewTextInput, 40, "Название страны");

                        Helper.visibleValuesTrue(value1);

                        break;
                    case "StoreAddress":
                        Helper.SetTextBoxProperties(value1, InputValidators.place_PreviewTextInput, 40, "Название города");
                        value2.MaxLength = 40;

                        Helper.setHelperText(value2, "Улица");
                        Helper.visibleValuesTrue(value1, value2);
                        break;

                    case "VAT":
                        Helper.SetTextBoxProperties(value1, InputValidators.onlyDigits_PreviewTextInput, 3, "НДС");


                        Helper.visibleValuesTrue(value1);
                        break;
                    case "ProductType":
                        Helper.SetTextBoxProperties(value1, InputValidators.onlyLetters_PreviewTextInput, 15, "Тип изделия");


                        Helper.visibleValuesTrue(value1);
                        break;
                    case "ProductSample":
                        Helper.SetTextBoxProperties(value1, InputValidators.onlyDigits_PreviewTextInput, 3, "Проба('375-999')");


                        Helper.visibleValuesTrue(value1);
                        break;
                    case "ProviderName":

                        Helper.SetTextBoxProperties(value1, InputValidators.providerName_PreviewTextInput, 20, "Название поставщика");

                        Helper.visibleValuesTrue(value1);
                        break;
                    case "ProviderAddress":
                        Helper.SetTextBoxProperties(value1, InputValidators.place_PreviewTextInput, 40, "Название города");
                        value2.MaxLength = 40;
                        Helper.setHelperText(value2, "Улица");


                        Helper.visibleValuesTrue(value1, value2);
                        break;
                    case "TaxInvoice":


                        Helper.SetTextBoxProperties(value1, InputValidators.onlyDigits_PreviewTextInput, 10, "Номер");
                        Helper.SetTextBoxProperties(value2, InputValidators.date_PreviewTextInput, 10, "Дата(yyyy-mm-dd)");
                        Helper.SetTextBoxProperties(value3, InputValidators.onlyDigits_PreviewTextInput, 5, "Количество");
                        Helper.SetTextBoxProperties(value4, InputValidators.doubles_PreviewTextInput, 10, "Вес(грамм)");
                        Helper.SetTextBoxProperties(value5, InputValidators.doubles_PreviewTextInput, 10, "Цена за грамм");
                        Helper.setHelperText(comboBox1, "Название поставщика");
                        Helper.setHelperText(comboBox2, "Тип изделия");
                        Helper.setHelperText(comboBox3, "Проба");
                        Helper.setHelperText(comboBox4, "НДС");

                        DatabaseActions.FillComboBox(comboBox1, "ProviderName", "provider_code", "provider_name");
                        DatabaseActions.FillComboBox(comboBox2, "ProductType", "product_type_code", "product_type_name");
                        DatabaseActions.FillComboBox(comboBox3, "ProductSample", "sample_code", "sample_name");
                        DatabaseActions.FillComboBox(comboBox4, "VAT", "vat_code", "vat_procent");

                        Helper.visibleValuesTrue(value1, value2, value3, value4, value5
                            , comboBox1, comboBox2, comboBox3, comboBox4);
                        break;
                    case "ProductProvider":

                        Helper.SetTextBoxProperties(value1, InputValidators.onlyDigits_PreviewTextInput, 13, "Фискальный код");
                        Helper.SetTextBoxProperties(value2, InputValidators.onlyDigits_PreviewTextInput, 12, "Номер телефона");
                        Helper.SetTextBoxProperties(value3, InputValidators.onlyDigits_PreviewTextInput, 13, "Платёжный счёт");
                        Helper.setHelperText(comboBox1, "Название поставщика");
                        Helper.setHelperText(comboBox2, "Страна поставщика");
                        Helper.setHelperText(comboBox3, "Адрес поставщика");


                        DatabaseActions.FillComboBox(comboBox1, "ProviderName", "provider_code", "provider_name");
                        DatabaseActions.FillComboBox(comboBox2, "Countries", "country_code", "country_name");
                        DatabaseActions.FillComboBox(comboBox3, "ProviderAddress", "provider_address_code", "provider_city", "provider_street");

                        Helper.visibleValuesTrue(comboBox1, comboBox2, comboBox3, value1, value2, value3);
                        break;

                    case "Cheque":
                        Helper.SetTextBoxProperties(value1, InputValidators.dateTime_PreviewTextInput, 19, "Дата");
                        Helper.SetTextBoxProperties(value2, InputValidators.onlyDigits_PreviewTextInput, 10, "Вес(грамм)");
                        Helper.SetTextBoxProperties(value3, InputValidators.doubles_PreviewTextInput, 10, "Цена за грамм");
                        Helper.SetTextBoxProperties(value4, InputValidators.onlyDigits_PreviewTextInput, 3, "Скидка %");
                        Helper.setHelperText(comboBox1, "Страна");
                        Helper.setHelperText(comboBox2, "Адрес");
                        Helper.setHelperText(comboBox3, "Тип продукта");
                        Helper.setHelperText(comboBox4, "НДС");


                        DatabaseActions.FillComboBox(comboBox1, "Countries", "country_code", "country_name");
                        DatabaseActions.FillComboBox(comboBox2, "StoreAddress", "store_address_code", "store_city", "store_street");
                        DatabaseActions.FillComboBox(comboBox3, "ProductType", "product_type_code", "product_type_name");
                        DatabaseActions.FillComboBox(comboBox4, "VAT", "vat_code", "vat_procent");

                        Helper.visibleValuesTrue(value1, value2, value3, value4,
                             comboBox1, comboBox2, comboBox3, comboBox4);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при редактировании!" + ex.Message);
            }
        }

        private void updateInfo_Click(object sender, RoutedEventArgs e)
        {
            string nameOfTable = selectedTable.Substring(0, selectedTable.Length - 4);
            if(dataGrid.SelectedIndex == -1)
            {
                MessageBox.Show("Сначала выберите строку!");
                return;
            }
            if (!CheckFields(nameOfTable))
            {
                MessageBox.Show("Корректно заполните все поля!", "Ошибка!");
                return;
            }
            switch (nameOfTable)
            {
                case "Countries":
                    DatabaseActions.CRUDInDB('U', $"EXEC UpdateCountry " +
                        $"@country_code = '{Helper.returnCellText(dataGrid, dataGrid.Columns.Count - 1)}' , " +
                        $"@country_name = '{value1.Text}'");
                    break;
                case "StoreAddress":
                    DatabaseActions.CRUDInDB('U', $"EXEC UpdateStoreAddress " +
                        $"@store_address_code = '{Helper.returnCellText(dataGrid, dataGrid.Columns.Count - 1)}' , " +
                        $"@store_city = '{value1.Text}' , " +
                        $"@store_street = '{value2.Text}'");
                    break;
                case "VAT":
                    DatabaseActions.CRUDInDB('U', $"EXEC UpdateVAT " +
                        $"@vat_code = '{Helper.returnCellText(dataGrid, dataGrid.Columns.Count - 1)}' , " +
                        $"@vat_procent = '{value1.Text}'");
                    break;
                case "ProductType":
                    DatabaseActions.CRUDInDB('U', $"EXEC UpdateProductType " +
                        $"@product_type_code = '{Helper.returnCellText(dataGrid, dataGrid.Columns.Count - 1)}' , " +
                        $"@product_type_name = '{value1.Text}'");
                    break;
                case "ProductSample":
                    DatabaseActions.CRUDInDB('U', $"EXEC UpdateProductSample " +
                        $"@sample_code = '{Helper.returnCellText(dataGrid, dataGrid.Columns.Count - 1)}' , " +
                        $"@sample_name = '{value1.Text}'");
                    break;
                case "ProviderName":
                    DatabaseActions.CRUDInDB('U', $"EXEC UpdateProviderName " +
                        $"@provider_code = '{Helper.returnCellText(dataGrid, dataGrid.Columns.Count - 1)}' , " +
                        $"@provider_name = '{value1.Text}'");
                    break;
                case "ProviderAddress":
                    DatabaseActions.CRUDInDB('U', $"EXEC UpdateProviderAddress " +
                        $"@provider_address_code = '{Helper.returnCellText(dataGrid, dataGrid.Columns.Count - 1)}' , " +
                        $"@provider_city = '{value1.Text}' , " +
                        $"@provider_street = '{value2.Text}'");
                    break;
                case "TaxInvoice":
                    DatabaseActions.CRUDInDB('U', $"EXEC UpdateTaxInvoice " +
                        $"@tax_invoice_numberOLD = '{Helper.returnCellText(dataGrid, 1)}' , " +
                        $"@tax_invoice_number = '{value1.Text}' , " +
                        $"@product_type_codeOLD = '{InputValidators.returnValueFromComboBox(comboBox2, Helper.returnCellText(dataGrid, 4))}' , " +
                        $"@product_type_code = '{comboBox2.SelectedValue.ToString()}' , " +

                        $"@delivery_date = '{value2.Text}' , " +
                        $"@provider_code = '{comboBox1.SelectedValue.ToString()}' , " +
                        $"@sample_code = '{comboBox3.SelectedValue.ToString()}' , " +
                        $"@quantity = '{value3.Text}' , " +
                        $"@weight_grams = '{value4.Text}' , " +
                        $"@price_per_gramm = '{value5.Text}' , " +
                        $"@vat_code = '{comboBox4.SelectedValue.ToString()}'");
                    break;
                    
                case "ProductProvider":
                    DatabaseActions.CRUDInDB('U', $"EXEC UpdateProductProvider " +
                        $"@fiscal_codeOLD = '{Helper.returnCellText(dataGrid, 2)}' , " +
                        $"@fiscal_code = '{value1.Text}' , " +
                        $"@provider_code = '{comboBox1.SelectedValue}' , " +
                        $"@country_code = '{comboBox2.SelectedValue}' , " +
                        $"@provider_address_code = '{comboBox3.SelectedValue}' , " +
                        $"@phone_number = '{value2.Text}' , " +
                        $"@payment_account = '{value3.Text}'");
                    break;
                    
                case "Cheque":
                    DateTime dateTimeValue;
                    string sqlFormattedDateTime = "";
                    if (DateTime.TryParse(value1.Text, out dateTimeValue))
                    {
                        char t = 'T';
                        sqlFormattedDateTime = dateTimeValue.ToString($"yyyy-MM-dd{t}HH:mm:ss");
                    }
                    DatabaseActions.CRUDInDB('U', $"EXEC UpdateCheque " +
                        $"@number_in_order = '{Helper.returnCellText(dataGrid, dataGrid.Columns.Count - 1)}' , " +
                        $"@date_and_time_of_sale = '{sqlFormattedDateTime}' , " +
                        $"@country_code = '{comboBox1.SelectedValue}' , " +
                        $"@store_adress_code = '{comboBox2.SelectedValue}' , " +
                        $"@product_type_code = '{comboBox3.SelectedValue}' , " +
                        $"@weight_grams = '{value2.Text}' , " +
                        $"@price_per_gram = '{value3.Text}' , " +
                        $"@discount_percentage = '{value4.Text}' , " +
                        $"@vat_code = '{comboBox4.SelectedValue}'");
                    break;
            }
        }

        private bool CheckFields(string tableName)
        {
            switch (tableName)
            {
                case "StoreCountry":
                    return InputValidators.AreAllValuesFilled(value1);
                case "StoreAdress":
                    return InputValidators.AreAllValuesFilled(value1, value2);
                case "VAT":
                    return InputValidators.CheckIntegerValue(value1, 0, 100);
                case "ProductType":
                    return InputValidators.AreAllValuesFilled(value1);
                case "ProductSample":
                    if (!InputValidators.CheckIntegerValue(value1, 375, 999))
                    {
                        MessageBox.Show("Проба не может быть меньше 375 и больше 999");
                        return false;
                    }
                    return InputValidators.CheckIntegerValue(value1, 375, 999);

                case "ProviderName":
                    return InputValidators.AreAllValuesFilled(value1);
                case "ProviderCountry":
                    return InputValidators.AreAllValuesFilled(value1);
                case "ProviderAddress":
                    return InputValidators.AreAllValuesFilled(value1, value2);
                case "TaxInvoice":
                    if (!DateTime.TryParseExact(value2.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
                    {
                        MessageBox.Show("Введите дату в верном формате!");
                        return false;
                    }
                    return InputValidators.AreAllValuesFilled(value1, value2, value3, value4, value5, comboBox1, comboBox2, comboBox3, comboBox4) &&
                           DateTime.TryParseExact(value2.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out _) &&
                           InputValidators.CheckIntegerValue(value3, 1, int.MaxValue) &&
                           InputValidators.CheckDoubleValue(value4, 0, double.MaxValue) &&
                           InputValidators.CheckDoubleValue(value5, 0, double.MaxValue);
                case "ProductProvider":
                    if (value1.Text.Length != 13)
                    {
                        MessageBox.Show("Фискальный код состоит из 13 цифр!");
                        return false;
                    }
                    if (value2.Text.Length < 5)
                    {
                        MessageBox.Show("Номер телефона не может быть менее 5 цифр!");
                        return false;
                    }
                    if (value3.Text.Length != 13)
                    {
                        MessageBox.Show("Платёжный счёт состоит из 13 цифр!");
                        return false;
                    }
                    return InputValidators.AreAllValuesFilled(value1, value2, value3, comboBox1, comboBox2, comboBox3) &&
                           value1.Text.Length == 13 && value2.Text.Length >= 5 && value2.Text.Length <= 12 && value3.Text.Length == 13;
                case "Cheque":
                    if (!DateTime.TryParseExact(value1.Text, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
                    {
                        MessageBox.Show("Введите дату в верном формате!");
                        return false;
                    }
                    return InputValidators.AreAllValuesFilled(value1, value2, value3, value4, comboBox1, comboBox2, comboBox3, comboBox4) &&
                           InputValidators.CheckIntegerValue(value2, 0, 32767) &&
                           InputValidators.CheckDoubleValue(value3, 0, double.MaxValue) &&
                           InputValidators.CheckIntegerValue(value4, 0, 100) &&
                           DateTime.TryParseExact(value1.Text, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
                default:
                    return true;
            }
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dataGrid.SelectedItem != null)
                {
                       string nameOfTable = selectedTable.Substring(0, selectedTable.Length - 4);
                    

                    // Получаем выбранную строку
                    DataRowView rowView = dataGrid.SelectedItem as DataRowView;

                    switch (nameOfTable)
                    {
                        case "Countries":
                        case "VAT":
                        case "ProductType":
                        case "ProductSample":
                        case "ProviderName":
                            value1.Text = rowView.Row.ItemArray[1].ToString();
                            break;

                        case "StoreAddress":
                        case "ProviderAddress":
                            value1.Text = rowView.Row.ItemArray[1].ToString();
                            value2.Text = rowView.Row.ItemArray[2].ToString();
                            break;

                        case "TaxInvoice":
                            value1.Text = rowView.Row.ItemArray[1].ToString();
                            DateTime date = (DateTime)rowView.Row.ItemArray[2];
                            value2.Text = date.ToString("yyyy-MM-dd");
                            value3.Text = rowView.Row.ItemArray[6].ToString();
                            value4.Text = rowView.Row.ItemArray[7].ToString();
                            value5.Text = rowView.Row.ItemArray[8].ToString();

                            string textValue = ((TextBox)value4).Text;
                            textValue = textValue.Replace(',', '.');
                            value4.Text = textValue;

                            InputValidators.selectInCombobox(comboBox1, rowView.Row.ItemArray[3].ToString());
                            InputValidators.selectInCombobox(comboBox2, rowView.Row.ItemArray[4].ToString());
                            InputValidators.selectInCombobox(comboBox3, rowView.Row.ItemArray[5].ToString());
                            InputValidators.selectInCombobox(comboBox4, rowView.Row.ItemArray[9].ToString());
                            break;
                        case "ProductProvider":
                            value1.Text = rowView.Row.ItemArray[2].ToString();
                            value2.Text = rowView.Row.ItemArray[6].ToString();
                            value3.Text = rowView.Row.ItemArray[7].ToString();

                            InputValidators.selectInCombobox(comboBox1, rowView.Row.ItemArray[1].ToString());
                            InputValidators.selectInCombobox(comboBox2, rowView.Row.ItemArray[3].ToString());
                            InputValidators.selectInCombobox(comboBox3, rowView.Row.ItemArray[4].ToString());
                            InputValidators.selectInCombobox(comboBox4, rowView.Row.ItemArray[5].ToString());
                            break;

                        case "Cheque":
                            value1.Text = rowView.Row.ItemArray[1].ToString();
                            DateTime date2 = (DateTime)rowView.Row.ItemArray[1];
                            value1.Text = date2.ToString("yyyy-MM-dd HH:mm:ss");
                            value2.Text = rowView.Row.ItemArray[6].ToString();
                            value3.Text = rowView.Row.ItemArray[7].ToString();
                            value4.Text = rowView.Row.ItemArray[8].ToString();
                            InputValidators.selectInCombobox(comboBox1, rowView.Row.ItemArray[2].ToString());
                            InputValidators.selectInCombobox(comboBox2, rowView.Row.ItemArray[3].ToString());
                            InputValidators.selectInCombobox(comboBox3, rowView.Row.ItemArray[5].ToString());
                            InputValidators.selectInCombobox(comboBox4, rowView.Row.ItemArray[9].ToString());
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при редактировании!" + ex.Message);
            }
        }

        private void dataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            if (dataGrid.Columns[dataGrid.Columns.Count - 1].Header.ToString() == "PRIVATE")
            {
                dataGrid.Columns[dataGrid.Columns.Count - 1].Visibility = Visibility.Collapsed;
            }
            fillTextBoxes();
            redactGrid.Visibility = Visibility.Visible; 
        }

        private void dataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string header = e.Column.Header.ToString();

            if (header.Contains("Дата поставки")) e.Column.ClipboardContentBinding.StringFormat = "yyyy-MM-dd";
            else if (header.Contains("Дата")) e.Column.ClipboardContentBinding.StringFormat = "yyyy-MM-dd HH:mm:ss";
        }

    }
}
