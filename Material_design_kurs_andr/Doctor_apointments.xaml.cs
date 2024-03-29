﻿using Material_design_kurs_andr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
        public HospitalkursContext db = HospitalkursContext.GetContext();
        public Doctor_apointments(int id, string fio, int workerid)
        {
            InitializeComponent();
            WorkerFio = fio;
            Workerid = workerid;
            Roleid = id;
            RecordList.ItemsSource = db.Records.Where(c => c.WorkerRecord == workerid).ToList();
            RecordList.IsReadOnly = true;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            RecordList.Columns[0].Visibility = Visibility.Hidden;
            RecordList.Columns[3].Visibility = Visibility.Hidden;
        }



        private void GetWorkerTicket_Click(object sender, RoutedEventArgs e)
        {
            DateTime MyDateTime = ((DateTime)calendar.SelectedDate).Date.Add(((DateTime)PresetTimePicker.SelectedTime).TimeOfDay);//Получение даты из даты календаря и Времени
            if (PresetTimePicker.SelectedTime != null && OMC.Text != null)
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

        public static bool StringIsValid(string str)//Выражение для полиса ОМС
        {
            return !Regex.IsMatch(str, @"^[0-9]*$") || str.Length < 16;
        }

        private void RecordList_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Delete)//Delete
                {
                    MessageBoxResult result = MessageBox.Show("Удалить выделенные элементы?", "Потдверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        List<Object> a = RecordList.SelectedItems.Cast<Object>().ToList();
                        a.ForEach(n => db.Remove(n));
                        db.SaveChanges();
                        RecordList.ItemsSource = db.Records.Where(c => c.WorkerRecord == Workerid).ToList();
                    }
                    else
                    {
                        e.Handled = true;
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
            this.NavigationService.Navigate(new DoctorMenupage(Roleid, WorkerFio, Workerid));//Переход к меню
        }
    }
}
