using Material_design_kurs_andr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Логика взаимодействия для Doctor_apointments.xaml
    /// </summary>
    public partial class Doctor_apointments : Page
    {
        public int Roleid;
        public string WorkerFio;
        public int Workerid;
        public Doctor_apointments(int id, string fio, int workerid)
        {
            InitializeComponent();
            WorkerFio = fio;
            Workerid = workerid;
            Roleid = id;
            var db = HospitalkursContext.GetContext();
            RecordList.ItemsSource = db.Records.Where(c => c.WorkerRecord==workerid).ToList();
            RecordList.IsReadOnly = true;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            RecordList.Columns[0].Visibility = Visibility.Hidden;
            RecordList.Columns[3].Visibility = Visibility.Hidden;
        }



        private void GetWorkerTicket_Click(object sender, RoutedEventArgs e)
        {
            var db = HospitalkursContext.GetContext();
            DateTime MyDateTime = ((DateTime)calendar.SelectedDate).Date.Add(((DateTime)PresetTimePicker.SelectedTime).TimeOfDay);
            if ( PresetTimePicker.SelectedTime != null && OMC.Text != null)
            {
                if (StringIsValid(OMC.Text) == true)
                {
                    MessageBox.Show("Неверный формат полиса! Проверьте поле ввода");
                    return;
                }
                List<FioId> a = db.FioId.Where(c => c.Fio == WorkerFio).ToList();
                if (a.Count == 0)
                {
                    MessageBox.Show("Непредвиденная ошибка!");
                }
                else
                {
                    Records record = new Records() { WorkerRecord = Workerid, RecordDate = MyDateTime, PatientOmc = OMC.Text.ToString() };
                    try
                    {
                        db.Records.Add(record);
                        db.SaveChanges();
                        MessageBox.Show("Запись подтверждена!");
                        RecordList.ItemsSource = db.Records.Where(c => c.WorkerRecord == Workerid).ToList();
                        RecordList.Columns[0].Visibility = Visibility.Hidden;
                        RecordList.Columns[3].Visibility = Visibility.Hidden;
                    }
                    catch (System.InvalidOperationException)
                    {
                        MessageBox.Show("Данное окно уже занято. Выберите другое время!");
                    }
                    catch (Microsoft.EntityFrameworkCore.DbUpdateException)
                    {
                        MessageBox.Show("Данное окно уже занято. Выберите другое время!");
                    }
                }

            }
            else
            {
                MessageBox.Show("Все поля обязательны к заполнению!");
            }
        }

        public static bool StringIsValid(string str)
        {
            return !Regex.IsMatch(str, @"^[0-9]*$") || str.Length < 16;
        }

        private void RecordList_PreviewKeyDown(object sender, KeyEventArgs e)
        {
                var db = HospitalkursContext.GetContext();
                try
                {
                    if (e.Key == Key.Delete)
                    {
                        MessageBoxResult result = MessageBox.Show("Удалить выделенные элементы?", "Потдверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (result == MessageBoxResult.Yes)
                        {
                            List<Object> a = RecordList.SelectedItems.Cast<Object>().ToList();
                            a.ForEach(n => db.Remove(n));
                            db.SaveChanges();
                        }
                        else
                        {
                            return;
                        }
                    }
                }
                catch (System.InvalidOperationException)
                {
                    MessageBox.Show("Ошибка. Изменения не были сохранены");
                    return;
                }

            }

        private void ToMenuPage_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new DoctorMenupage(Roleid, WorkerFio,Workerid));
        }
    }
    }
