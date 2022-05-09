using Material_design_kurs_andr.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Kurs_Andreev
{
    /// <summary>
    /// Логика взаимодействия для Entry_frame.xaml
    /// </summary>
    public partial class Entry_frame : Page
    {
        public Entry_frame()
        {
            InitializeComponent();
        }
        private const string salt = "Password";
        private const int IterationCount = 100000;
        private const int NumBytesRequested = 256 / 8;
        private const KeyDerivationPrf hMACSHA256 = KeyDerivationPrf.HMACSHA256;
        int[] array = { 1, 2, 7, 8, 9,10 };

        public static string HashPassword(string password)
        {
            byte[] saltBytes = Convert.FromBase64String(salt);

            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password, salt: saltBytes, prf: hMACSHA256, iterationCount: IterationCount, numBytesRequested: NumBytesRequested));
        }

        private void GotoReg_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Registration.xaml", UriKind.Relative));//Переход на страницу входа
        }

        private void entry_Click(object sender, RoutedEventArgs e)
        {
            if (Login.Text != "" & Password.Password != "")//Все поля заполнены пользователем
            {
                var db = HospitalkursContext.GetContext();
                string login = Login.Text.ToString();
                string pas = Password.Password.ToString();
                List<FioId> a = db.FioId.Where(c => c.Fio == login).ToList();
                if (a.Count == 0)
                {
                    MessageBox.Show("Данный человек не работает в данной больнице!");
                }
                else
                {
                    List<LoginStorage> passwords = db.LoginStorage.Where(c => c.WorkerId == a[0].IdWorkers && c.Password ==HashPassword(pas)).ToList();
                    if (passwords.Count == 0) MessageBox.Show("Проверьте правильность ввода!");
                    else if (passwords[0].AccountVerification == "-")
                    {
                        MessageBox.Show("Дождитесь потдверждение администратора!");
                    }
                    else {
                        List<Workers> worker = db.Workers.Where(c => c.IdWorkers == a[0].IdWorkers).ToList();
                        bool result = array.Any(n => n == worker[0].WorkerPosition);
                        if (worker[0].WorkerPosition == 11) this.NavigationService.Navigate(new Uri("Administration.xaml", UriKind.Relative));
                        else if (worker[0].WorkerPosition == 3) MessageBox.Show("Вы главврач!");
                        else if (result==true) MessageBox.Show("Вы врач!");
                        MessageBox.Show("Добро пожаловать"+" "+a[0].Fio);
                    }
                }

            }
            else//не все поля заполнены
            {
                MessageBox.Show("Все поля обязательны к заполнению");
            }
        }
    }
}
