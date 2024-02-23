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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;
using Microsoft.Data.Sqlite;
using System.Collections.ObjectModel;

namespace SqLiteBeolvas
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Games> dataList = new(); 
        string csvImport, sqlExport;
        SqliteConnection connection;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnSQL_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "db files (*.db)|*.db|sqlite3 (*.sqlite3)|*sqlite3";
            if (sfd.ShowDialog() == true)
            {
                sqlExport = sfd.FileName;
                sqlSource.Text = sqlExport;
                if (sqlSource.Text != "" && csvSource.Text != "")
                {
                    btnConvert.IsEnabled = true;
                }
            }

        }

        private void btnConvert_Click(object sender, RoutedEventArgs e)
        {
            connection = new($"Filename={sqlExport}");
            connection.Open();
            var fileContent = File.ReadAllLines(csvImport);
            string[] parts = fileContent[0].Split(';');
            string commandText = $"CREATE TABLE IF NOT EXISTS games(id INTEGER PRIMARY KEY AUTOINCREMENT, {parts[0]} VARCHAR(100),{parts[1]} DOUBLE, {parts[2]} INT, {parts[3]} VARCHAR(100))";
            SqliteCommand sqliteCommand = new SqliteCommand(commandText, connection);
            sqliteCommand.ExecuteNonQuery();
            foreach (var item in fileContent.Skip(1))
            {
                parts = item.Split(';');
                commandText = $"INSERT INTO games({fileContent[0].Replace(';', ',')}) VALUES ('{parts[0]}','{parts[1]}','{parts[2]}','{parts[3]}')";
                sqliteCommand = new(commandText, connection);
                sqliteCommand.ExecuteNonQuery();
            }
        }

        private void btnCSV_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "csv files (*.csv)|*.csv|text files (*.txt)|*.txt";
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog() == true)
            {
                csvImport = ofd.FileName;
                csvSource.Text = csvImport;
                foreach (var item in File.ReadAllLines(csvImport).Skip(1))
                {
                    string[] adat = item.Split(';');
                    dataList.Add(new Games(adat[0], double.Parse(adat[1]), int.Parse(adat[2]), adat[3]));
                }
                dgSqlite.ItemsSource = dataList;
                if (sqlSource.Text != "" && csvSource.Text != "")
                {
                    btnConvert.IsEnabled = true;
                }
            }
        }
    }
}
