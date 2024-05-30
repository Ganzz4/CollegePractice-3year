using System.Text.RegularExpressions;
using System.Windows;

namespace Lab11.Forms
{
    /// <summary>
    /// Логика взаимодействия для ChangeUserRole.xaml
    /// </summary>
    public partial class ChangeUserRole : Window
    {
        string role;
        string login;
        public ChangeUserRole(string userLogin, string userRole)
        {
            InitializeComponent();
            role = userRole;
            login = userLogin;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            comboBoxChangeRole.Items.Add("user");
            comboBoxChangeRole.Items.Add("admin");
            comboBoxChangeRole.SelectedValue = role;
            textBoxLogin.Text = login;
        }

        private void buttonExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void changeRole_Click(object sender, RoutedEventArgs e)
        {

            if (!CheckFields()) return;

            DatabaseActions.CRUDInDB('U', $"EXEC UpdateUserRole " +
    $"@userLogin = '{textBoxLogin.Text}' , " +
    $"@userRole = '{comboBoxChangeRole.Text}'");
            Close();
        }


        private bool CheckFields()
        {
            Regex regexLogin = new Regex("^[a-zA-Z][a-zA-Z0-9_.]{4,10}");

            if (textBoxLogin.Text.Length < 5)
            {
                MessageBox.Show("Логин должен быть длинее 5 символов!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (!regexLogin.IsMatch(textBoxLogin.Text))
            {
                MessageBox.Show("Логин должен начинаться с буквы и должен быть длинее 5 символов!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (!InputValidators.AreAllValuesFilled(textBoxLogin))
            {
                MessageBox.Show("Не все поля заполнены!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;

        }
    }
}
