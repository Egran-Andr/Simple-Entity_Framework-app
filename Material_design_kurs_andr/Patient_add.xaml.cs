using Material_design_kurs_andr.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Material_design_kurs_andr
{
    /// <summary>
    /// Логика взаимодействия для Patient_add.xaml
    /// </summary>
    public partial class Patient_add : Page
    {
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
        public Patient_add(int id, string fio, int workerid)
        {
            Roleid = id;
            Workerfio = fio;
            Workerid = workerid;
            InitializeComponent();
            Patiend_Birth.DisplayDateEnd = DateTime.Now;
        }

        private void ToMenuPage_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new DoctorMenupage(Roleid, Workerfio, Workerid));
        }

        private void WomanRadio_Checked(object sender, RoutedEventArgs e)
        {
            gender = "Ж";
        }

        private void ManRadio_Checked(object sender, RoutedEventArgs e)
        {
            gender = "М";
        }

        private void Patient_add1_Click(object sender, RoutedEventArgs e)
        {
            string name = null;
            string surname = null;
            string secondname = null;
            string address, phone, OMC,SNILS;
            var db = HospitalkursContext.GetContext();
            if (New_patient_Fio.Text != null && Patiend_Birth.SelectedDate != null && New_patient_Adress.Text != null && PhoneTextBox.Text != null && PatientOMC.Text != null && PatientSNILS.Text != null)
            {
                string[] patientfio = SplitFio(New_patient_Fio.Text.ToString());
                DateTime dateTime = Patiend_Birth.SelectedDate.Value;
                if (StringIsValid(PhoneTextBox.Text) == false || StringIsValid(PatientOMC.Text) == false || StringIsValid(PatientSNILS.Text) == false)
                {
                    address = New_patient_Adress.Text;
                    phone = PhoneTextBox.Text;
                    OMC = PatientOMC.Text;
                    SNILS = PatientSNILS.Text;
                }
                else
                {
                    MessageBox.Show("Проверьте правильность ввода полей");
                    return;
                }
                if (patientfio.Length < 2)
                {
                    MessageBox.Show("Неверно введено ФИО(Минимум имя и фамилия)");
                    return;
                }
                else if (patientfio.Length == 2)
                {
                    name = patientfio[0];
                    surname = patientfio[1];
                }
                else if (patientfio.Length == 3)
                {
                    name = patientfio[0];
                    surname = patientfio[1];
                    secondname = patientfio[2];
                }
                else
                {
                    MessageBox.Show("Неверный ввод фамилии");
                    return;
                }

                if (String.IsNullOrWhiteSpace(Patient_dmc_org.Text) && String.IsNullOrWhiteSpace(Patient_dmc_num.Text))
                {
                    PatientInfo newpatient = new PatientInfo() { PatientName=name,PatientSurname=surname,PatientSecondname=secondname,PatientAge=dateTime,PatientGender=gender,PatientAdress=address,PatientPhone=phone,OmcNumber=OMC,SnilsNumber=SNILS};
                    
                        db.PatientInfo.AddRange(newpatient);
                        db.SaveChanges();
                        MessageBox.Show("Работник записан");
                    
                }
                else if(Patient_dmc_org.Text.Length>0 && Patient_dmc_num.Text.Length>0)
                {
                    PatientInfo newpatient = new PatientInfo() { PatientName = name, PatientSurname = surname, PatientSecondname = secondname, PatientAge = dateTime, PatientGender = gender, PatientAdress = address, PatientPhone = phone, OmcNumber = OMC, SnilsNumber = SNILS,DmcOrganisation= Patient_dmc_org.Text,DmcNumber = Patient_dmc_num.Text };
                    try
                    {
                        db.PatientInfo.AddRange(newpatient);
                        db.SaveChanges();
                        MessageBox.Show("Работник записан");
                    }
                    catch (Microsoft.EntityFrameworkCore.DbUpdateException)
                    {
                        MessageBox.Show("Данный пациент уже есть в базе");
                    }
                }
                else
                {
                    MessageBox.Show("Необходимо указать страховую организацию И номер страхового полиса");
                    Patient_dmc_num.Text = null;
                    Patient_dmc_org.Text = null;
                }
                
            }
            else MessageBox.Show("Все поля обязательны к заполнению");
        }
    }
}
