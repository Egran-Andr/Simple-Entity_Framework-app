using Material_design_kurs_andr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace Kurs_Andreev
{
    /// <summary>
    /// Логика взаимодействия для Record_registration.xaml
    /// </summary>
    public partial class Record_registration : Page
    {
        public HospitalkursContext db = HospitalkursContext.GetContext();
        public Record_registration()
        {
            InitializeComponent();
            calendar.DisplayDateStart = DateTime.Now;
            calendar.DisplayDateEnd = DateTime.Now.AddDays(14);
            calendar.SelectedDate = DateTime.Now;
            Workername.ItemsSource = db.WorkersScedule.Select(n => n.Fio).ToList();

        }
        public static bool StringIsValid(string str)
        {
            return !Regex.IsMatch(str, @"^[0-9]*$") || str.Length < 16;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Entry_frame.xaml", UriKind.Relative));//Переход на страницу входа
        }

        private void PresetTimePicker_SelectedTimeChanged(object sender, RoutedPropertyChangedEventArgs<DateTime?> e)
        {
            DateTime MyDateTime = ((DateTime)calendar.SelectedDate).Date.Add(((DateTime)PresetTimePicker.SelectedTime).TimeOfDay);
            DateTime curday = calendar.SelectedDate.Value;
            String weekday = curday.DayOfWeek.ToString();
            DateTime dt = (DateTime)PresetTimePicker.SelectedTime;
            TimeSpan st = dt.TimeOfDay;
            Workername.SelectedItem = null;
            Workername.ItemsSource = db.WorkersScedule.Where(x => x.Starttime < st && x.Endtime > st && x.Days.Contains(weekday)).Select(x => x.Fio).ToList();
        }

        private void GetWorkerTicket_Click(object sender, RoutedEventArgs e)
        {
            DateTime MyDateTime = ((DateTime)calendar.SelectedDate).Date.Add(((DateTime)PresetTimePicker.SelectedTime).TimeOfDay);
            if (Workername.SelectedItem != null && PresetTimePicker.SelectedTime != null && OMC.Text != null)
            {
                if (StringIsValid(OMC.Text) == true)
                {
                    MessageBox.Show("Неверный формат полиса! Проверьте поле ввода");
                    return;
                }
                List<FioId> a = db.FioId.Where(c => c.Fio == Workername.Text.ToString()).ToList();
                if (a.Count == 0)
                {
                    MessageBox.Show("Непредвиденная ошибка!");
                }
                else
                {
                    Records record = new Records() { WorkerRecord = a[0].IdWorkers, RecordDate = MyDateTime, PatientOmc = OMC.Text.ToString() };
                    try
                    {
                        db.Records.Add(record);
                        db.SaveChanges();
                        MessageBox.Show("Запись подтверждена!");
                        this.NavigationService.Navigate(new Uri("Entry_frame.xaml", UriKind.Relative));//Переход на страницу входа
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
    }
}
