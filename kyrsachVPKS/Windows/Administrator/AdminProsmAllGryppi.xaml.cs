using kyrsachVPKS.Classes;
using kyrsachVPKS.Windows.Users;
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

namespace kyrsachVPKS.Windows.Administrator
{
    /// <summary>
    /// Логика взаимодействия для AdminProsmAllGryppi.xaml
    /// </summary>
    public partial class AdminProsmAllGryppi : Window
    {
        private UserSession _currentUser;
        private Window _nazadWindow;
        public AdminProsmAllGryppi(UserSession user, Window nazadWindow)
        {
            InitializeComponent();
            _currentUser = user;
            _nazadWindow = nazadWindow;
        }
        private void Nazad_Click(object sender, RoutedEventArgs e)
        {
            _nazadWindow.Show();
            this.Close();
        }
        private void Avtorizacia_Click(object sender, RoutedEventArgs e)
        {
            Avtorizacia avtorizacia = new Avtorizacia();
            avtorizacia.Show();
            this.Close();
        }
        private void Yvedoml_Click(object sender, RoutedEventArgs e)
        {
            //Avtorizacia avtorizacia = new Avtorizacia();
            //avtorizacia.Show();
            //this.Close();
        }
        private void Profil_Click(object sender, RoutedEventArgs e)
        {
            AdminProfil s = new AdminProfil(_currentUser);
            s.Show();
            this.Close();
        }
        private void Zaiavki_Click(object sender, RoutedEventArgs e)
        {
            AdminZaiavki s = new AdminZaiavki(_currentUser);
            s.Show();
            this.Close();
        }
        private void Raspisanie_Click(object sender, RoutedEventArgs e)
        {
            AdminRaspisanie s = new AdminRaspisanie(_currentUser);
            s.Show();
            this.Close();
        }
        private void Kyrsi_Click(object sender, RoutedEventArgs e)
        {
            AdminKyrsi s = new AdminKyrsi(_currentUser);
            s.Show();
            this.Close();
        }
        private void Prepodi_Click(object sender, RoutedEventArgs e)
        {
            AdminPrepodi s = new AdminPrepodi(_currentUser);
            s.Show();
            this.Close();
        }
        private void Gryppi_Click(object sender, RoutedEventArgs e)
        {
            AdminGryppi s = new AdminGryppi(_currentUser);
            s.Show();
            this.Close();
        }
        private void Admin_Click(object sender, RoutedEventArgs e)
        {
            AdminAdmin s = new AdminAdmin(_currentUser);
            s.Show();
            this.Close();
        }
        private void Dob_Click(object sender, RoutedEventArgs e)
        {
            AdminDob s = new AdminDob(_currentUser);
            s.Show();
            this.Close();
        }

        private void ProsmotrGryppi_Click(object sender, RoutedEventArgs e)
        {
            AdminProsmGryppi s = new AdminProsmGryppi(_currentUser, this);
            s.Show();
            this.Hide();
        }
    }
}
