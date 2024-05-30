using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace Lab11.Forms
{
    /// <summary>
    /// Логика взаимодействия для UserManagment.xaml
    /// </summary>
    public partial class UserManagment : Window
    {
        string selectedTable = "UserDataView";

        string login;

        public UserManagment(string userLogin)
        {
            InitializeComponent();
            login = userLogin;
        }

        private void dataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            if (selectedTable != "")
                DatabaseActions.CRUDInDB('R', $"SELECT * FROM {selectedTable}", dataGrid);
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

                MessageBoxResult mb = MessageBox.Show("Вы уверены, что хотите удалить данные?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (mb == MessageBoxResult.No) return;

                string nameOfTable = "";
                if (selectedTable.Length >= 4)
                {
                    nameOfTable = selectedTable.Substring(0, selectedTable.Length - 4);
                }
                if(login == Helper.returnCellText(dataGrid, dataGrid.Columns.Count - 2))
                {
                    MessageBox.Show("Вы не можете удалить себя!", "Ошибка!");
                    return;
                }
                DatabaseActions.CRUDInDB('D', $"EXEC DeleteUser @userLogin = '{Helper.returnCellText(dataGrid, dataGrid.Columns.Count - 2)}'");

                DatabaseActions.CRUDInDB('R', $"SELECT * FROM {selectedTable}", dataGrid);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Возникла ошибка!" + ex.Message);
            }

        }

        private void redact_Click(object sender, RoutedEventArgs e)
        {
            if (login == Helper.returnCellText(dataGrid, dataGrid.Columns.Count - 2))
            {
                MessageBox.Show("Вы не можете изменить свою роль!", "Ошибка!");
                return;
            }
            else
            {
                ChangeUserRole changeUserRole = new ChangeUserRole(Helper.returnCellText(dataGrid, 1), Helper.returnCellText(dataGrid, 2));
                changeUserRole.ShowDialog();
                if (selectedTable != "")
                    DatabaseActions.CRUDInDB('R', $"SELECT * FROM {selectedTable}", dataGrid);
            }


        }

        private void PackIcon_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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

        private void addInfo_Click(object sender, RoutedEventArgs e)
        {
            Auth auth = new Auth(true);
            auth.ShowDialog();

            if (selectedTable != "")
                DatabaseActions.CRUDInDB('R', $"SELECT * FROM {selectedTable}", dataGrid);
        }
    }
}
