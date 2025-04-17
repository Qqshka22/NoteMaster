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
    /// Логика взаимодействия для notes.xaml
    /// </summary>
    public partial class notes : Window
    {
        public notes()
        {
            InitializeComponent();
            LoadDataFromDatabase();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Hide();
        }

        private void LoadDataFromDatabase()
        {
            // Строка подключения к PostgreSQL
            string connectionString = "Host=localhost;Username=postgres;Password=0000;Database=noteapp";

            List<User> users = new List<User>();

            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    // SQL-запрос для выборки данных
                    string query = "SELECT * FROM notes";
                    using (var command = new NpgsqlCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                users.Add(new User
                                {
                                    Id = reader.GetGuid(0),
                                    title = reader.GetString(1),
                                    content = reader.GetString(2),
                                    categoryid = reader.GetGuid(3),
                                    tags = reader.GetString(4),
                                    createdat = reader.GetDateTime(5),
                                    updatedat = reader.GetDateTime(6),
                                    isarchived = reader.GetBoolean(7),
                                    isdeleted = reader.GetBoolean(8)
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            
        }
    }

    // Модель данных
    public class User
    {
        public Guid Id { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public Guid categoryid { get; set; }
        public string tags { get; set; }
        public DateTime createdat { get; set; }
        public DateTime updatedat { get; set; }
        public bool isarchived { get; set; }
        public bool isdeleted { get; set; }
    }
}
