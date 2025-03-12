using kyrsachVPKS.Classes;
using kyrsachVPKS.Entities;
using kyrsachVPKS.Windows.Users;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
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
    /// Логика взаимодействия для PrepodRaspisanie.xaml
    /// </summary>
    public partial class PrepodRaspisanie : Window
    {
        private UserSession _currentUser;
        public PrepodRaspisanie(UserSession user)
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
                        LoadScheduleForTeacher((int)teacher.ID);
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
        public void LoadScheduleForTeacher(int teacherId)
        {
            using (ITschoolEntities db = new ITschoolEntities())
            {
                // Загружаем расписание с курсами и группами для преподавателя
                var schedule = db.Schedule
                                 .Where(s => s.ID_Teachers == teacherId)
                                 .Include(s => s.Courses)  // Загружаем связанные курсы
                                 .Include(s => s.Groups)   // Загружаем связанные группы
                                 .OrderBy(s => s.Day_of_the_week)
                                 .ToList();

                // Разделяем расписание по дням недели
                var mondaySchedule = schedule.Where(s => s.Day_of_the_week == "Понедельник")
                                              .Select(s => new
                                              {
                                                  Start_time = s.Start_time,
                                                  End_time = s.End_time,
                                                  ID_Groups = s.ID_Groups,
                                                  CourseName = s.Courses.Name,      
                                                  Lecture_hall = s.Lecture_hall
                                              }).ToList();

                var tuesdaySchedule = schedule.Where(s => s.Day_of_the_week == "Вторник")
                                              .Select(s => new
                                              {
                                                  Start_time = s.Start_time,
                                                  End_time = s.End_time,
                                                  ID_Groups = s.ID_Groups,
                                                  CourseName = s.Courses.Name,
                                                  Lecture_hall = s.Lecture_hall
                                              }).ToList();

                var wednesdaySchedule = schedule.Where(s => s.Day_of_the_week == "Среда")
                                                .Select(s => new
                                                {
                                                    Start_time = s.Start_time,
                                                    End_time = s.End_time,
                                                    ID_Groups = s.ID_Groups,
                                                    CourseName = s.Courses.Name,
                                                    Lecture_hall = s.Lecture_hall
                                                }).ToList();

                var thursdaySchedule = schedule.Where(s => s.Day_of_the_week == "Четверг")
                                               .Select(s => new
                                               {
                                                   Start_time = s.Start_time,
                                                   End_time = s.End_time,
                                                   ID_Groups = s.ID_Groups,
                                                   CourseName = s.Courses.Name,
                                                   Lecture_hall = s.Lecture_hall
                                               }).ToList();

                var fridaySchedule = schedule.Where(s => s.Day_of_the_week == "Пятница")
                                             .Select(s => new
                                             {
                                                 Start_time = s.Start_time,
                                                 End_time = s.End_time,
                                                 ID_Groups = s.ID_Groups,
                                                 CourseName = s.Courses.Name,
                                                 Lecture_hall = s.Lecture_hall
                                             }).ToList();

                var saturdaySchedule = schedule.Where(s => s.Day_of_the_week == "Суббота")
                                               .Select(s => new
                                               {
                                                   Start_time = s.Start_time,
                                                   End_time = s.End_time,
                                                   ID_Groups = s.ID_Groups,
                                                   CourseName = s.Courses.Name,
                                                   Lecture_hall = s.Lecture_hall
                                               }).ToList();

                var sundaySchedule = schedule.Where(s => s.Day_of_the_week == "Воскресенье")
                                             .Select(s => new
                                             {
                                                 Start_time = s.Start_time,
                                                 End_time = s.End_time,
                                                 ID_Groups = s.ID_Groups,
                                                 CourseName = s.Courses.Name,
                                                 Lecture_hall = s.Lecture_hall
                                             }).ToList();

                // Привязываем данные к соответствующим DataGrid
                pnRaspisanieDG.ItemsSource = mondaySchedule;
                vtRaspisanieDG.ItemsSource = tuesdaySchedule;
                srRaspisanieDG.ItemsSource = wednesdaySchedule;
                chtRaspisanieDG.ItemsSource = thursdaySchedule;
                ptRaspisanieDG.ItemsSource = fridaySchedule;
                sbRaspisanieDG.ItemsSource = saturdaySchedule;
                vsRaspisanieDG.ItemsSource = sundaySchedule;
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
