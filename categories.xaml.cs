using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Npgsql;

namespace NoteMaster
{
    /// <summary>
    /// Логика взаимодействия для categories.xaml
    /// </summary>
    public partial class categories : Window
    {
        public categories()
        {
            InitializeComponent();
            LoadDataFromDatabase();
        }

        private void LoadDataFromDatabase()
        {
            // Строка подключения к PostgreSQL
            string connectionString = "Host=localhost;Username=postgres;Password=0000;Database=noteapp";

            List<categoriesclass> users = new List<categoriesclass>();

            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    // SQL-запрос для выборки данных
                    string query = "SELECT * FROM categories";
                    using (var command = new NpgsqlCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                users.Add(new categoriesclass
                                {
                                    Id = reader.GetGuid(0),
                                    name = reader.GetString(1)
                                });
                            }
                        }
                    }
                }

                // Привязываем данные к DataGrid
                dataGrid.ItemsSource = users;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Hide();
        }
    }

    // Модель данных
    public class categoriesclass
    {
        public Guid Id { get; set; }
        public string name { get; set; }
    }
}