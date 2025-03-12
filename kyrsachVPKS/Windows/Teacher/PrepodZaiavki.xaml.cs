using kyrsachVPKS.Classes;
using kyrsachVPKS.Entities;
using kyrsachVPKS.Windows.Student;
using kyrsachVPKS.Windows.Users;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Логика взаимодействия для PrepodZaiavki.xaml
    /// </summary>
    public partial class PrepodZaiavki : Window
    {
        private UserSession _currentUser;
        public PrepodZaiavki(UserSession user)
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

                        var requests = db.Requests
                            .Where(r => r.Courses.Teachers.Any(t => t.ID == teacher.ID))
                            .Select(r => new
                            {
                                ID = r.ID,
                                CourseName = r.Courses.Name,
                                StudentFIO = r.Students.Surname + " " + r.Students.Name + " " + r.Students.Middle_name,
                                Status = r.Status
                            })
                            .ToList();

                        // Проверим, получаем ли мы данные
                        if (requests.Any())
                        {
                            ZaiavkiDG.ItemsSource = requests;
                        }
                        else
                        {
                            MessageBox.Show("Нет заявок с таким статусом!");
                        }
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
        private void Profil_Click(object sender, RoutedEventArgs e)
        {
            PrepodProfil s = new PrepodProfil(_currentUser);
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
        private void ProsmotrStud_Click(object sender, RoutedEventArgs e)
        {
            var selectedRequest = ZaiavkiDG.SelectedItem;

            if (selectedRequest == null)
            {
                MessageBox.Show("Выберите заявку, чтобы посмотреть информацию о студенте!");
                return;
            }

            var request = (dynamic)selectedRequest;
            int studentId = Convert.ToInt32(request.ID);

            // Передаем studentId в конструктор, а не requestId
            PrepodProsmotrStud s = new PrepodProsmotrStud(_currentUser, this, studentId);
            s.Show();
            this.Hide();
        }




    }
}
