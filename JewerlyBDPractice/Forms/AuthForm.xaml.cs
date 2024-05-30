using System;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows;

namespace Lab11
{
    /// <summary>
    /// Логика взаимодействия для Auth.xaml
    /// </summary>
    public partial class Auth : Window
    {
        bool fromAdminPanel = false;

        public Auth()
        {
            InitializeComponent();
            setTextBoxProperties();
        }

        public Auth(bool fromAdminPanel = false)
        {
            InitializeComponent();
            setTextBoxProperties();
            this.fromAdminPanel = fromAdminPanel;
            if (fromAdminPanel)
            {
                buttonsEntryReg.Visibility = Visibility.Collapsed;
                panelConfirmPassword.Visibility = Visibility.Visible;
                ConfirmRegistration.Visibility = Visibility.Visible;
                arrowBack.Visibility = Visibility.Visible;
                TopPanelText.Text = "Регистрация";
            }
        }

        private void Entry_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!CheckFields()) return;
                string role = "user";

                using (var db = new Database())
                {
                    db.OpenConnection();

                    string query = $"SELECT dbo.ValidateUser(@login, @password)";
                    SqlCommand command = new SqlCommand(query, db.GetConnection());
                    command.Parameters.AddWithValue("@login", textBoxLogin.Text);
                    command.Parameters.AddWithValue("@password", textBoxPassword.Password);

                    object result = command.ExecuteScalar();

                    if (result != DBNull.Value)
                    {
                        role = (string)result;
                        MainWindow mainWindow = new MainWindow(role, textBoxLogin.Text);
                        mainWindow.Show();
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("Неверный логин или пароль!", "Ошибка входа", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }

                    db.CloseConnection();
                }

            }
            catch (SqlException ex)
            {
                MessageBox.Show("Ошибка при выполнении операции!" + ex.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при выполнении операции!" + ex.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private bool CheckFields()
        {
            Regex regexLogin = new Regex("^[a-zA-Z][a-zA-Z0-9_.]{4,10}");
            Regex regexPassword = new Regex("^(?=.*[a-zA-Z])(?=.*\\d)[A-Za-z\\d!]{5,20}");

            if (textBoxLogin.Text.Length <5)
            {
                MessageBox.Show("Логин должен быть длинее 5 символов!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (!regexLogin.IsMatch(textBoxLogin.Text))
            {
                    MessageBox.Show("Логин должен начинаться с буквы и должен быть длинее 5 символов!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
            }

            if (textBoxPassword.Password.Length < 5 || textBoxPassword.Password.Length > 20)
            {
                MessageBox.Show("Пароль должен быть длинее 5 символов и меньше 20!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (!regexPassword.IsMatch(textBoxPassword.Password))
            {
                MessageBox.Show("Пароль должен содержать хотя бы 1 букву, цифру и должен быть длинее 5 символов!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (!InputValidators.AreAllValuesFilled(textBoxLogin, textBoxPassword)) {
                MessageBox.Show("Не все поля заполнены!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
             }
            return true;

            }

        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            buttonsEntryReg.Visibility = Visibility.Collapsed;
            panelConfirmPassword.Visibility = Visibility.Visible;
            ConfirmRegistration.Visibility = Visibility.Visible;
            arrowBack.Visibility = Visibility.Visible;
            textBoxLogin.Clear();
            textBoxConfirmPassword.Clear();
            textBoxPassword.Clear();
            TopPanelText.Text = "Регистрация";
        }

        private void ConfirmRegistration_Click(object sender, RoutedEventArgs e){
      try
            {
                if (!CheckFields()) return;

                if (!textBoxPassword.Password.Equals(textBoxConfirmPassword.Password))
                {
                    MessageBox.Show("Пароли не совпадают!", "Ошибка регистрации", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                using (var db = new Database())
                {
                    db.OpenConnection();
                    // Проверка на существование пользователя с таким же логином
                    SqlCommand checkUserCommand = new SqlCommand($"SELECT COUNT(*) FROM UserData WHERE UserLogin = '{textBoxLogin.Text}' ", db.GetConnection());; ;

                    int userCount = (int)checkUserCommand.ExecuteScalar();

                    if (userCount > 0)
                    {
                        MessageBox.Show("Пользователь с таким логином уже существует!", "Ошибка регистрации", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    // Вставка нового пользователя
                    SqlCommand insertCommand = new SqlCommand($"EXEC CreateUser @userLogin = '{textBoxLogin.Text}', @userPassword = '{textBoxPassword.Password}', @userRole = 'user'", db.GetConnection());

                    int rowsAffected = insertCommand.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Регистрация прошла успешно!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                        // Переход на главную страницу или вход в систему
                        if (!fromAdminPanel)
                        {
                            MainWindow mainWindow = new MainWindow("user", textBoxLogin.Text);
                            mainWindow.Show();
                        }
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("Ошибка при регистрации пользователя!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    db.CloseConnection();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Ошибка при выполнении операции!" + ex.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при выполнении операции!" + ex.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }

        private void arrowBack_Click(object sender, RoutedEventArgs e)
        {
            buttonsEntryReg.Visibility = Visibility.Visible;
            panelConfirmPassword.Visibility = Visibility.Collapsed;
            textBoxConfirmPassword.Password = "";
            ConfirmRegistration.Visibility = Visibility.Collapsed;
            textBoxLogin.Clear();
            textBoxConfirmPassword.Clear();
            textBoxPassword.Clear();

            arrowBack.Visibility = Visibility.Collapsed;
            TopPanelText.Text = "Авторизация";
        }

        private void buttonExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void setTextBoxProperties()
        {
            Helper.SetTextBoxProperties(textBoxLogin, InputValidators.login_PreviewTextInput, 20);
            textBoxPassword.PreviewTextInput += InputValidators.password_PreviewTextInput;
            textBoxPassword.MaxLength = 30;

            textBoxConfirmPassword.PreviewTextInput += InputValidators.password_PreviewTextInput;
            textBoxConfirmPassword.MaxLength = 30;
        }
    }
    }
