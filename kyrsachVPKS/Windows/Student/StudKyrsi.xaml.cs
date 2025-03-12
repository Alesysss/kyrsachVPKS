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
    /// Логика взаимодействия для StudKyrsi.xaml
    /// </summary>
    public partial class StudKyrsi : Window
    {
        private UserSession _currentUser;
        public StudKyrsi(UserSession user)
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
                    var student = db.Students.FirstOrDefault(s => s.ID == account.ID_Students);

                    if (student != null)
                    {
                        FIO.Text = $"{student.Surname} {student.Name} {student.Middle_name}";

                        // Получаем все курсы со статусом "Набор студентов"
                        var allCourses = db.Courses
                            .Where(c => c.Status == "Набор студентов")
                            .ToList();

                        // Получаем ID курсов, на которые студент уже подал заявки
                        var requestedCourseIds = db.Requests
                            .Where(r => r.ID_Students == student.ID)
                            .Select(r => r.ID_Courses)
                            .ToHashSet();

                        // Фильтруем курсы: оставляем только те, на которые студент не подавал заявку
                        var availableCourses = allCourses
                            .Where(c => !requestedCourseIds.Contains(c.ID))
                            .Select(c => new
                            {
                                ID = c.ID,
                                CourseName = c.Name,
                                Teacher = c.Teachers
                                    .Select(t => t.Surname + " " + t.Name + " " + t.Middle_name)
                                    .FirstOrDefault()
                            })
                            .ToList();

                        KyrsiiDG.ItemsSource = availableCourses;
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
        private void ProsmotrKyrsi_Click(object sender, RoutedEventArgs e)
        {
            var selectedCourse = KyrsiiDG.SelectedItem;

            if (selectedCourse == null)
            {
                MessageBox.Show("Выберите курс для просмотра!");
                return;
            }

            var course = (dynamic)selectedCourse;
            int courseId = Convert.ToInt32(course.ID);

            StudProsmotrKyrsi s = new StudProsmotrKyrsi(_currentUser, this, courseId, false);
            s.Show();
            this.Hide();
        }

        private void ProsmotrPrepod_Click(object sender, RoutedEventArgs e)
        {
            var selectedCourse = KyrsiiDG.SelectedItem;

            if (selectedCourse == null)
            {
                MessageBox.Show("Выберите курс, чтобы посмотреть преподавателя!");
                return;
            }

            var course = (dynamic)selectedCourse;
            int courseId = Convert.ToInt32(course.ID);

            using (ITschoolEntities db = new ITschoolEntities())
            {
                var teacher = db.Courses
                    .Where(c => c.ID == courseId)
                    .SelectMany(c => c.Teachers)
                    .Select(t => new
                    {
                        t.ID,
                        t.Surname,
                        t.Name,
                        t.Middle_name
                    })
                    .FirstOrDefault();

                if (teacher == null)
                {
                    MessageBox.Show("Преподаватель не найден!");
                    return;
                }

                int teacherId = Convert.ToInt32(teacher.ID);

                // Передаем false, так как это окно всех курсов
                StudProsmotrPrepod s = new StudProsmotrPrepod(_currentUser, this, teacherId, false);
                s.Show();
                this.Hide();
            }
        }




    }
}
