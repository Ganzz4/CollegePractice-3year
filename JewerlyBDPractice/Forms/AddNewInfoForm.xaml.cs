using System;
using System.Windows;
using System.Windows.Controls;
using System.Globalization;

namespace Lab11
{
    /// <summary>
    /// Логика взаимодействия для AddNewInfoForm.xaml
    /// </summary>
    public partial class AddNewInfoForm : Window
    {
        public AddNewInfoForm(string tableName)
        {
            InitializeComponent();
            try
            {
                if (tableName != "")
                    mainComboBox.SelectedValue = mainComboBox.FindName(tableName);
            }
            catch(Exception ex) {
                MessageBox.Show("Ошибка при открытии таблицы!" + ex.Message,"Ошибка!");
            }
           
        }

       
        // скрытие всех grid
        private void visibleAllValuesFalse()
        {
            Helper.visibleValuesFalse(gridValue1, gridValue2, gridValue3, gridValue4, gridValue5, gridComboBox1, gridComboBox2, gridComboBox3, gridComboBox4);
        }

        // проверка значений
        private bool CheckFields(int index)
        {
            switch (index)
            {
                case 1:
                    return InputValidators.AreAllValuesFilled(value1);
                case 2:
                    return InputValidators.AreAllValuesFilled(value1, value2);
                case 3:
                    return InputValidators.CheckIntegerValue(value1, 0, 100);
                case 4:
                    return InputValidators.AreAllValuesFilled(value1);
                case 5:
                    if (!InputValidators.CheckIntegerValue(value1, 375, 999))
                    {
                        MessageBox.Show("Проба не может быть меньше 375 и больше 999");
                        return false;
                    }
                    return InputValidators.CheckIntegerValue(value1, 375, 999);
                case 6:
                    return InputValidators.AreAllValuesFilled(value1);
                case 7:
                    return InputValidators.AreAllValuesFilled(value1,value2);
                case 8:
                    if(!DateTime.TryParseExact(value2.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
                    {
                        MessageBox.Show("Введите дату в верном формате!");
                        return false;
                    }
                    return InputValidators.AreAllValuesFilled(value1, value2, value3, value4, value5, comboBox1, comboBox2, comboBox3, comboBox4) &&
                           DateTime.TryParseExact(value2.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out _) &&
                           InputValidators.CheckIntegerValue(value3, 1, int.MaxValue) &&
                           InputValidators.CheckDoubleValue(value4, 0, double.MaxValue) &&
                           InputValidators.CheckDoubleValue(value5, 0, double.MaxValue);
                case 9:
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
                case 10:
                    return InputValidators.AreAllValuesFilled(value1, value2, value3, comboBox1, comboBox2, comboBox3, comboBox4) &&
                           InputValidators.CheckIntegerValue(value1, 0, 32767) &&
                           InputValidators.CheckDoubleValue(value2, 0, double.MaxValue) &&
                           InputValidators.CheckIntegerValue(value3, 1, 100);
                default:
                    return true;
            }
        }

        // подача запроса в функцию addNewInfo
        private void addInfo_Click(object sender, RoutedEventArgs e)
        {
            if (mainComboBox.SelectedIndex == 0)
            {
                return;
            }

            if (!CheckFields(mainComboBox.SelectedIndex))
            {
                MessageBox.Show("Корректно заполните все поля!", "Ошибка!");
                return;
            }

            switch (mainComboBox.SelectedIndex)
            {
                case 1:
                    DatabaseActions.CRUDInDB('A', $"EXEC InsertCountry @country_name = '{value1.Text}'");
                    break;
                case 2:
                    DatabaseActions.CRUDInDB('A', $"EXEC InsertStoreAddress @store_city = '{value1.Text}' , @store_street = '{value2.Text}' ");
                    break;
                case 3:
                    DatabaseActions.CRUDInDB('A', $"EXEC InsertVAT @vat_procent = '{value1.Text}'");
                    break;
                case 4:
                    DatabaseActions.CRUDInDB('A', $"EXEC InsertProductType @product_type_name = '{value1.Text}'");
                    break;
                case 5:
                    DatabaseActions.CRUDInDB('A', $"EXEC InsertProductSample @sample_name = '{value1.Text}'");
                    break;
                case 6:
                    DatabaseActions.CRUDInDB('A', $"EXEC InsertProviderName @provider_name = '{value1.Text}'");
                    break;
                case 7:
                    DatabaseActions.CRUDInDB('A', $"EXEC InsertProviderAddress @provider_city = '{value1.Text}' , @provider_street = '{value2.Text}'");
                    break;
                case 8:
                    DatabaseActions.CRUDInDB('A', $"EXEC InsertTaxInvoice " +
                        $"@tax_invoice_number = '{value1.Text}'," +
                        $" @delivery_date = '{value2.Text}' ," +
                        $" @provider_code = '{InputValidators.returnValueFromComboBox(comboBox1, comboBox1.Text)}'" +
                        $", @product_type_code = '{InputValidators.returnValueFromComboBox(comboBox2, comboBox2.Text)}'," +
                        $" @sample_code = '{InputValidators.returnValueFromComboBox(comboBox3, comboBox3.Text)}'" +
                        $", @quantity = '{value3.Text}'," +
                        $" @weight_grams = '{value4.Text}'," +
                        $" @price_per_gramm = '{value5.Text}' ," +
                        $" @vat_code = '{InputValidators.returnValueFromComboBox(comboBox4, comboBox4.Text)}'");
                    break;
                case 9:
                    DatabaseActions.CRUDInDB('A', $"EXEC InsertProductProvider " +
                        $"@provider_code = '{InputValidators.returnValueFromComboBox(comboBox1, comboBox1.Text)}'," +
                        $" @fiscal_code = '{value1.Text}' , " +
                        $"@provider_country_code = '{InputValidators.returnValueFromComboBox(comboBox2, comboBox2.Text)}'," +
                        $"@provider_address_code = '{InputValidators.returnValueFromComboBox(comboBox3, comboBox3.Text)}', " +
                        $"@phone_number = '{value2.Text}', " +
                        $"@payment_account = '{value3.Text}'");
                    break;
                case 10:
                    DatabaseActions.CRUDInDB('A', $"EXEC InsertCheque " +
                    $"@date_and_time_of_sale = '{DateTime.Now}'," +
                    $"@store_country_code = '{InputValidators.returnValueFromComboBox(comboBox1, comboBox1.Text)}' , " +
                    $"@store_adress_code = '{InputValidators.returnValueFromComboBox(comboBox2, comboBox2.Text)}'," +
                    $"@product_type_code = '{InputValidators.returnValueFromComboBox(comboBox3, comboBox3.Text)}', " +
                    $"@weight_grams = '{value1.Text}', " +
                    $"@price_per_gram = '{value2.Text}' ,"+
                    $"@discount_percentage = '{value3.Text}', " +
                    $"@vat_code = '{InputValidators.returnValueFromComboBox(comboBox4, comboBox4.Text)}'");
                    break;
            }
            Close();
        }

        // отображение, заполнение TextBlock, ComboBox и привязка типов TextBox 
        private void mainComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (mainComboBox.SelectedIndex)
            {
                case 0:
                    if (gridValue1 != null)visibleAllValuesFalse();
                    break;
                case 1:
                    Helper.SetTextBoxProperties(value1, InputValidators.place_PreviewTextInput, 40);
                    visibleAllValuesFalse();
                    textBlockValue1.Text = "Название страны";
                    Helper.visibleValuesTrue(gridValue1);
                  
                    break;
                case 2:
                    Helper.SetTextBoxProperties(value1, InputValidators.place_PreviewTextInput, 40);
                    value2.MaxLength = 40;
                    visibleAllValuesFalse();
                    textBlockValue1.Text = "Название города";
                    textBlockValue2.Text = "Улица";
                    Helper.visibleValuesTrue(gridValue1,gridValue2);
                    break;

                    case 3:
                    Helper.SetTextBoxProperties(value1, InputValidators.onlyDigits_PreviewTextInput, 3);
                    visibleAllValuesFalse();
                    textBlockValue1.Text = "НДС";
                    Helper.visibleValuesTrue(gridValue1);
                    break;
                case 4:
                    Helper.SetTextBoxProperties(value1, InputValidators.onlyLetters_PreviewTextInput, 15);
                    visibleAllValuesFalse();
                    textBlockValue1.Text = "Тип изделия";
                    Helper.visibleValuesTrue(gridValue1);
                    break;
                case 5:
                    Helper.SetTextBoxProperties(value1, InputValidators.onlyDigits_PreviewTextInput, 3);
                    visibleAllValuesFalse();
                    textBlockValue1.Text = "Проба('375-999')";
                    Helper.visibleValuesTrue(gridValue1);
                    break;
                case 6:
                    Helper.SetTextBoxProperties(value1, InputValidators.onlyLetters_PreviewTextInput, 20);

                    visibleAllValuesFalse();
                    textBlockValue1.Text = "Название поставщика";
                    Helper.visibleValuesTrue(gridValue1);
                    break;
                case 7:
                    Helper.SetTextBoxProperties(value1, InputValidators.place_PreviewTextInput, 40);
                    value2.MaxLength = 40;

                    visibleAllValuesFalse();
                    textBlockValue1.Text = "Название города";
                    textBlockValue2.Text = "Улица";
                    Helper.visibleValuesTrue(gridValue1, gridValue2);
                    break;
                case 8:
                    visibleAllValuesFalse();

                    Helper.SetTextBoxProperties(value1, InputValidators.onlyDigits_PreviewTextInput, 10);
                    Helper.SetTextBoxProperties(value2, InputValidators.date_PreviewTextInput, 10);
                    Helper.SetTextBoxProperties(value3, InputValidators.onlyDigits_PreviewTextInput, 5);
                    Helper.SetTextBoxProperties(value4, InputValidators.doubles_PreviewTextInput, 10);
                    Helper.SetTextBoxProperties(value1, InputValidators.onlyDigits_PreviewTextInput, 10);
                    Helper.SetTextBoxProperties(value5, InputValidators.doubles_PreviewTextInput, 10);

                    textBlockValue1.Text = "Номер";
                    textBlockValue2.Text = "Дата(yyyy-mm-dd)";
                    textBlockValue3.Text = "Количество";
                    textBlockValue4.Text = "Вес(грамм)";
                    textBlockValue5.Text = "Цена за грамм";

                    textBlockComboValue1.Text = "Название поставщика";
                    textBlockComboValue2.Text = "Тип изделия";
                    textBlockComboValue3.Text = "Проба";
                    textBlockComboValue4.Text = "НДС";

                    DatabaseActions.FillComboBox(comboBox1, "ProviderName", "provider_code", "provider_name");
                    DatabaseActions.FillComboBox(comboBox2, "ProductType", "product_type_code", "product_type_name");
                    DatabaseActions.FillComboBox(comboBox3, "ProductSample", "sample_code", "sample_name");
                    DatabaseActions.FillComboBox(comboBox4, "VAT", "vat_code", "vat_procent");
                 
                    Helper.visibleValuesTrue(gridComboBox2, gridValue1, gridValue2, gridValue3, gridValue4,gridValue5
                        , gridComboBox1, gridComboBox3, gridComboBox4 );
                    break;
                case 9:

                    Helper.SetTextBoxProperties(value1, InputValidators.onlyDigits_PreviewTextInput, 13);
                    Helper.SetTextBoxProperties(value2, InputValidators.onlyDigits_PreviewTextInput, 12);
                    Helper.SetTextBoxProperties(value3, InputValidators.onlyDigits_PreviewTextInput, 13);

                    visibleAllValuesFalse();
                    textBlockValue1.Text = "Фискальный код(13 символов)";
                    textBlockValue2.Text = "Номер телефона(5-12 символов)";
                    textBlockValue3.Text = "Платёжный счёт(13 символов)";
                    textBlockComboValue1.Text = "Название поставщика";
                    textBlockComboValue2.Text = "Страна поставщика";
                    textBlockComboValue3.Text = "Адрес поставщика";

                    DatabaseActions.FillComboBox(comboBox1, "ProviderName", "provider_code", "provider_name");
                    DatabaseActions.FillComboBox(comboBox2, "ProviderCountry", "provider_country_code", "provider_country_name");
                    DatabaseActions.FillComboBox(comboBox3, "ProviderAddress", "provider_address_code", "provider_city", "provider_street");
                    Helper.visibleValuesTrue(gridComboBox1,gridComboBox2,gridComboBox3,gridValue1,gridValue2,gridValue3);
                    break;

                case 10:
                    Helper.SetTextBoxProperties(value1, InputValidators.onlyDigits_PreviewTextInput, 10);
                    Helper.SetTextBoxProperties(value2, InputValidators.doubles_PreviewTextInput, 10);
                    Helper.SetTextBoxProperties(value3, InputValidators.onlyDigits_PreviewTextInput, 3);

                    visibleAllValuesFalse();
                    textBlockValue1.Text = "Вес(грамм)";
                    textBlockValue2.Text = "Цена за грамм";
                    textBlockValue3.Text = "Скидка %";

                    textBlockComboValue1.Text = "Страна";
                    textBlockComboValue2.Text = "Адрес";
                    textBlockComboValue3.Text = "Тип продукта";
                    textBlockComboValue4.Text = "НДС";

                    DatabaseActions.FillComboBox(comboBox1, "Countries", "country_code", "country_name");
                    DatabaseActions.FillComboBox(comboBox2, "StoreAddress", "store_address_code", "store_city", "store_street");
                    DatabaseActions.FillComboBox(comboBox3, "ProductType", "product_type_code", "product_type_name");
                    DatabaseActions.FillComboBox(comboBox4, "VAT", "vat_code", "vat_procent");

                    Helper.visibleValuesTrue(gridValue1, gridValue2, gridValue3,
                         gridComboBox1, gridComboBox2, gridComboBox3, gridComboBox4);
                    break;
            }
        }


    }
}