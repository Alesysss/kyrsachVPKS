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
using static System.Net.Mime.MediaTypeNames;

namespace kyrsachVPKS.Windows.Teacher
{
    /// <summary>
    /// Логика взаимодействия для PrepodGryppi.xaml
    /// </summary>
    public partial class PrepodGryppi : Window
    {
        private UserSession _currentUser;
        public PrepodGryppi(UserSession user)
        {
            InitializeComponent();
            _currentUser = user;
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

                        // Вызов метода для получения данных и привязка к DataGrid
                        GetTeacherGroupsAndCourses(teacher.ID);
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
            }
        }

        private void GetTeacherGroupsAndCourses(long teacherId)
        {
            using (ITschoolEntities db = new ITschoolEntities())
            {
                if (_currentUser == null)
                {
                    MessageBox.Show("Ошибка: пользователь не передан!");
                    return;
                }

                var account = db.Accounts.FirstOrDefault(a => a.ID == _currentUser.Id);

                if (account != null)
                {
                    var teacher = db.Teachers
                        .Where(t => t.ID == teacherId)
                        .FirstOrDefault();

                    if (teacher != null)
                    {
                        // Получаем группы, количество студентов с оплаченной заявкой, возрастную группу и курс
                        var groupsInfo = (from g in db.Groups
                                          join sc in db.Schedule on g.ID equals sc.ID_Groups
                                          join c in db.Courses on sc.ID_Courses equals c.ID
                                          where sc.ID_Teachers == teacher.ID
                                          select new
                                          {
                                              GroupNumber = g.ID,
                                              NumberOfPaidStudents = db.Requests
                                                  .Count(r => r.ID_Courses == c.ID && r.Status == "Оплачено" && r.ID_Students == db.Students
                                                  .Where(s => s.ID == r.ID_Students)
                                                  .Select(s => s.ID).FirstOrDefault()), // подсчет студентов с оплаченной заявкой
                                              AgeGroup = c.Age_group,
                                              CourseName = c.Name
                                          }).Distinct().ToList();

                        // Привязываем данные к DataGrid
                        vtDG.ItemsSource = groupsInfo;
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
        private void ProsmotrGryppi_Click(object sender, RoutedEventArgs e)
        {
            if (vtDG.SelectedItem != null)
            {
                var selectedGroup = vtDG.SelectedItem as dynamic; // Мы не используем модели, поэтому используем dynamic

                long groupId = selectedGroup.GroupNumber; // Получаем ID группы из выбранной строки

                PrepodProsmotrGryppi s = new PrepodProsmotrGryppi(_currentUser, this, groupId);
                s.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите группу!");
            }
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
    }
}
