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

namespace kyrsachVPKS.Windows.Student
{
    /// <summary>
    /// Логика взаимодействия для StudProsmotrKyrsi.xaml
    /// </summary>
    public partial class StudProsmotrKyrsi : Window
    {
        private UserSession _currentUser;
        private Window _nazadWindow;
        private int _requestId;
        private bool _isFromRequests;
        public StudProsmotrKyrsi(UserSession user, Window nazadWindow, int requestId, bool isFromRequests)
        {
            InitializeComponent();
            _currentUser = user;
            _nazadWindow = nazadWindow;
            _requestId = requestId;
            _isFromRequests = isFromRequests;
            ZapolnitDannyeKursa();
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
                    var student = db.Students.FirstOrDefault(s => s.ID == account.ID_Students);

                    if (student != null)
                    {
                        FIO.Text = $"{student.Surname} {student.Name} {student.Middle_name}";

                        
                    }
                    else
                    {
                        MessageBox.Show("Студент не найден в базе!");
                    }
                }
                else
                {
                    MessageBox.Show("Аккаунт не найден в базе!");
                }
            }
        }

        private void ZapolnitDannyeKursa()
        {
            using (ITschoolEntities db = new ITschoolEntities())
            {
                if (_isFromRequests)
                {
                    var request = db.Requests
                        .Where(r => r.ID == _requestId)
                        .Select(r => new
                        {
                            CourseName = r.Courses.Name,
                            AgeGroup = r.Courses.Age_group,
                            LessonCount = r.Courses.Number_of_classes,
                            Description = r.Courses.Description
                        })
                        .FirstOrDefault();

                    if (request != null)
                    {
                        KursNazvanie.Text = request.CourseName;
                        KursVozrast.Text = request.AgeGroup;
                        KursZanyatiya.Text = request.LessonCount.ToString();
                        KursOpisanie.Text = request.Description;
                        return;
                    }
                }
                else
                {
                    var course = db.Courses
                        .Where(c => c.ID == _requestId)
                        .Select(c => new
                        {
                            CourseName = c.Name,
                            AgeGroup = c.Age_group,
                            LessonCount = c.Number_of_classes,
                            Description = c.Description
                        })
                        .FirstOrDefault();

                    if (course != null)
                    {
                        KursNazvanie.Text = course.CourseName;
                        KursVozrast.Text = course.AgeGroup;
                        KursZanyatiya.Text = course.LessonCount.ToString();
                        KursOpisanie.Text = course.Description;
                        return;
                    }
                }

                MessageBox.Show("Ошибка: данные о курсе не найдены!");
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
        private void Zaiavki_Click(object sender, RoutedEventArgs e)
        {
            StudZaiavki studZaiavki = new StudZaiavki(_currentUser);
            studZaiavki.Show();
            this.Close();
        }
        private void Profil_Click(object sender, RoutedEventArgs e)
        {
            StudProfil s = new StudProfil(_currentUser);
            s.Show();
            this.Close();
        }
        private void Kyrsi_Click(object sender, RoutedEventArgs e)
        {
            StudKyrsi s = new StudKyrsi(_currentUser);
            s.Show();
            this.Close();
        }
    }
}
