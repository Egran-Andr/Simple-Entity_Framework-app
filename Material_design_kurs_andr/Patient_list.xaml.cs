using Material_design_kurs_andr.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Material_design_kurs_andr
{
    /// <summary>
    /// Логика взаимодействия для Patient_list.xaml
    /// </summary>
    public partial class Patient_list : Page
    {

        public int Roleid;
        public string Workerfio;
        public int Workerid;
        public HospitalkursContext db = HospitalkursContext.GetContext();
        public Patient_list(int id, string fio, int workerid)
        {
            Roleid = id;
            Workerfio = fio;
            Workerid = workerid;
            InitializeComponent();
            PatientList.ItemsSource = db.PatientInfo.ToList();
        }

        private void PatientList_PreviewKeyDown(object sender, KeyEventArgs e)//нажатие на кнопку delete(удаление строки)
        {

            if (e.Key == Key.Delete)
            {
                MessageBoxResult result = MessageBox.Show("Удалить информацию о данном пользователе из всех таблиц(Посещения,процедуры,)", "Потдверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    List<Object> a = PatientList.SelectedItems.Cast<Object>().ToList();
                    PatientInfo dataRowView = (PatientInfo)PatientList.SelectedItem;
                    int ID = Convert.ToInt32(dataRowView.PatientId);
                    MessageBox.Show(ID.ToString());
                    foreach (var c in db.PatientVisit.Where(e => e.PatientId == ID).ToList())//удаление из таблицы  посещений
                    {
                        foreach (var k in db.PatientProcedure.Where(e => e.PatientVisit == c.VizitPatId).ToList())//удаление посещений из таблицы назначенных процедур
                        {
                            db.PatientProcedure.Remove(k);
                            db.SaveChanges();
                        }
                        foreach (var b in db.VizitIll.Where(e => e.IdVizit == c.VizitPatId).ToList())//удаление списка болезней и посещений
                        {
                            db.VizitIll.Remove(b);
                            db.SaveChanges();
                        }
                        db.PatientVisit.Remove(c);
                        db.SaveChanges();
                    }
                    foreach (var c in db.ServiceSchedule.Where(e => e.PatientId == ID).ToList())//удаление из таблицы медицинских процедур
                    {
                        db.ServiceSchedule.Remove(c);
                        db.SaveChanges();
                    }
                    a.ForEach(n => db.Remove(n));
                    db.SaveChanges();
                    PatientList.ItemsSource = db.PatientInfo.ToList();
                }
                else
                {
                    e.Handled = true;
                    return;
                }
            }
        }

        private void GotoEntry_Click(object sender, RoutedEventArgs e)//Выход в меню
        {
            this.NavigationService.Navigate(new DoctorMenupage(Roleid, Workerfio, Workerid));
        }

        private void SaveChanges_Click(object sender, RoutedEventArgs e)//Сохранение изменений в таблице
        {
            PatientList.EndInit();
            MessageBoxResult result = MessageBox.Show("Сохранить изменения", "Потдверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                db.SaveChanges();
            }
            else
            {
                return;
            }

        }

        private void ToPatientAdd_Click(object sender, RoutedEventArgs e)//Добавление пациента
        {
            this.NavigationService.Navigate(new Patient_add(Roleid, Workerfio, Workerid));
        }
    }
}
