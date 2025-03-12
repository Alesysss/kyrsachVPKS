using kyrsachVPKS.Classes;
using kyrsachVPKS.Entities;
using kyrsachVPKS.Windows.Users;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Principal;
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


namespace kyrsachVPKS.Windows.Student
{
    /// <summary>
    /// Логика взаимодействия для StudProfil.xaml
    /// </summary>
    public partial class StudProfil : Window
    {
        private UserSession _currentUser;
        public StudProfil(UserSession user)
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
                    var student = db.Students.FirstOrDefault(s => s.ID == account.ID_Students);
                    var guardians = db.Guardians.FirstOrDefault(s => s.ID == student.ID_Guardians);
                    var doc_guardians = db.Documents.FirstOrDefault(s => s.ID == guardians.ID_Documents);
                    var regAdr_guardians = db.RegistrationAddress.FirstOrDefault(s => s.ID == 
                                                                    guardians.ID_RegistrationAddress);
                    var resAdr_guardians = db.ResidenceAddress.FirstOrDefault(s => s.ID == 
                                                                    guardians.ID_ResidenceAddress);
                    var doc_student = db.Documents.FirstOrDefault(s => s.ID == student.ID_Documents);
                    var regAdr_student = db.RegistrationAddress.FirstOrDefault(s => s.ID ==
                                                                    student.ID_RegistrationAddress);

                    var resAdr_student = db.ResidenceAddress.FirstOrDefault(s => s.ID ==
                                                                    student.ID_ResidenceAddress);
                    if (student != null)
                    {
                        FIO.Text = $"{student.Surname} {student.Name} {student.Middle_name}";
                        FIO2.Text = $"{student.Surname} {student.Name} {student.Middle_name}";
                        Email1.Text = $"{student.Email}";
                        Tel1.Text = $"{student.Phone_number}";
                        DatR1.Text = $"{student.Date_of_birth:dd.MM.yyyy}";

                        EmTB.Text = $"{student.Email}";
                        TelTB.Text = $"{student.Phone_number}";
                        PassTB.Text = $"{account.Password}";
                        Pass2TB.Text = $"{account.Password}";
                        LogTB.Text = $"{account.Login}";

                        opImiaTB.Text = $"{guardians.Name}";
                        opFamTB.Text = $"{guardians.Surname}";
                        opOtchTB.Text = $"{guardians.Middle_name}";
                        opDataRozTB.Text = $"{guardians.Date_of_birth:dd.MM.yyyy}";
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

                        rebImiaTB.Text = $"{student.Name}";
                        rebFamTB.Text = $"{student.Surname}";
                        rebOtchTB.Text = $"{student.Middle_name}";
                        rebDataRozTB.Text = $"{student.Date_of_birth:dd.MM.yyyy}";
                        //ПОЛ

                        rebSerTB.Text = $"{doc_student.Passport_series}";
                        rebNomTB.Text = $"{doc_student.Passport_number}";
                        rebDataVidTB.Text = $"{doc_student.Date_of_issue:dd.MM.yyyy}";
                        rebVidanTB.Text = $"{doc_student.Issued}";
                        rebKodPodrTB.Text = $"{doc_student.Unit_code}";
                        rebINNTB.Text = $"{doc_student.INN}";
                        rebSNILSTB.Text = $"{doc_student.SNILS}";

                        rebStranaTB.Text = $"{regAdr_student.Country}";
                        rebRegTB.Text = $"{regAdr_student.Region}";
                        rebGorodTB.Text = $"{regAdr_student.City}";
                        rebRaionTB.Text = $"{regAdr_student.District}";
                        rebYlTB.Text = $"{regAdr_student.Street}";
                        rebDomTB.Text = $"{regAdr_student.House}";
                        rebPodTB.Text = $"{regAdr_student.Entrance}";
                        rebKvTB.Text = $"{regAdr_student.Flat}";

                        rebFaktStranaTB.Text = $"{resAdr_student.Country}";
                        rebFaktRegTB.Text = $"{resAdr_student.Region}";
                        rebFaktGorodTB.Text = $"{resAdr_student.City}";
                        rebFaktRaionTB.Text = $"{resAdr_student.District}";
                        rebFaktYlTB.Text = $"{resAdr_student.Street}";
                        rebFaktDomTB.Text = $"{resAdr_student.House}";
                        rebFaktPodTB.Text = $"{resAdr_student.Entrance}";
                        rebFaktKvTB.Text = $"{resAdr_student.Flat}";

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

            // 🔹 Проверка студента
            if (!nameRegex.IsMatch(rebFamTB.Text)) { ShowError(rebFamTB, "Ошибка в фамилии студента."); return false; }
            if (!nameRegex.IsMatch(rebImiaTB.Text)) { ShowError(rebImiaTB, "Ошибка в имени студента."); return false; }
            if (rebOtchTB.Text.Length > 0 && !optionalPatronymicRegex.IsMatch(rebOtchTB.Text))
            { ShowError(rebOtchTB, "Ошибка в отчестве студента."); return false; }

            if (!phoneRegex.IsMatch(TelTB.Text)) { ShowError(TelTB, "Ошибка в номере телефона студента."); return false; }

            if (!passportSeriesRegex.IsMatch(rebSerTB.Text)) { ShowError(rebSerTB, "Ошибка в серии паспорта студента."); return false; }
            if (!passportNumberRegex.IsMatch(rebNomTB.Text)) { ShowError(rebNomTB, "Ошибка в номере паспорта студента."); return false; }
            if (!passportIssuedByRegex.IsMatch(rebVidanTB.Text)) { ShowError(rebVidanTB, "Ошибка в поле 'Кем выдан' студента."); return false; }
            if (!unitCodeRegex.IsMatch(rebKodPodrTB.Text)) { ShowError(rebKodPodrTB, "Ошибка в коде подразделения студента."); return false; }
            if (!innRegex.IsMatch(rebINNTB.Text)) { ShowError(rebINNTB, "Ошибка в ИНН студента."); return false; }
            if (!snilsRegex.IsMatch(rebSNILSTB.Text)) { ShowError(rebSNILSTB, "Ошибка в СНИЛС студента."); return false; }

            if (!addressRegex.IsMatch(rebStranaTB.Text)) { ShowError(rebStranaTB, "Ошибка в стране студента."); return false; }
            if (!addressRegex.IsMatch(rebGorodTB.Text)) { ShowError(rebGorodTB, "Ошибка в городе студента."); return false; }
            if (!streetRegex.IsMatch(rebYlTB.Text)) { ShowError(rebYlTB, "Ошибка в улице студента."); return false; }
            if (!numberRegex.IsMatch(rebDomTB.Text)) { ShowError(rebDomTB, "Ошибка в номере дома студента."); return false; }
            if (rebPodTB.Text.Length > 0 && !numberRegex.IsMatch(rebPodTB.Text)) { ShowError(rebPodTB, "Ошибка в номере подъезда студента."); return false; }
            if (!numberRegex.IsMatch(rebKvTB.Text)) { ShowError(rebKvTB, "Ошибка в номере квартиры студента."); return false; }

            if (!emailRegex.IsMatch(EmTB.Text)) { ShowError(EmTB, "Ошибка в email студента."); return false; }
            if (!loginRegex.IsMatch(LogTB.Text)) { ShowError(LogTB, "Ошибка в логине студента."); return false; }
            if (!passwordRegex.IsMatch(PassTB.Text) || PassTB.Text != Pass2TB.Text)
            { ShowError(PassTB, "Ошибка в пароле студента. Пароли должны совпадать."); return false; }

            // 🔹 Проверка родителя
            if (!nameRegex.IsMatch(opFamTB.Text)) { ShowError(opFamTB, "Ошибка в фамилии родителя."); return false; }
            if (!nameRegex.IsMatch(opImiaTB.Text)) { ShowError(opImiaTB, "Ошибка в имени родителя."); return false; }
            if (opOtchTB.Text.Length > 0 && !optionalPatronymicRegex.IsMatch(opOtchTB.Text))
            { ShowError(opOtchTB, "Ошибка в отчестве родителя."); return false; }

            if (!passportSeriesRegex.IsMatch(opSerTB.Text)) { ShowError(opSerTB, "Ошибка в серии паспорта родителя."); return false; }
            if (!passportNumberRegex.IsMatch(opNomTB.Text)) { ShowError(opNomTB, "Ошибка в номере паспорта родителя."); return false; }
            if (!passportIssuedByRegex.IsMatch(opVidanTB.Text)) { ShowError(opVidanTB, "Ошибка в поле 'Кем выдан' родителя."); return false; }
            if (!unitCodeRegex.IsMatch(opKodPodrTB.Text)) { ShowError(opKodPodrTB, "Ошибка в коде подразделения родителя."); return false; }
            if (!innRegex.IsMatch(opINNTB.Text)) { ShowError(opINNTB, "Ошибка в ИНН родителя."); return false; }
            if (!snilsRegex.IsMatch(opSNILSTB.Text)) { ShowError(opSNILSTB, "Ошибка в СНИЛС родителя."); return false; }

            if (!addressRegex.IsMatch(opStranaTB.Text)) { ShowError(opStranaTB, "Ошибка в стране родителя."); return false; }
            if (!addressRegex.IsMatch(opGorodTB.Text)) { ShowError(opGorodTB, "Ошибка в городе родителя."); return false; }
            if (!streetRegex.IsMatch(opYlTB.Text)) { ShowError(opYlTB, "Ошибка в улице родителя."); return false; }
            if (!numberRegex.IsMatch(opDomTB.Text)) { ShowError(opDomTB, "Ошибка в номере дома родителя."); return false; }
            if (opPodTB.Text.Length > 0 && !numberRegex.IsMatch(opPodTB.Text)) { ShowError(opPodTB, "Ошибка в номере подъезда родителя."); return false; }
            if (!numberRegex.IsMatch(opKvTB.Text)) { ShowError(opKvTB, "Ошибка в номере квартиры родителя."); return false; }
                       
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
                var student = db.Students.FirstOrDefault(s => s.ID == account.ID_Students);

                var guardians = db.Guardians.FirstOrDefault(s => s.ID == student.ID_Guardians);
                var doc_guardians = db.Documents.FirstOrDefault(s => s.ID == guardians.ID_Documents);
                var regAdr_guardians = db.RegistrationAddress.FirstOrDefault(s => s.ID ==
                                                                guardians.ID_RegistrationAddress);
                var resAdr_guardians = db.ResidenceAddress.FirstOrDefault(s => s.ID ==
                                                                guardians.ID_ResidenceAddress);
                var doc_student = db.Documents.FirstOrDefault(s => s.ID == student.ID_Documents);
                var regAdr_student = db.RegistrationAddress.FirstOrDefault(s => s.ID ==
                                                                student.ID_RegistrationAddress);

                var resAdr_student = db.ResidenceAddress.FirstOrDefault(s => s.ID ==
                                                                student.ID_ResidenceAddress);
                if (account != null)
                {
                    

                    if (student != null)
                    {
                        if (string.IsNullOrWhiteSpace(EmTB.Text) || string.IsNullOrWhiteSpace(TelTB.Text) ||
                            string.IsNullOrWhiteSpace(rebImiaTB.Text) || string.IsNullOrWhiteSpace(rebFamTB.Text) ||
                            string.IsNullOrWhiteSpace(PassTB.Text) ||
                            string.IsNullOrWhiteSpace(Pass2TB.Text) || string.IsNullOrWhiteSpace(LogTB.Text) ||

                            string.IsNullOrWhiteSpace(rebDataRozTB.Text) ||
                            string.IsNullOrWhiteSpace(rebSerTB.Text) || string.IsNullOrWhiteSpace(rebNomTB.Text) ||
                            string.IsNullOrWhiteSpace(rebDataVidTB.Text) || string.IsNullOrWhiteSpace(rebVidanTB.Text) ||
                            string.IsNullOrWhiteSpace(rebKodPodrTB.Text) || string.IsNullOrWhiteSpace(rebINNTB.Text) ||
                            string.IsNullOrWhiteSpace(rebSNILSTB.Text) || 
                            
                            string.IsNullOrWhiteSpace(rebStranaTB.Text) ||
                            string.IsNullOrWhiteSpace(rebGorodTB.Text) || string.IsNullOrWhiteSpace(rebYlTB.Text) ||
                            string.IsNullOrWhiteSpace(rebDomTB.Text) || 
                            string.IsNullOrWhiteSpace(rebKvTB.Text) ||

                            string.IsNullOrWhiteSpace(rebFaktStranaTB.Text) ||
                            string.IsNullOrWhiteSpace(rebFaktGorodTB.Text) || string.IsNullOrWhiteSpace(rebFaktYlTB.Text) ||
                            string.IsNullOrWhiteSpace(rebFaktDomTB.Text) ||
                            string.IsNullOrWhiteSpace(rebFaktKvTB.Text) ||

                            string.IsNullOrWhiteSpace(opDataRozTB.Text) ||
                            string.IsNullOrWhiteSpace(opSerTB.Text) || string.IsNullOrWhiteSpace(opNomTB.Text) ||
                            string.IsNullOrWhiteSpace(opDataVidTB.Text) || string.IsNullOrWhiteSpace(opVidanTB.Text) ||
                            string.IsNullOrWhiteSpace(opKodPodrTB.Text) || string.IsNullOrWhiteSpace(opINNTB.Text) ||
                            string.IsNullOrWhiteSpace(opSNILSTB.Text) ||

                            string.IsNullOrWhiteSpace(opStranaTB.Text) ||
                            string.IsNullOrWhiteSpace(opGorodTB.Text) || string.IsNullOrWhiteSpace(opYlTB.Text) ||
                            string.IsNullOrWhiteSpace(opDomTB.Text) ||
                            string.IsNullOrWhiteSpace(opKvTB.Text) ||

                            string.IsNullOrWhiteSpace(opFaktStranaTB.Text) ||
                            string.IsNullOrWhiteSpace(opFaktGorodTB.Text) || string.IsNullOrWhiteSpace(opFaktYlTB.Text) ||
                            string.IsNullOrWhiteSpace(opFaktDomTB.Text) ||
                            string.IsNullOrWhiteSpace(opFaktKvTB.Text) )
                        {
                            MessageBox.Show("Все обязательные поля должны быть заполнены!");
                            return;
                        }


                        if (student.Email == EmTB.Text && student.Phone_number == TelTB.Text &&
                            account.Password == PassTB.Text && account.Password == Pass2TB.Text &&
                            account.Login == LogTB.Text &&

                            student.Name == rebImiaTB.Text && student.Surname == rebFamTB.Text &&
                            student.Middle_name == rebOtchTB.Text && student.Date_of_birth.ToShortDateString() == rebDataRozTB.Text &&

                            doc_student.Passport_series.ToString() == rebSerTB.Text && 
                            doc_student.Passport_number.ToString() == rebNomTB.Text &&
                            doc_student.Date_of_issue.ToShortDateString() == rebDataVidTB.Text &&
                            doc_student.Issued == rebVidanTB.Text && doc_student.Unit_code == rebKodPodrTB.Text &&
                            doc_student.INN == rebINNTB.Text && doc_student.SNILS == rebSNILSTB.Text &&

                            regAdr_student.Country == rebStranaTB.Text && regAdr_student.Region == rebRegTB.Text &&
                            regAdr_student.City == rebGorodTB.Text && regAdr_student.District == rebRaionTB.Text &&
                            regAdr_student.Street == rebYlTB.Text && regAdr_student.House == rebDomTB.Text &&
                            regAdr_student.Entrance.ToString() == rebPodTB.Text&& 
                            regAdr_student.Flat.ToString() == rebKvTB.Text &&

                            resAdr_student.Country == rebFaktStranaTB.Text && resAdr_student.Region == rebFaktRegTB.Text &&
                            resAdr_student.City == rebFaktGorodTB.Text && resAdr_student.District == rebFaktRaionTB.Text &&
                            resAdr_student.Street == rebFaktYlTB.Text && resAdr_student.House == rebFaktDomTB.Text &&
                            resAdr_student.Entrance.ToString() == rebFaktPodTB.Text &&
                            resAdr_student.Flat.ToString() == rebFaktKvTB.Text &&

                            guardians.Name == opImiaTB.Text && guardians.Surname == opFamTB.Text &&
                            guardians.Middle_name == opOtchTB.Text && guardians.Date_of_birth.ToShortDateString() == opDataRozTB.Text &&

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
                            resAdr_guardians.Flat.ToString() == opFaktKvTB.Text )
                        {
                            MessageBox.Show("Изменения не были внесены.");
                            return;
                            
                        }

                        if (!ValidateFields()) return;

                        student.Email = EmTB.Text;
                        student.Phone_number = TelTB.Text;
                        student.Name = rebImiaTB.Text;
                        student.Surname = rebFamTB.Text;
                        student.Middle_name = rebOtchTB.Text;
                        student.Date_of_birth = DateTime.Parse(rebDataRozTB.Text);

                        if(guardians != null)
                        {
                            guardians.Name = opImiaTB.Text;
                            guardians.Surname = opFamTB.Text;
                            guardians.Middle_name = opOtchTB.Text;
                            guardians.Date_of_birth = DateTime.Parse(opDataRozTB.Text);
                        }

                        if (doc_student != null)
                        {
                            doc_student.Passport_series = Convert.ToInt32(rebSerTB.Text);
                            doc_student.Passport_number = Convert.ToInt32(rebNomTB.Text);
                            doc_student.Date_of_issue = DateTime.Parse(rebDataVidTB.Text);
                            doc_student.Issued = rebVidanTB.Text;
                            doc_student.Unit_code = rebKodPodrTB.Text;
                            doc_student.INN = rebINNTB.Text;
                            doc_student.SNILS = rebSNILSTB.Text;
                        }

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

                        if (regAdr_student != null)
                        {
                            regAdr_student.Country = rebStranaTB.Text;
                            regAdr_student.Region = rebRegTB.Text;
                            regAdr_student.City = rebGorodTB.Text;
                            regAdr_student.District = rebRaionTB.Text;
                            regAdr_student.Street = rebYlTB.Text;
                            regAdr_student.House = rebDomTB.Text;
                            regAdr_student.Entrance = Convert.ToInt32(rebPodTB.Text);
                            regAdr_student.Flat = Convert.ToInt32(rebKvTB.Text);
                            db.Entry(regAdr_student).State = EntityState.Modified;
                        }
                        else
                        {
                            MessageBox.Show("РЕГИСТР АДР СТУД");
                            return;
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

                        if (resAdr_student != null)
                        {
                            resAdr_student.Country = rebFaktStranaTB.Text;
                            resAdr_student.Region = rebFaktRegTB.Text;
                            resAdr_student.City = rebFaktGorodTB.Text;
                            resAdr_student.District = rebFaktRaionTB.Text;
                            resAdr_student.Street = rebFaktYlTB.Text;
                            resAdr_student.House = rebFaktDomTB.Text;
                            resAdr_student.Entrance = Convert.ToInt32(rebFaktPodTB.Text);
                            resAdr_student.Flat = Convert.ToInt32(rebFaktKvTB.Text);
                            db.Entry(resAdr_student).State = EntityState.Modified;
                        }
                        else
                        {
                            MessageBox.Show("ФАКТ АДР СТУД");
                            return;
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
                        FIO.Text = $"{student.Surname} {student.Name} {student.Middle_name}";
                        FIO2.Text = $"{student.Surname} {student.Name} {student.Middle_name}";
                        Email1.Text = $"{student.Email}";
                        Tel1.Text = $"{student.Phone_number}";
                        DatR1.Text = $"{student.Date_of_birth:dd.MM.yyyy}";
                    }
                    else
                    {
                        MessageBox.Show("Студент не найден!");
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
        private void Zaiavki_Click(object sender, RoutedEventArgs e)
        {
            StudZaiavki studZaiavki = new StudZaiavki(_currentUser);
            studZaiavki.Show();
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
