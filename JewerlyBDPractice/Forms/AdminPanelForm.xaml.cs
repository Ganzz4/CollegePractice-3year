using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;


namespace Lab11.Forms
{
    /// <summary>
    /// Логика взаимодействия для AdminPanelForm.xaml
    /// </summary>
    public partial class AdminPanelForm : Window
    {
        string login;
        public AdminPanelForm(string userLogin)
        {
            InitializeComponent();
            login = userLogin;
        }

        private void buttonExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private async void createBackup_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                string backupFilePath = string.Empty;

                // Открываем диалоговое окно для выбора пути файла
                using (System.Windows.Forms.SaveFileDialog saveFileDialog = new System.Windows.Forms.SaveFileDialog())
                {
                    saveFileDialog.Filter = "Backup Files (*.bak)|*.bak|All Files (*.*)|*.*";
                    saveFileDialog.DefaultExt = "bak";
                    saveFileDialog.AddExtension = true;
                    saveFileDialog.Title = "Сохранение бэкапа базы данных";

                    if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) backupFilePath = saveFileDialog.FileName;
                    else return;

                }

                // Проверяем, что путь к файлу не пустой
                if (string.IsNullOrWhiteSpace(backupFilePath))
                {
                    Console.WriteLine("Неверный путь к файлу.");
                    return;
                }

                string backupQuery = "BACKUP DATABASE JEWERLYBD TO DISK = N'" + backupFilePath + "' WITH NOFORMAT, NOINIT, NAME = N'JEWERLYBD-Full Database Backup', SKIP, NOREWIND, NOUNLOAD,  STATS = 10";


                using (var db = new Database())
                {
                    db.OpenConnection();
                    SqlCommand command = new SqlCommand(backupQuery, db.GetConnection());

                    await command.ExecuteNonQueryAsync();
                    MessageBox.Show("Успешное создание резервной копиии!\nПуть к файлу резервной копии: " + backupFilePath, "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                    db.CloseConnection();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при выполнении операции!" + ex.Message, "Ошибка!");
                return;
            }

        }

        private async void restoreBackup_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string filePath;

                // Открываем диалоговое окно для выбора пути файла
                using (System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog())
                {
                    openFileDialog.Filter = "Backup Files (*.bak)|*.bak|All Files (*.*)|*.*";
                    openFileDialog.Title = "Сохранение бэкапа базы данных";

                    if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        filePath = openFileDialog.FileName;
                    else
                        return;
                }

                // Проверяем, что путь к файлу не пустой
                if (string.IsNullOrWhiteSpace(filePath))
                {
                    MessageBox.Show("Неверный путь к файлу.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                string connectionString = @"Data Source=SANCHO;Initial Catalog=master;Integrated Security=True;Encrypt=False;";

                using (SqlConnection masterConnection = new SqlConnection(connectionString))
                {
                    await masterConnection.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand("RestoreDatabase", masterConnection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@FilePath", filePath);
                        await cmd.ExecuteNonQueryAsync();
                    }

                    MessageBox.Show("Успешное восстановление резервной копии", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Ошибка при выполнении операции: {ex.Message}", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Неизвестная ошибка: {ex.Message}", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void changeRoleUser_Click(object sender, RoutedEventArgs e)
        {
          UserManagment userManagment = new UserManagment(login);
            userManagment.ShowDialog();

        }
    }
}
