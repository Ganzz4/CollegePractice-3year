using System;
using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Lab11
{
    public static class InputValidators
    {
        public static void place_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^а-яА-Я\\s\\-\\/]+").IsMatch(e.Text);
        }

        public static void onlyLetters_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^а-яА-Яa-zA-Z]+").IsMatch(e.Text);
        }

        public static void FIO_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^а-яА-Я\\s\\.]+").IsMatch(e.Text);
        }

        public static void providerName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^a-zA-Zа-яА-я\\s\\-\\&]+").IsMatch(e.Text);
        }

        public static void date_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9\\s\\-]+").IsMatch(e.Text);
        }
        public static void dateTime_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9\\s\\-\\:]+").IsMatch(e.Text);
        }
        public static void doubles_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9\\.]+").IsMatch(e.Text);
        }

        public static void onlyDigits_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        public static void login_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("^[a-zA-Z][a-zA-Z0-9_.]{2,20}").IsMatch(e.Text);
        }

        public static void password_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("^(?=.*[a-zA-Z])(?=.*\\d)(?=.*[!@#$%^&*])[A-Za-z\\d!@#$%^&*]{8,30}").IsMatch(e.Text);
        }

        public static bool CheckIntegerValue(FrameworkElement element, int minValue, int maxValue)
        {
            if (!AreAllValuesFilled(element))
                return false;

            if (!int.TryParse(((TextBox)element).Text, out int number))
                return false;

            return number >= minValue && number <= maxValue;
        }

        public static bool CheckDoubleValue(FrameworkElement element, double minValue, double maxValue)
        {
            if (!AreAllValuesFilled(element))
                return false;

            if (!double.TryParse(((TextBox)element).Text, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out double number))
                return false;

            return number >= minValue && number <= maxValue;
        }

        public static bool AreAllValuesFilled(params FrameworkElement[] elements)
        {
            foreach (var element in elements)
            {
                if (element is TextBox textBox)
                {
                    if (string.IsNullOrEmpty(textBox.Text))
                    {
                        return false;
                    }
                }
                else if (element is ComboBox comboBox)
                {
                    if (comboBox.SelectedItem == null)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static void selectInCombobox(ComboBox comboBox, string value)
        {
            foreach (DataRowView item in comboBox.Items){
                if (item.Row[1].ToString() == value){
                    comboBox.SelectedItem = item;
                    break;
                }
            }
        }

        public static string returnValueFromComboBox(ComboBox comboBox, string value)
        {
            foreach (DataRowView item in comboBox.Items){
                if (item.Row[1].ToString() == value)
                    return item.Row[0].ToString();
            }
            return "-1";
        }
    }
}
