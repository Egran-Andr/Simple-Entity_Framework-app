using Material_design_kurs_andr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Логика взаимодействия для Worker_datework.xaml
    /// </summary>
    public partial class Worker_datework : Page
    {
        public int Roleid;
        public string Workerfio;
        public int Workerid;
        private int weekday;
        private bool Iswork;
        public HospitalkursContext db = HospitalkursContext.GetContext();
        public Worker_datework(int id, string fio, int workerid)
        {
            InitializeComponent();
            Roleid = id;
            Workerfio = fio;
            Workerid = workerid;
            Iswork = false;
            elipse1.Visibility = Visibility.Visible;
            rectangle1.Visibility = Visibility.Collapsed;
            elipse2.Visibility = Visibility.Collapsed;
            rectangle2.Visibility = Visibility.Visible;
            Worker.ItemsSource = db.FioId.Select(n => n.Fio).ToList();
            Worker1.ItemsSource=db.FioId.Select(n => n.Fio).ToList();
        }

        private void GotoEntry_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new DoctorMenupage(Roleid, Workerfio, Workerid));
        }

        private void workers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (WorkDateAdd != null && WorkDateAdd.IsSelected)
            {
                elipse1.Visibility = Visibility.Visible;
                rectangle1.Visibility = Visibility.Collapsed;
                elipse2.Visibility = Visibility.Collapsed;
                rectangle2.Visibility = Visibility.Visible;

            }

            if (HolidaysAdd != null && HolidaysAdd.IsSelected)
            {
                elipse1.Visibility = Visibility.Collapsed;
                rectangle1.Visibility = Visibility.Visible;
                elipse2.Visibility = Visibility.Visible;
                rectangle2.Visibility = Visibility.Collapsed;
            }

        }

        private void SceduleAdd_Click(object sender, RoutedEventArgs e)
        {
            if (Worker.SelectedItem != null && calendarbegin.SelectedDate != null && TimeDayBegin.SelectedTime != null && TimeDayend.SelectedTime != null)
            {
                var begindate = calendarbegin.SelectedDate.Value.Date;
                var timebegin = TimeDayBegin.SelectedTime.Value.TimeOfDay;
                var timeend = TimeDayend.SelectedTime.Value.TimeOfDay;
                WorkScedule newscedule = new WorkScedule();
                List<FioId> a = db.FioId.Where(c => c.Fio == Worker.Text.ToString()).ToList();
                weekday = 0;
                if (Monday_check.IsChecked == true) weekday += 2;
                if (Tuesday_check.IsChecked == true) weekday += 4;
                if (Wednesday_check.IsChecked == true) weekday += 8;
                if (Thursday_check.IsChecked == true) weekday += 16;
                if (Friday_check.IsChecked == true) weekday += 32;
                if (Saturday_check.IsChecked == true) weekday += 64;
                if (Sunday_check.IsChecked == true) weekday += 128;
                if(weekday == 0)
                {
                    MessageBox.Show("Необходимо выбрать день недели");
                    return;
                }
                try { 
                if (calendarend.SelectedDate == null)
                {
                        newscedule.WorkerId = a[0].IdWorkers;
                        newscedule.Starttime = timebegin;
                        newscedule.Endtime = timeend;
                        newscedule.Weekday = weekday;
                        newscedule.WorkFrom = begindate;
                    db.WorkScedule.Add(newscedule);
                    db.SaveChanges();
                    MessageBox.Show("Сотрудник приписан к отделению");
                        newscedule = null;
                    }
                else
                {
                        var enddate = calendarend.SelectedDate.Value.Date;
                        newscedule.WorkerId = a[0].IdWorkers;
                        newscedule.Starttime = timebegin;
                        newscedule.Endtime = timeend;
                        newscedule.Weekday = weekday;
                        newscedule.WorkFrom = begindate;
                        newscedule.WorkTo = enddate;
                        db.WorkScedule.Add(newscedule);
                    db.SaveChanges();
                    MessageBox.Show("Сотрудник приписан к отделению");
                    newscedule = null;
                }
                }
                catch (Microsoft.EntityFrameworkCore.DbUpdateException)
                {
                    MessageBox.Show("Ошибка добавления");
                }
                catch (System.InvalidOperationException)
                {
                    MessageBox.Show("Ошибка. Изменения не были сохранены");
                    return;
                }
            }
            else MessageBox.Show("Все необходимые поля должны быть заполненны!");
           
        }

        private void Allworkers_Checkbox_Unchecked(object sender, RoutedEventArgs e)
        {

            Worker1.IsReadOnly = true;
            Worker1.IsEnabled = true;
            Worker1.Background = Brushes.White;
        }

        private void Allworkers_Checkbox_Checked(object sender, RoutedEventArgs e)
        {
            Worker1.IsReadOnly = true;
            Worker1.IsEnabled = false;
        }
        private void Isdayoff_Checked(object sender, RoutedEventArgs e)
        {
            Iswork = true;
        }

        private void Isdayoff_Unchecked(object sender, RoutedEventArgs e)
        {
            Iswork = false;
        }

        private void Holidayadd_Click(object sender, RoutedEventArgs e)
        {
            List<FioId> b = db.FioId.Where(c => c.Fio == Worker1.Text.ToString()).ToList();
            int idcounter = db.Holidays.OrderByDescending(p => p.Idholidays).First().Idholidays +1;
            Holidays newholiday= new Holidays();
            if (holidaybegin.SelectedDate != null && holidayend.SelectedDate != null)
            {
                for (DateTime counter = holidaybegin.SelectedDate.Value; counter <= holidayend.SelectedDate.Value; counter = counter.AddDays(1))
                {
                    if (Allworkers_Checkbox.IsChecked == true)
                    {
                        newholiday.Idholidays = idcounter;
                        newholiday.Hday = counter;
                        newholiday.IsWork = Iswork;
                        db.Holidays.Add(newholiday);
                        idcounter++;

                    }
                    else
                    {
                        newholiday.Idholidays = idcounter;
                        newholiday.Hday = counter;
                        newholiday.Worker = b[0].IdWorkers;
                        newholiday.IsWork = Iswork;
                        db.Holidays.Add(newholiday);
                        idcounter++;
                    }
                    db.SaveChanges();
                    MessageBox.Show("Данные сохранены");
                }

            }
            else {
                MessageBox.Show("Даты обязательны к заполнению");
            } 
        }

       
    }
}
