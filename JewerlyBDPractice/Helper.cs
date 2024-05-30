using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Lab11
{
    public static class Helper
    {
        public static void visibleValuesTrue(params UIElement[] controls)
        {
            foreach (var control in controls)
            {
                if (control.Visibility == Visibility.Collapsed)
                    control.Visibility = Visibility.Visible;
            }
        }

        public static void visibleValuesFalse(params UIElement[] controls)
        {
            foreach (var control in controls)
            {
                if (control.Visibility == Visibility.Visible)
                    control.Visibility = Visibility.Collapsed;
            }
        }

        public static void clearControls(params Control[] controls)
        {
            foreach (var control in controls)
            {
                if (control is TextBox textBox)
                {
                    textBox.Clear();
                }
                else if (control is ComboBox comboBox)
                {
                    comboBox.SelectedIndex = -1;
                }
            }
        }

        public static void lineNumbering(DataSet ds)
        {
            // Добавляем временную колонку для нумерации строк перед заполнением данными
            DataColumn rowNumColumn = new DataColumn("N", typeof(int));
            ds.Tables[0].Columns.Add(rowNumColumn);

            // Заполняем колонку номерами строк
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++) ds.Tables[0].Rows[i]["N"] = i + 1;

            ds.Tables[0].Columns["N"].SetOrdinal(0);
        }

        public static void SetTextBoxProperties(TextBox textBox, TextCompositionEventHandler handler, int maxLength, string helperText = "")
        {
            textBox.PreviewTextInput += handler;
            textBox.MaxLength = maxLength;
            if (helperText != "") setHelperText(textBox,helperText);
        }
       
        public static void setHelperText(UIElement element, string helperText)
        {
            MaterialDesignThemes.Wpf.HintAssist.SetHelperText(element, helperText);
        }

        public static string returnCellText(DataGrid dataGrid, int index)
        {
            // Проверяем, есть ли выбранная строка
            if (dataGrid.SelectedItem != null)
            {
                // Получаем текущую строку из DataGrid
                DataRowView rowView = (DataRowView)dataGrid.SelectedItem;

                // Получаем значение ячейки по имени столбца
                string cellText = rowView[dataGrid.Columns[index].Header.ToString()].ToString();

                return cellText;
            }

            return string.Empty;
        }
    }
}
