using Material_design_kurs_andr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace Material_design_kurs_andr
{
    /// <summary>
    /// Логика взаимодействия для WorkerAdd.xaml
    /// </summary>
    public partial class WorkerAdd : Page
    {
        private int minvalue = 1,
        maxvalue = 100,
        startvalue = 5;
        public int Roleid;
        public string Workerfio;
        public int Workerid;
        public string gender;
        public static string[] SplitFio(string fio)
        {
            var results = fio.Split(" ", StringSplitOptions.None);
            return results;
        }
        public static bool StringIsValid(string str)
        {
            return !Regex.IsMatch(str, @"^[0-9]*$");
        }

        public WorkerAdd(int id, string fio, int workerid)
        {
            Roleid = id;
            Workerfio = fio;
            Workerid = workerid;
            InitializeComponent();
            WorkerBirth.DisplayDateEnd = DateTime.Now;
            txtNum.Text = startvalue.ToString();
            var db = HospitalkursContext.GetContext();
            Spesiality.ItemsSource = db.Positions.Select(n => n.PositionName).ToList();
            Spesiality.SelectedIndex = 0;
        }

        private void ToMenuPage_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new DoctorMenupage(Roleid, Workerfio, Workerid));
        }

        private void Worker_add_Click(object sender, RoutedEventArgs e)
        {
            string name = null;
            string surname = null;
            string secondname = null;
            string address, phone, passport;
            var db = HospitalkursContext.GetContext();
            if (New_worker_Fio.Text != null && WorkerBirth.SelectedDate != null && New_worker_Adress.Text != null && PhoneTextBox.Text != null && WorkerPassport.Text != null)
            {
                string[] workerfio = SplitFio(New_worker_Fio.Text.ToString());
                string spec = Spesiality.SelectedItem.ToString();
                DateTime dateTime = WorkerBirth.SelectedDate.Value;
                int workexperience = Convert.ToInt32(txtNum.Text);
                List<Positions> a = db.Positions.Where(c => c.PositionName ==spec).ToList();
                if (StringIsValid(PhoneTextBox.Text) == false || StringIsValid(WorkerPassport.Text) == false)
                {
                    address = New_worker_Adress.Text;
                    phone = PhoneTextBox.Text;
                    passport = WorkerPassport.Text;
                }
                else
                {
                    MessageBox.Show("Проверьте правильность ввода полей");
                    return;
                }
                if (workerfio.Length < 2)
                {
                    MessageBox.Show("Неверно введено ФИО(Минимум имя и фамилия)");
                    return;
                }
                else if (workerfio.Length == 2)
                {
                    name = workerfio[0];
                    surname = workerfio[1];
                }
                else if (workerfio.Length == 3)
                {
                    name = workerfio[0];
                    surname = workerfio[1];
                    secondname = workerfio[2];
                }
                else
                {
                    MessageBox.Show("Неверный ввод фамилии");
                    return;
                }

                Workers newworker = new Workers() { WorkerName=name,WorkerSurname=surname,WorkerSecondname=secondname,WorkerAge=dateTime,WorkerGender=gender,WorkerAdress= address,WorkerPhone=phone,WorkerPassport=passport,WorkerPosition=a[0].IdPositions,WorkExperience=workexperience};
                try
                {
                    db.Workers.AddRange(newworker);
                    db.SaveChanges();
                    MessageBox.Show("Работник записан");
                }
                catch (Microsoft.EntityFrameworkCore.DbUpdateException)
                {
                    MessageBox.Show("Данный сотрудник уже зарегестрирован в базе");
                }


            }
            else MessageBox.Show("Все поля обязательны к заполнению");


        }

        private void txtNum_TextChanged(object sender, TextChangedEventArgs e)
        {
            int number = 0;
            if (txtNum.Text != "")
                if (!int.TryParse(txtNum.Text, out number)) txtNum.Text = startvalue.ToString();
            if (number > maxvalue) txtNum.Text = maxvalue.ToString();
            if (number < minvalue) txtNum.Text = minvalue.ToString();
            txtNum.SelectionStart = txtNum.Text.Length;
        }

        private void WomanRadio_Checked(object sender, RoutedEventArgs e)
        {
            gender = "Ж";
        }

        private void ManRadio_Checked(object sender, RoutedEventArgs e)
        {
            gender = "М";
        }

        private void cmdDown_Click(object sender, RoutedEventArgs e)
        {
            int number;
            if (txtNum.Text != "") number = Convert.ToInt32(txtNum.Text);
            else number = 0;
            if (number > minvalue)
                txtNum.Text = Convert.ToString(number - 1);
        }

        private void cmdUp_Click(object sender, RoutedEventArgs e)
        {
            int number;
            if (txtNum.Text != "") number = Convert.ToInt32(txtNum.Text);
            else number = 0;
            if (number < maxvalue)
                txtNum.Text = Convert.ToString(number + 1);
        }
    }
}
