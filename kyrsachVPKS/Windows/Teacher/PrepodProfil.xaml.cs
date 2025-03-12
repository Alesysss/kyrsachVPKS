using kyrsachVPKS.Classes;
using kyrsachVPKS.Entities;
using kyrsachVPKS.Windows.Student;
using kyrsachVPKS.Windows.Users;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для PrepodProfil.xaml
    /// </summary>
    public partial class PrepodProfil : Window
    {
        private UserSession _currentUser;
        public PrepodProfil(UserSession user)
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
                // Получаем аккаунт, соответствующий текущему пользователю
                var account = db.Accounts.FirstOrDefault(a => a.ID == _currentUser.Id);

                if (account != null)
                {
                    // Получаем студента по ID из столбца ID_Students в таблице Accounts
                    var teachers = db.Teachers.FirstOrDefault(s => s.ID == account.ID_Teachers);
                    var doc_guardians = db.Documents.FirstOrDefault(s => s.ID == teachers.ID_Documents);
                    var regAdr_guardians = db.RegistrationAddress.FirstOrDefault(s => s.ID ==
                                                                    teachers.ID_RegistrationAddress);
                    var resAdr_guardians = db.ResidenceAddress.FirstOrDefault(s => s.ID ==
                                                                    teachers.ID_ResidenceAddress);
                    
                    if (teachers != null)
                    {
                        FIO.Text = $"{teachers.Surname} {teachers.Name} {teachers.Middle_name}";
                        FIO2.Text = $"{teachers.Surname} {teachers.Name} {teachers.Middle_name}";
                        Email1.Text = $"{teachers.Email}";
                        Tel1.Text = $"{teachers.Phone_number}";
                        DatR1.Text = $"{teachers.Date_of_birth:dd.MM.yyyy}";

                        EmTB.Text = $"{teachers.Email}";
                        TelTB.Text = $"{teachers.Phone_number}";
                        PassTB.Text = $"{account.Password}";
                        Pass2TB.Text = $"{account.Password}";
                        LogTB.Text = $"{account.Login}";
                        DateKarStTB.Text = $"{teachers.Date_career_start:dd.MM.yyyy}";

                        opImiaTB.Text = $"{teachers.Name}";
                        opFamTB.Text = $"{teachers.Surname}";
                        opOtchTB.Text = $"{teachers.Middle_name}";
                        opDataRozTB.Text = $"{teachers.Date_of_birth:dd.MM.yyyy}";
                        //ПОЛ

                        opSerTB.Text = $"{doc_guardians.Passport_series}";
                        opNomTB.Text = $"{doc_guardians.Passport_number}";
                        opDataVidTB.Text = $"{doc_guardians.Date_of_issue:dd.MM.yyyy}";
                        opVidanTB.Text = $"{doc_guardians.Issued}";
                        opKodPodrTB.Text = $"{doc_guardians.Unit_code}";
                        opINNTB.Text = $"{doc_guardians.INN}";
                        opSNILSTB.Text = $"{doc_guardians.SNILS}";

                        opStranaTB.Text = $"{regAdr_guardians.Country}";
                        opRegTB.Text = $"{regAdr_guardians.Region}";
                        opGorodTB.Text = $"{regAdr_guardians.City}";
                        opRaionTB.Text = $"{regAdr_guardians.District}";
                        opYlTB.Text = $"{regAdr_guardians.Street}";
                        opDomTB.Text = $"{regAdr_guardians.House}";
                        opPodTB.Text = $"{regAdr_guardians.Entrance}";
                        opKvTB.Text = $"{regAdr_guardians.Flat}";

                        opFaktStranaTB.Text = $"{resAdr_guardians.Country}";
                        opFaktRegTB.Text = $"{resAdr_guardians.Region}";
                        opFaktGorodTB.Text = $"{resAdr_guardians.City}";
                        opFaktRaionTB.Text = $"{resAdr_guardians.District}";
                        opFaktYlTB.Text = $"{resAdr_guardians.Street}";
                        opFaktDomTB.Text = $"{resAdr_guardians.House}";
                        opFaktPodTB.Text = $"{resAdr_guardians.Entrance}";
                        opFaktKvTB.Text = $"{resAdr_guardians.Flat}";                      

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
        private bool ValidateFields()
        {
            Regex nameRegex = new Regex(@"^[А-ЯЁ][а-яё]{2,14}$");
            Regex optionalPatronymicRegex = new Regex(@"^[А-ЯЁ][а-яё]{2,14}$");
            Regex phoneRegex = new Regex(@"^8\d{10}$");
            Regex passportSeriesRegex = new Regex(@"^\d{4}$");
            Regex passportNumberRegex = new Regex(@"^\d{6}$");
            Regex passportIssuedByRegex = new Regex(@"[А-ЯЁ][а-яё\s\-]{3,29}$");
            Regex unitCodeRegex = new Regex(@"^\d{3}-\d{3}$");
            Regex innRegex = new Regex(@"^\d{12}$");
            Regex snilsRegex = new Regex(@"^\d{3}-\d{3}-\d{3} \d{2}$");

            Regex addressRegex = new Regex(@"^[А-ЯЁ][а-яё]{4,29}$");
            Regex streetRegex = new Regex(@"^[А-ЯЁа-яё0-9\s,-]{5,50}$");
            Regex numberRegex = new Regex(@"^\d{1,7}$");

            Regex emailRegex = new Regex(@"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$");
            Regex loginRegex = new Regex(@"^[A-Za-z0-9]{6,20}$");
            Regex passwordRegex = new Regex(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,20}$");

            void ShowError(Control field, string message)
            {
                MessageBox.Show(message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                field.Focus();
            }


            if (!phoneRegex.IsMatch(TelTB.Text)) { ShowError(TelTB, "Ошибка в номере телефона."); return false; }

            if (!emailRegex.IsMatch(EmTB.Text)) { ShowError(EmTB, "Ошибка в email."); return false; }
            if (!loginRegex.IsMatch(LogTB.Text)) { ShowError(LogTB, "Ошибка в логине."); return false; }
            if (!passwordRegex.IsMatch(PassTB.Text) || PassTB.Text != Pass2TB.Text)
            { ShowError(PassTB, "Ошибка в пароле. Пароли должны совпадать."); return false; }

            // 🔹 Проверка родителя
            if (!nameRegex.IsMatch(opFamTB.Text)) { ShowError(opFamTB, "Ошибка в фамилии."); return false; }
            if (!nameRegex.IsMatch(opImiaTB.Text)) { ShowError(opImiaTB, "Ошибка в имени."); return false; }
            if (opOtchTB.Text.Length > 0 && !optionalPatronymicRegex.IsMatch(opOtchTB.Text))
            { ShowError(opOtchTB, "Ошибка в отчестве."); return false; }

            if (!passportSeriesRegex.IsMatch(opSerTB.Text)) { ShowError(opSerTB, "Ошибка в серии паспорта."); return false; }
            if (!passportNumberRegex.IsMatch(opNomTB.Text)) { ShowError(opNomTB, "Ошибка в номере паспорта."); return false; }
            if (!passportIssuedByRegex.IsMatch(opVidanTB.Text)) { ShowError(opVidanTB, "Ошибка в поле 'Кем выдан'."); return false; }
            if (!unitCodeRegex.IsMatch(opKodPodrTB.Text)) { ShowError(opKodPodrTB, "Ошибка в коде подразделения."); return false; }
            if (!innRegex.IsMatch(opINNTB.Text)) { ShowError(opINNTB, "Ошибка в ИНН."); return false; }
            if (!snilsRegex.IsMatch(opSNILSTB.Text)) { ShowError(opSNILSTB, "Ошибка в СНИЛС."); return false; }

            if (!addressRegex.IsMatch(opStranaTB.Text)) { ShowError(opStranaTB, "Ошибка в стране."); return false; }
            if (!addressRegex.IsMatch(opGorodTB.Text)) { ShowError(opGorodTB, "Ошибка в городе."); return false; }
            if (!streetRegex.IsMatch(opYlTB.Text)) { ShowError(opYlTB, "Ошибка в улице."); return false; }
            if (!numberRegex.IsMatch(opDomTB.Text)) { ShowError(opDomTB, "Ошибка в номере дома."); return false; }
            if (opPodTB.Text.Length > 0 && !numberRegex.IsMatch(opPodTB.Text)) { ShowError(opPodTB, "Ошибка в номере подъезда."); return false; }
            if (!numberRegex.IsMatch(opKvTB.Text)) { ShowError(opKvTB, "Ошибка в номере квартиры."); return false; }

            return true;
        }
        private void Upd_Click(object sender, RoutedEventArgs e)
        {

            if (_currentUser == null)
            {
                MessageBox.Show("Ошибка: пользователь не передан!");
                return;
            }

            using (ITschoolEntities db = new ITschoolEntities())
            {
                var account = db.Accounts.FirstOrDefault(a => a.ID == _currentUser.Id);
                var teachers = db.Teachers.FirstOrDefault(s => s.ID == account.ID_Teachers);
                var doc_guardians = db.Documents.FirstOrDefault(s => s.ID == teachers.ID_Documents);
                var regAdr_guardians = db.RegistrationAddress.FirstOrDefault(s => s.ID ==
                                                                teachers.ID_RegistrationAddress);
                var resAdr_guardians = db.ResidenceAddress.FirstOrDefault(s => s.ID ==
                                                                teachers.ID_ResidenceAddress);
                if (account != null)
                {


                    if (teachers != null)
                    {
                        if (string.IsNullOrWhiteSpace(EmTB.Text) || string.IsNullOrWhiteSpace(TelTB.Text) ||
                            string.IsNullOrWhiteSpace(opImiaTB.Text) || string.IsNullOrWhiteSpace(opFamTB.Text) ||
                            string.IsNullOrWhiteSpace(PassTB.Text) ||
                            string.IsNullOrWhiteSpace(Pass2TB.Text) || string.IsNullOrWhiteSpace(LogTB.Text) ||
                            string.IsNullOrWhiteSpace(Pass2TB.Text) ||

                            string.IsNullOrWhiteSpace(opDataRozTB.Text) ||
                            string.IsNullOrWhiteSpace(opSerTB.Text) || string.IsNullOrWhiteSpace(opNomTB.Text) ||
                            string.IsNullOrWhiteSpace(opDataVidTB.Text) || string.IsNullOrWhiteSpace(opVidanTB.Text) ||
                            string.IsNullOrWhiteSpace(opKodPodrTB.Text) || string.IsNullOrWhiteSpace(opINNTB.Text) ||
                            string.IsNullOrWhiteSpace(DateKarStTB.Text) ||

                            string.IsNullOrWhiteSpace(opStranaTB.Text) ||
                            string.IsNullOrWhiteSpace(opGorodTB.Text) || string.IsNullOrWhiteSpace(opYlTB.Text) ||
                            string.IsNullOrWhiteSpace(opDomTB.Text) ||
                            string.IsNullOrWhiteSpace(opKvTB.Text) ||

                            string.IsNullOrWhiteSpace(opFaktStranaTB.Text) ||
                            string.IsNullOrWhiteSpace(opFaktGorodTB.Text) || string.IsNullOrWhiteSpace(opFaktYlTB.Text) ||
                            string.IsNullOrWhiteSpace(opFaktDomTB.Text) ||
                            string.IsNullOrWhiteSpace(opFaktKvTB.Text))
                        {
                            MessageBox.Show("Все обязательные поля должны быть заполнены!");
                            return;
                        }


                        if (teachers.Email == EmTB.Text && teachers.Phone_number == TelTB.Text &&
                            account.Password == PassTB.Text && account.Password == Pass2TB.Text &&
                            account.Login == LogTB.Text &&

                            teachers.Name == opImiaTB.Text && teachers.Surname == opFamTB.Text &&
                            teachers.Middle_name == opOtchTB.Text && teachers.Date_of_birth.ToShortDateString() == opDataRozTB.Text &&
                            teachers.Date_career_start.ToShortDateString() == DateKarStTB.Text &&
                            
                            doc_guardians.Passport_series.ToString() == opSerTB.Text &&
                            doc_guardians.Passport_number.ToString() == opNomTB.Text &&
                            doc_guardians.Date_of_issue.ToShortDateString() == opDataVidTB.Text &&
                            doc_guardians.Issued == opVidanTB.Text && doc_guardians.Unit_code == opKodPodrTB.Text &&
                            doc_guardians.INN == opINNTB.Text && doc_guardians.SNILS == opSNILSTB.Text &&

                            regAdr_guardians.Country == opStranaTB.Text && regAdr_guardians.Region == opRegTB.Text &&
                            regAdr_guardians.City == opGorodTB.Text && regAdr_guardians.District == opRaionTB.Text &&
                            regAdr_guardians.Street == opYlTB.Text && regAdr_guardians.House == opDomTB.Text &&
                            regAdr_guardians.Entrance.ToString() == opPodTB.Text &&
                            regAdr_guardians.Flat.ToString() == opKvTB.Text &&

                            resAdr_guardians.Country == opFaktStranaTB.Text && resAdr_guardians.Region == opFaktRegTB.Text &&
                            resAdr_guardians.City == opFaktGorodTB.Text && resAdr_guardians.District == opFaktRaionTB.Text &&
                            resAdr_guardians.Street == opFaktYlTB.Text && resAdr_guardians.House == opFaktDomTB.Text &&
                            resAdr_guardians.Entrance.ToString() == opFaktPodTB.Text &&
                            resAdr_guardians.Flat.ToString() == opFaktKvTB.Text)
                        {
                            MessageBox.Show("Изменения не были внесены.");
                            return;
                        }
                        if (!ValidateFields()) return;
                        teachers.Email = EmTB.Text;
                        teachers.Phone_number = TelTB.Text;
                        teachers.Name = opImiaTB.Text;
                        teachers.Surname = opFamTB.Text;
                        teachers.Middle_name = opOtchTB.Text;
                        teachers.Date_of_birth = DateTime.Parse(opDataRozTB.Text);
                        teachers.Date_career_start = DateTime.Parse(DateKarStTB.Text);

                        if (doc_guardians != null)
                        {
                            doc_guardians.Passport_series = Convert.ToInt32(opSerTB.Text);
                            doc_guardians.Passport_number = Convert.ToInt32(opNomTB.Text);
                            doc_guardians.Date_of_issue = DateTime.Parse(opDataVidTB.Text);
                            doc_guardians.Issued = opVidanTB.Text;
                            doc_guardians.Unit_code = opKodPodrTB.Text;
                            doc_guardians.INN = opINNTB.Text;
                            doc_guardians.SNILS = opSNILSTB.Text;
                        }

                        if (regAdr_guardians != null)
                        {
                            regAdr_guardians.Country = opStranaTB.Text;
                            regAdr_guardians.Region = opRegTB.Text;
                            regAdr_guardians.City = opGorodTB.Text;
                            regAdr_guardians.District = opRaionTB.Text;
                            regAdr_guardians.Street = opYlTB.Text;
                            regAdr_guardians.House = opDomTB.Text;
                            regAdr_guardians.Entrance = Convert.ToInt32(opPodTB.Text);
                            regAdr_guardians.Flat = Convert.ToInt32(opKvTB.Text);
                        }
                                                
                        if (resAdr_guardians != null)
                        {
                            resAdr_guardians.Country = opFaktStranaTB.Text;
                            resAdr_guardians.Region = opFaktRegTB.Text;
                            resAdr_guardians.City = opFaktGorodTB.Text;
                            resAdr_guardians.District = opFaktRaionTB.Text;
                            resAdr_guardians.Street = opFaktYlTB.Text;
                            resAdr_guardians.House = opFaktDomTB.Text;
                            resAdr_guardians.Entrance = Convert.ToInt32(opFaktPodTB.Text);
                            resAdr_guardians.Flat = Convert.ToInt32(opFaktKvTB.Text);
                        }

                        if (PassTB.Text == Pass2TB.Text)
                        {
                            account.Password = PassTB.Text;
                        }
                        else
                        {
                            MessageBox.Show("Пароль не подтвержден!");
                            return;
                        }

                        account.Login = LogTB.Text;
                        db.SaveChanges();
                        MessageBox.Show("Изменения успешно сохранены!");
                        FIO.Text = $"{teachers.Surname} {teachers.Name} {teachers.Middle_name}";
                        FIO2.Text = $"{teachers.Surname} {teachers.Name} {teachers.Middle_name}";
                        Email1.Text = $"{teachers.Email}";
                        Tel1.Text = $"{teachers.Phone_number}";
                        DatR1.Text = $"{teachers.Date_of_birth:dd.MM.yyyy}";
                    }
                    else
                    {
                        MessageBox.Show("Преподаватель не найден!");
                    }
                }
                else
                {
                    MessageBox.Show("Аккаунт не найден!");
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
        private void Zaiavki2_Click(object sender, RoutedEventArgs e)
        {
            PrepodProfilZaiavki s = new PrepodProfilZaiavki(_currentUser);
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
