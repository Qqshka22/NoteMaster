using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace NoteMaster
{
    /// <summary>
    /// Логика взаимодействия для reminders.xaml
    /// </summary>
    public partial class reminders : Window
    {
        public reminders()
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

            List<remindersclass> users = new List<remindersclass>();
            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    // SQL-запрос для выборки данных
                    string query = "SELECT * FROM reminders";
                    using (var command = new NpgsqlCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                users.Add(new remindersclass
                                {
                                    Id = reader.GetGuid(0),
                                    noteid = reader.GetGuid(1),
                                    remindertime = reader.GetDateTime(2),
                                    recurrence = reader.GetString(3),
                                    isactive = reader.GetBoolean(4)
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
    public class remindersclass
    {
        public Guid Id { get; set; }
        public Guid noteid { get; set; }
        public DateTime remindertime { get; set; }
        public string recurrence { get; set; }
        public bool isactive { get; set; }
    }
}