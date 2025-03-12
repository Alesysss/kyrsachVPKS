using kyrsachVPKS.Classes;
using kyrsachVPKS.Entities;
using kyrsachVPKS.Windows.Student;
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
    /// Логика взаимодействия для PrepodProfilZaiavki.xaml
    /// </summary>
    public partial class PrepodProfilZaiavki : Window
    {
        private UserSession _currentUser;
        public PrepodProfilZaiavki(UserSession user)
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
                        FIO2.Text = $"{teacher.Surname} {teacher.Name} {teacher.Middle_name}";
                        Email1.Text = teacher.Email;
                        Tel1.Text = teacher.Phone_number;
                        DatR1.Text = $"{teacher.Date_of_birth:dd.MM.yyyy}";

                        var requests = db.Requests
                            .Where(r => r.Courses.Teachers.Any(t => t.ID == teacher.ID) && r.Status == "На рассмотрении")
                            .OrderBy(r => r.Date_of_creation)
                            .Select(r => new
                            {
                                ID = r.ID,
                                CourseName = r.Courses.Name,
                                StudentFIO = r.Students.Surname + " " + r.Students.Name + " " + r.Students.Middle_name,
                                Status = r.Status
                            })
                            .ToList();

                        ZaiavkiDG.ItemsSource = requests;
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
        private void Dannie_Click(object sender, RoutedEventArgs e)
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
        private void ProsmotrStud_Click(object sender, RoutedEventArgs e)
        {
            //PrepodProsmotrStud s = new PrepodProsmotrStud(_currentUser, this);
            //s.Show();
            //this.Hide();
        }
    }
}
