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

namespace kyrsachVPKS.Windows.Student
{
    /// <summary>
    /// Логика взаимодействия для StudZaiavki.xaml
    /// </summary>
    public partial class StudZaiavki : Window
    {
        private UserSession _currentUser;
        public StudZaiavki(UserSession user)
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

                        // Загружаем заявки студента
                        var requests = db.Requests
                            .Where(r => r.ID_Students == student.ID)
                            .Select(r => new
                            {
                                ID = r.ID,
                                CourseName = r.Courses.Name,
                                Teacher = r.Courses.Teachers.Select(t => t.Surname + " " + t.Name + " " + t.Middle_name)
                                                            .FirstOrDefault(),
                                Status = r.Status
                            })
                            .ToList();

                        ZaiavkiDG.ItemsSource = requests;
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
        private void Profil_Click(object sender, RoutedEventArgs e)
        {
            StudProfil studProfil = new StudProfil(_currentUser);
            studProfil.Show();
            this.Close();
        }
        private void Kyrsi_Click(object sender, RoutedEventArgs e)
        {
            StudKyrsi s = new StudKyrsi(_currentUser);
            s.Show();
            this.Close();
        }
        private void ProsmotrKyrsi_Click(object sender, RoutedEventArgs e)
        {
            var selectedRequest = ZaiavkiDG.SelectedItem;

            if (selectedRequest == null)
            {
                MessageBox.Show("Выберите курс для просмотра!");
                return;
            }

            dynamic request = selectedRequest;
            int requestId = (int)request.ID;

            StudProsmotrKyrsi s = new StudProsmotrKyrsi(_currentUser, this, requestId, true);
            s.Show();
            this.Hide();
        }


        private void ProsmotrPrepod_Click(object sender, RoutedEventArgs e)
        {
            var selectedRequest = ZaiavkiDG.SelectedItem;

            if (selectedRequest == null)
            {
                MessageBox.Show("Выберите преподавателя для просмотра!");
                return;
            }

            dynamic request = selectedRequest;
            int requestId = (int)request.ID;

            StudProsmotrPrepod s = new StudProsmotrPrepod(_currentUser, this, requestId, true);
            s.Show();
            this.Hide();
        }
    }
}
