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
    /// Логика взаимодействия для PrepodProsmotrStud.xaml
    /// </summary>
    public partial class PrepodProsmotrStud : Window
    {
        private UserSession _currentUser;
        private Window _nazadWindow;
        private int _requestId;
        public PrepodProsmotrStud(UserSession user, Window nazadWindow, int requestId)
        {
            InitializeComponent();
            _currentUser = user;
            _nazadWindow = nazadWindow;
            _requestId = requestId;
            ZapolnitDannyeStud();
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
            }
        }
        private void ZapolnitDannyeStud()
        {
            using (ITschoolEntities db = new ITschoolEntities())
            {
                // Получаем информацию о студенте из заявки
                var request = db.Requests
                                .Where(r => r.ID == _requestId) // Используем _requestId
                                .Select(r => new
                                {
                                    StudentId = r.ID_Students
                                })
                                .FirstOrDefault();

                if (request != null)
                {
                    // Ищем студента по ID
                    var student = db.Students.FirstOrDefault(s => s.ID == request.StudentId);

                    if (student != null)
                    {
                        // Заполняем данные о студенте
                        FIOstud.Text = $"{student.Surname} {student.Name} {student.Middle_name}";
                        Em.Text = student.Email;
                        Tel.Text = student.Phone_number;
                        DatR.Text = student.Date_of_birth.ToString("d");
                    }
                    else
                    {
                        MessageBox.Show("Студент не найден в базе!");
                    }
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
