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
    /// Логика взаимодействия для attachments.xaml
    /// </summary>
    public partial class attachments : Window
    {
        public attachments()
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

            List<attachmentsclass> users = new List<attachmentsclass>();

            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    // SQL-запрос для выборки данных
                    string query = "SELECT * FROM attachments";
                    using (var command = new NpgsqlCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                users.Add(new attachmentsclass
                                {
                                    Id = reader.GetGuid(0),
                                    noteid = reader.GetGuid(1),
                                    filepath = reader.GetString(2),
                                    filesize = reader.GetInt32(3)
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
    }

    // Модель данных
    public class attachmentsclass
    {
        public Guid Id { get; set; }
        public Guid noteid { get; set; }
        public string filepath { get; set; }
        public int filesize { get; set; }
    }
}

