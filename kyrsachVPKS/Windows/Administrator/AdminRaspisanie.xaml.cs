using kyrsachVPKS.Classes;
using kyrsachVPKS.Entities;
using kyrsachVPKS.Windows.Users;
using System;
using System.Collections.Generic;
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

namespace kyrsachVPKS.Windows.Administrator
{
    /// <summary>
    /// Логика взаимодействия для AdminRaspisanie.xaml
    /// </summary>
    public partial class AdminRaspisanie : Window
    {
        private UserSession _currentUser;
        public AdminRaspisanie(UserSession user)
        {
            InitializeComponent();
            _currentUser = user;
            LoadSchedule();
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
        public void LoadSchedule()
        {
            using (ITschoolEntities db = new ITschoolEntities())
            {
                var schedule = db.Schedule
                                 .Include(s => s.Courses)
                                 .Include(s => s.Groups)
                                 .OrderBy(s => s.Day_of_the_week)
                                 .ToList();

                var groupedSchedule = schedule.GroupBy(s => s.Day_of_the_week)
                                              .ToDictionary(g => g.Key, g => g
                                                  .Select(s => (dynamic)new
                                                  {
                                                      Начало = s.Start_time,
                                                      Конец = s.End_time,
                                                      Группа = s.Groups.ID,
                                                      Курс = s.Courses.Name,
                                                      Аудитория = s.Lecture_hall
                                                  }).ToList());

                SetDataGridSource(pnRaspisanieDG, groupedSchedule, "Понедельник");
                SetDataGridSource(vtRaspisanieDG, groupedSchedule, "Вторник");
                SetDataGridSource(srRaspisanieDG, groupedSchedule, "Среда");
                SetDataGridSource(chtRaspisanieDG, groupedSchedule, "Четверг");
                SetDataGridSource(ptRaspisanieDG, groupedSchedule, "Пятница");
                SetDataGridSource(sbRaspisanieDG, groupedSchedule, "Суббота");
                SetDataGridSource(vsRaspisanieDG, groupedSchedule, "Воскресенье");
            }
        }

        private void SetDataGridSource(DataGrid dataGrid, Dictionary<string, List<dynamic>> schedule, string day)
        {
            if (schedule.ContainsKey(day))
            {
                dataGrid.ItemsSource = schedule[day];

                // Устанавливаем ширину столбцов
                dataGrid.AutoGeneratingColumn += (s, e) =>
                {
                    if (e.Column.Header.ToString() == "Курс")
                        e.Column.Width = new DataGridLength(1, DataGridLengthUnitType.Star); // Заполняет оставшееся пространство
                    else
                        e.Column.Width = DataGridLength.Auto; // Минимальная ширина по содержимому
                };
            }
            else
            {
                dataGrid.ItemsSource = null;
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
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            AdminRaspisanieAdd s = new AdminRaspisanieAdd(_currentUser, this);
            s.Show();
            this.Hide();
        }
        private void Upd_Click(object sender, RoutedEventArgs e)
        {
            //AdminRaspisanieUpd s = new AdminRaspisanieUpd(_currentUser, this);
            //s.Show();
            //this.Hide();
        }
        private void Del_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
