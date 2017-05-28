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
using System.Data.SqlClient;

namespace DatabaseInfo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btn_Build_Click(object sender, RoutedEventArgs e)
        {
            SqlConnectionStringBuilder cs = BuildConnection();
            MessageBox.Show(cs.ToString());
            //txtConnStr
        }

        private SqlConnectionStringBuilder BuildConnection()
        {
            var server = txtServer.Text;
            var db = txtDBName.Text;
            var login = txtLoginName.Text;
            var ignoto = txtPass.Password;
            SqlConnectionStringBuilder cs = DBUtilities.StringConnection(server, db, login, ignoto);
            return cs;
        }

        private void btn_DBINfo_Click(object sender, RoutedEventArgs e)
        {
            DBUtilities util = new DBUtilities(); 
            var server = txtServer.Text;
            var db = txtDBName.Text;
            var login = txtLoginName.Text;
            var ignoto = txtPass.Password;
            SqlConnectionStringBuilder cs = DBUtilities.StringConnection(server, db, login, ignoto);
            
            List<string> dbInfo = util.ListDB(cs);
            listView.ItemsSource = dbInfo;

        }

        
    }
}
