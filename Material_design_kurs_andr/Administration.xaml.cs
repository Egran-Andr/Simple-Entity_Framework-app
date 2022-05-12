﻿using Material_design_kurs_andr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Kurs_Andreev
{
    /// <summary>
    /// Логика взаимодействия для Administration.xaml
    /// </summary>
    public partial class Administration : Page
    {
        public Administration()
        {
            InitializeComponent();
            var db = HospitalkursContext.GetContext();
            AccountList.ItemsSource = db.LoginStorage.ToList();
        }

        private void GotoEntry_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Entry_frame.xaml", UriKind.Relative));//Переход на страницу входа
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            AccountList.Columns[3].Visibility = Visibility.Hidden;
            AccountList.Columns[1].IsReadOnly=true;
        }

        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            var db = HospitalkursContext.GetContext();
            AccountList.EndInit();
            MessageBoxResult result = MessageBox.Show("Сохранить изменения", "Потдверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                List<Object> a = AccountList.Items.Cast<Object>().ToList();
                db.SaveChanges();
            }
            else
            {
                return;
            }

        }

        private void AccountList_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            var db = HospitalkursContext.GetContext();
            try
            {
                if (e.Key == Key.Delete)
                {
                    MessageBoxResult result = MessageBox.Show("Удалить выделенные элементы?", "Потдверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        List<Object> a = AccountList.SelectedItems.Cast<Object>().ToList();
                        a.ForEach(n => db.Remove(n));
                        db.SaveChanges();
                    }
                    else
                    {
                        return;
                    }
                }
            }
            catch (System.InvalidOperationException){
                MessageBox.Show("Ошибка. Изменения не были сохранены");
                return;
            }
            
        }
    }
}
