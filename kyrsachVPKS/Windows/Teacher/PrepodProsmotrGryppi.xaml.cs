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

namespace kyrsachVPKS.Windows.Teacher
{
    /// <summary>
    /// Логика взаимодействия для PrepodProsmotrGryppi.xaml
    /// </summary>
    public partial class PrepodProsmotrGryppi : Window
    {
        private UserSession _currentUser;
        private Window _nazadWindow;
        private long _groupId;
        public PrepodProsmotrGryppi(UserSession user, Window nazadWindow, long groupId)
        {
            InitializeComponent();
            _currentUser = user;
            _nazadWindow = nazadWindow;
            _groupId = groupId;
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
                    var teacher = db.Teachers.FirstOrDefault(t => t.ID == account.ID_Teachers);

                    if (teacher != null)
                    {
                        FIO.Text = $"{teacher.Surname} {teacher.Name} {teacher.Middle_name}";
                    }
                    else
                    {
                        MessageBox.Show("Преподаватель не найден в базе!");
                    }
                }
                else
                {
                    MessageBox.Show("Аккаунт не найден в базе!");
                }

                var studentsInGroup = (from g in db.Groups
                                       join sc in db.Schedule on g.ID equals sc.ID_Groups
                                       join r in db.Requests on sc.ID_Courses equals r.ID_Courses
                                       join s in db.Students on r.ID_Students equals s.ID
                                       where g.ID == _groupId && r.Status == "Оплачено"
                                       select new
                                       {
                                           StudentName = s.Surname + " " + s.Name + " " + s.Middle_name,
                                           DateOfBirth = s.Date_of_birth
                                       }).Distinct().ToList();

                var studentsInfo = studentsInGroup.Select((s, index) => new
                {
                    GroupNumber = index + 1,
                    FullName = s.StudentName,
                    Age = (DateTime.Now.Year - s.DateOfBirth.Year) - (DateTime.Now.DayOfYear < s.DateOfBirth.DayOfYear ? 1 : 0)
                }).ToList();

                GryppaDG.ItemsSource = studentsInfo;
            }
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
        private void Nazad_Click(object sender, RoutedEventArgs e)
        {
            _nazadWindow.Show();
            this.Close();
        }
        private void ProsmotrStud_Click(object sender, RoutedEventArgs e)
        {
            //PrepodProsmotrStud s = new PrepodProsmotrStud(_currentUser, this);
            //s.Show();
            //this.Hide();
        }
        private void Profil_Click(object sender, RoutedEventArgs e)
        {
            PrepodProfil s = new PrepodProfil(_currentUser);
            s.Show();
            this.Close();
        }
        private void Zaiavki_Click(object sender, RoutedEventArgs e)
        {
            PrepodZaiavki s = new PrepodZaiavki(_currentUser);
            s.Show();
            this.Close();
        }
        private void Raspisanie_Click(object sender, RoutedEventArgs e)
        {
            PrepodRaspisanie s = new PrepodRaspisanie(_currentUser);
            s.Show();
            this.Close();
        }
        private void Kyrsi_Click(object sender, RoutedEventArgs e)
        {
            PrepodKyrsi s = new PrepodKyrsi(_currentUser);
            s.Show();
            this.Close();
        }
        private void Gryppi_Click(object sender, RoutedEventArgs e)
        {
            PrepodGryppi s = new PrepodGryppi(_currentUser);
            s.Show();
            this.Close();
        }
    }
}
