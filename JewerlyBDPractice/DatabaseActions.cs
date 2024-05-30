using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows;
using System.Windows.Controls;


namespace Lab11{
    public static class DatabaseActions
    {
        public static void FillComboBox(ComboBox comboBox, string tableName, string valueMember, string displayMember, string additionalDisplayMember = "")
        {
            try
            {
                string query = $"SELECT DISTINCT {valueMember}, {displayMember}" + (string.IsNullOrEmpty(additionalDisplayMember) ? "" : $", {additionalDisplayMember}") + $" FROM {tableName}";
                
                using (var db = new Database())
                {
                    db.OpenConnection();

                    SqlConnection connection = db.GetConnection();

                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds, $"{tableName}_data");

                    if (ds.Tables.Contains($"{tableName}_data"))
                    {
                        comboBox.SelectedValuePath = valueMember;
                        if (string.IsNullOrEmpty(additionalDisplayMember))
                            comboBox.DisplayMemberPath = displayMember;
                        else
                        {
                           var newColumn = ds.Tables[$"{tableName}_data"].Columns.Add("FullDisplay", typeof(string), $"{displayMember} + ', ' + {additionalDisplayMember}");
                           newColumn.SetOrdinal(1);
                            comboBox.DisplayMemberPath = "FullDisplay";
                            
                        }
                        comboBox.ItemsSource = ds.Tables[$"{tableName}_data"].DefaultView;
                    }

                    db.CloseConnection();
                }
            }
            catch
            {
                MessageBox.Show("Невозможно подключиться к базе данных", "Ошибка!");
            }
        }

        public static void SearchInDB(DataGrid dataGrid, string searchText, string selectedTable)
        {
            try
            {
                dataGrid.ItemsSource = null;
                using (var db = new Database())
                {
                    db.OpenConnection();
                    SqlConnection connection = db.GetConnection();
                    // Получаем имена столбцов представления
                    string findColumns = $"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{selectedTable}'";
                    SqlCommand getColumns = new SqlCommand(findColumns, connection);

                    List<string> columns = new List<string>();
                    using (SqlDataReader reader = getColumns.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            columns.Add($"[{reader.GetString(0)}]");
                        }
                    }

                    // Формируем условие поиска
                    string searchConditions = "";
                    foreach (string column in columns)
                    {
                        searchConditions += $" {column} LIKE '%{searchText}%' OR";
                    }
                    // Удаляем последний OR
                    searchConditions = searchConditions.Remove(searchConditions.Length - 2);

                    // Формируем итоговый запрос
                    string query = $"SELECT * FROM {selectedTable} WHERE {searchConditions}";
                    // Создаем SqlDataAdapter с запросом
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);


                    Helper.lineNumbering(ds);

                    // Отображаем результаты в DataGrid
                    dataGrid.ItemsSource = ds.Tables[0].DefaultView;
                    if (ds.Tables[0].Columns.Contains("PRIVATE"))
                    {
                        // Найден столбец "PRIVATE", скрываем его
                        dataGrid.Columns[dataGrid.Columns.Count - 1].Visibility = Visibility.Collapsed;
                    }
                    db.CloseConnection();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка: " + ex.Message, "Ошибка!");
            }
        }

        public static void FilterData(DataGrid dataGrid, string selectedTable, Dictionary<string, object> filterParams, string priceFilter)
        {
            try
            {
                dataGrid.ItemsSource = null;
                using (var db = new Database())
                {
                    db.OpenConnection();
                    SqlConnection connection = db.GetConnection();

                    // Формируем условие фильтрации
                    string filterConditions = "";
                    foreach (var kvp in filterParams)
                    {
                        if (kvp.Key == "DateFilter")
                        {
                            filterConditions += $"{kvp.Value} AND ";
                        }
                        else
                        {
                            filterConditions += $"[{kvp.Key}] LIKE '%{kvp.Value}%' AND ";
                        }
                    }
                    if (!string.IsNullOrEmpty(priceFilter))
                    {
                        filterConditions += priceFilter + " AND ";
                    }
                    filterConditions = filterConditions.TrimEnd(" AND ".ToCharArray()); // Удаляем последний "AND"

                    // Формируем запрос с условиями фильтрации
                    string query = $"SELECT * FROM {selectedTable} WHERE {filterConditions}";
                                             // Создаем команду с параметрами
                    SqlCommand command = new SqlCommand(query, connection);

                    // Создаем SqlDataAdapter с командой
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);

                    Helper.lineNumbering(ds);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        // Отображаем результаты в DataGrid
                        dataGrid.ItemsSource = ds.Tables[0].DefaultView;
                        if (ds.Tables[0].Columns.Contains("PRIVATE"))
                        {
                            // Найден столбец "PRIVATE", скрываем его
                            dataGrid.Columns[dataGrid.Columns.Count - 1].Visibility = Visibility.Collapsed;
                        }
                    }
                    else
                    {
                        MessageBox.Show("По заданному фильтру нет данных!", "Ошибка!");
                        DatabaseActions.CRUDInDB('R', $"SELECT * FROM {selectedTable}", dataGrid);
                    }
                    db.CloseConnection();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка: " + ex.Message, "Ошибка!");
            }
        }

        public static void CRUDInDB(char operation, string sql, DataGrid dataGrid = null)
        {
            try
            {
                using (var db = new Database())
                {
                    db.OpenConnection();
                    SqlCommand command = new SqlCommand(sql, db.GetConnection());
                    
                    int rowsAffected = command.ExecuteNonQuery();
                    switch (operation)
                    {
                        case 'R':
                            dataGrid.ItemsSource = null;


                            SqlDataAdapter adapter = new SqlDataAdapter(sql, db.GetConnection());
                            DataSet ds = new DataSet();


                            adapter.Fill(ds);

                             Helper.lineNumbering(ds);

                            dataGrid.ItemsSource = ds.Tables[0].DefaultView;

                           
                            dataGrid.Columns[0].Width = 50;

                            if (ds.Tables[0].Columns.Contains("PRIVATE"))
                                dataGrid.Columns[dataGrid.Columns.Count - 1].Visibility = Visibility.Collapsed;

                            break;
                        case 'A':
                        case 'U':
                        case 'D':
                            if (rowsAffected > 0) MessageBox.Show("Операция успешно выполнена!");
                            
                            else MessageBox.Show("Ошибка при выполнении операции!");
                            
                            break;
                        default:
                            MessageBox.Show("Неподдерживаемый тип операции.", "Ошибка!");
                            return;
                    }
                db.CloseConnection();
            }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при выполнении операции!\n" + ex.Message, "Ошибка!");
            }
        }

    }
}