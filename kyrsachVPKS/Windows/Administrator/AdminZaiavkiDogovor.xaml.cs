using kyrsachVPKS.Classes;
using kyrsachVPKS.Entities;
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
    /// Логика взаимодействия для AdminZaiavkiDogovor.xaml
    /// </summary>
    public partial class AdminZaiavkiDogovor : Window
    {
        private UserSession _currentUser;        
        private Window _nazadWindow;
        private int _requestId;
        public AdminZaiavkiDogovor(UserSession user, Window nazadWindow, int requestId)
        {
            InitializeComponent();
             _currentUser = user;
            _nazadWindow = nazadWindow;
            _requestId = requestId;
            ZapolnitDannye();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (_currentUser == null)
            {
                MessageBox.Show("Ошибка: пользователь не передан!");
                return;
            }

            using (ITschoolEntities db = new ITschoolEntities())
            {
                var account = db.Accounts.FirstOrDefault(a => a.ID == _currentUser.Id);

                if (account != null)
                {
                    var admin = db.Administrators.FirstOrDefault(t => t.ID == account.ID_Administrators);

                    if (admin != null)
                    {
                        FIO.Text = $"{admin.Surname} {admin.Name} {admin.Middle_name}";                       
                    }
                    else
                    {
                        MessageBox.Show("Администратор не найден в базе!");
                    }
                }
                else
                {
                    MessageBox.Show("Аккаунт не найден в базе!");
                }
            }
        }
        private void ZapolnitDannye()
        {
            using (ITschoolEntities db = new ITschoolEntities())
            {
                // Получаем данные по заявке
                var request = db.Requests
                                .Where(r => r.ID == _requestId)
                                .Select(r => new
                                {
                                    ContractNumber = r.Contract, // Договор
                                    r.Status, // Статус
                                    StudentFIO = db.Students.Where(s => s.ID == r.ID_Students)
                                                            .Select(s => s.Surname + " " + s.Name + " " + s.Middle_name)
                                                            .FirstOrDefault(),
                                    CourseName = db.Courses.Where(c => c.ID == r.ID_Courses)
                                                           .Select(c => c.Name)
                                                           .FirstOrDefault()
                                })
                                .FirstOrDefault();

                if (request != null)
                {
                    // Заполняем данные
                    Nom.Text = request.ContractNumber.Trim(); 
                    Status.Text = request.Status;
                    Kyrs.Text = request.CourseName ?? "Курс не найден";
                    FIOstud.Text = request.StudentFIO ?? "Студент не найден";
                }
                else
                {
                    MessageBox.Show("Заявка не найдена!");
                }
            }
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
        private void Dannie_Click(object sender, RoutedEventArgs e)
        {
            AdminProfil s = new AdminProfil(_currentUser);
            s.Show();
            this.Close();
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
    }
}
