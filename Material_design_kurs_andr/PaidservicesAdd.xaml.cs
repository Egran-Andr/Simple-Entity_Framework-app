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
    /// Логика взаимодействия для PaidservicesAdd.xaml
    /// </summary>
    public partial class PaidservicesAdd : Page
    {
        public int Roleid;
        public string Workerfio;
        public int Workerid;
        public HospitalkursContext db = HospitalkursContext.GetContext();
        public PaidservicesAdd(int id, string fio, int workerid)
        {
            InitializeComponent();
            Roleid = id;
            Workerfio = fio;
            Workerid = workerid;
            if (Roleid != 3)
            {
                Services_List.IsReadOnly = true;
                Services_List.ItemsSource = db.ServiceSchedule.Where(c => c.WorkerId == workerid).ToList();
                Workercombo.Visibility = Visibility.Collapsed;
                WorkerLabel.Visibility = Visibility.Collapsed;
            }
            else
            {
                Services_List.ItemsSource = db.ServiceSchedule.ToList();
            }
            Workercombo.ItemsSource = db.FioId.Select(n => n.Fio).ToList();
            PatientCombo.ItemsSource = db.PatientfioId.Select(n => n.Fio).ToList();
            Servisecombo.ItemsSource = db.Service.Select(n => n.ServiceName).ToList();
        }

        private void ToMenuPage_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new DoctorMenupage(Roleid, Workerfio, Workerid));
        }

        private void ServiseAdd_Click(object sender, RoutedEventArgs e)
        {
            if (calendar.SelectedDate != null && PresetTimePicker.SelectedTime != null && Servisecombo.SelectedItem != null && PatientCombo.SelectedItem != null)
            {
                List<PatientfioId> b = db.PatientfioId.Where(c => c.Fio == PatientCombo.Text.ToString()).ToList();
                List<Service> c = db.Service.Where(c => c.ServiceName == Servisecombo.Text.ToString()).ToList();
                var date = calendar.SelectedDate.Value.Date;
                var time = PresetTimePicker.SelectedTime.Value.TimeOfDay;
                DateTime resultdate = date + time;
                if (Workercombo.Visibility == Visibility.Collapsed)
                {
                    List<FioId> a = db.FioId.Where(c => c.Fio == Workerfio).ToList();
                    ServiceSchedule newservice = new ServiceSchedule() { ServiceId = c[0].IdService, PatientId = b[0].PatientId, WorkerId = a[0].IdWorkers, ServiceDate = resultdate };
                    try
                    {
                        db.ServiceSchedule.Add(newservice);
                        db.SaveChanges();
                        MessageBox.Show("Запись добавленна!");
                        Services_List.ItemsSource = db.ServiceSchedule.Where(c => c.WorkerId == Workerid).ToList();
                        Services_List.Columns[4].Visibility = Visibility.Hidden;
                        Services_List.Columns[5].Visibility = Visibility.Hidden;
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
                else
                {
                    if (Workercombo.SelectedItem != null)
                    {
                        List<FioId> a = db.FioId.Where(c => c.Fio == Workercombo.Text.ToString()).ToList();
                        ServiceSchedule newservice = new ServiceSchedule() { ServiceId = c[0].IdService, PatientId = b[0].PatientId, WorkerId = a[0].IdWorkers, ServiceDate = resultdate };
                        try
                        {
                            db.ServiceSchedule.Add(newservice);
                            db.SaveChanges();
                            MessageBox.Show("Запись добавленна!");
                            Services_List.ItemsSource = db.ServiceSchedule.ToList();
                            Services_List.Columns[4].Visibility = Visibility.Hidden;
                            Services_List.Columns[5].Visibility = Visibility.Hidden;
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
                    else
                    {
                        MessageBox.Show("Выберите работника");
                        return;

                    }

                }
            }
            else
            {
                MessageBox.Show("Все поля обязательны к заполнению!");
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Services_List.Columns[4].Visibility = Visibility.Hidden;
            Services_List.Columns[5].Visibility = Visibility.Hidden;
        }

        private void Page_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                MessageBoxResult result = MessageBox.Show("Удалить информацию о данном пользователе из всех таблиц(Посещения,процедуры,)", "Потдверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    List<Object> a = Services_List.SelectedItems.Cast<Object>().ToList();
                    
                    a.ForEach(n => db.Remove(n));
                    db.SaveChanges();
                    if (Roleid != 3)
                    {
                        Services_List.IsReadOnly = true;
                        Services_List.ItemsSource = db.ServiceSchedule.Where(c => c.WorkerId == Workerid).ToList();
                    }
                    else
                    {
                        Services_List.ItemsSource = db.ServiceSchedule.ToList();
                    }
                    Services_List.Columns[4].Visibility = Visibility.Hidden;
                    Services_List.Columns[5].Visibility = Visibility.Hidden;
                }
                else
                {
                    return;
                }
            }
        }
    }
}
