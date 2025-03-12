using kyrsachVPKS.Classes;
using kyrsachVPKS.Entities;
using kyrsachVPKS.Windows.Administrator;
using kyrsachVPKS.Windows.Student;
using kyrsachVPKS.Windows.Teacher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using static System.Net.Mime.MediaTypeNames;

namespace kyrsachVPKS.Windows.Users
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class Avtorizacia : Window
    {
        public Avtorizacia()
        {
            InitializeComponent();
        }
        private void Register_Click(object sender, RoutedEventArgs e)
        {
            Registracia registracia = new Registracia();
            registracia.Show();
            this.Close(); 
        }
        private bool ValidateFields()
        {
            Regex loginRegex = new Regex(@"^[A-Za-z0-9]{1,20}$");
            Regex passwordRegex = new Regex(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{1,20}$");

            void ShowError(Control field, string message)
            {
                MessageBox.Show(message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                field.Focus();
            }
            if (!loginRegex.IsMatch(LogTB.Text)) { ShowError(LogTB, "Ошибка в логине."); return false; }
            if (!passwordRegex.IsMatch(PassTB.Text))
            { ShowError(PassTB, "Ошибка в пароле."); return false; }

            return true;
        }
        private void Avtoriz_Click(object sender, RoutedEventArgs e)
        {
            string login = LogTB.Text;
            string password = PassTB.Text;
            if (!ValidateFields()) return;
            using (ITschoolEntities db = new ITschoolEntities())
            {
                var user = db.Accounts.FirstOrDefault(a => a.Login == login && a.Password == password);

                if (user != null)
                {
                    UserSession currentUser = new UserSession
                    {
                        Id = (int)user.ID,
                        Login = user.Login,
                        Role = user.Role
                    };

                    OpenUserWindow(currentUser);
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль!");
                }
            }
        }

        private void OpenUserWindow(UserSession user)
        {
            Window window = null;

            switch (user.Role)
            {
                case "Администратор":
                    window = new AdminProfil(user);
                    break;
                case "Преподаватель":
                    window = new PrepodProfil(user);
                    break;
                case "Студент":
                    window = new StudProfil(user);
                    break;
                default:
                    MessageBox.Show("Неизвестная роль!");
                    return;
            }

            window.Show();
            this.Close();
        }





    }
}
