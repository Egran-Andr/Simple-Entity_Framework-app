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
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Page
    {
        private const string salt = "Password";
        private const int IterationCount = 100000;
        private const int NumBytesRequested = 256 / 8;
        private const KeyDerivationPrf hMACSHA256 = KeyDerivationPrf.HMACSHA256;
        public HospitalkursContext db = HospitalkursContext.GetContext();

        public static string HashPassword(string password)
        {
            byte[] saltBytes = Convert.FromBase64String(salt);

            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password, salt: saltBytes, prf: hMACSHA256, iterationCount: IterationCount, numBytesRequested: NumBytesRequested));
        }

        public Registration()
        {
            InitializeComponent();
        }


        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Entry_frame.xaml", UriKind.Relative));//Переход на страницу входа
        }

        private void RegistrationAcsess_Click(object sender, RoutedEventArgs e)
        {
            if (Login.Text != "" & Password.Password != "" & PasswordRepeat.Password != "")//Все поля заполнены пользователем
            {

                if ((Password.Password.ToString()) != (PasswordRepeat.Password.ToString()))//Введеный пароль и подтвержденный пароль не совпадают
                {
                    MessageBox.Show("Пароли не совпадают. Проверьте правильность ввода");
                }
                else
                {
                    string login = Login.Text.ToString();
                    string pas = Password.Password.ToString();
                    List<FioId> a = db.FioId.Where(c => c.Fio == login).ToList();
                    if(a.Count==0) {
                        MessageBox.Show("Данный человек не работает в данной больнице!");
                    }
                    else{
                        LoginStorage user = new LoginStorage() {WorkerId=a[0].IdWorkers,Password=HashPassword(pas),AccountVerification="-"};
                        try { 
                        db.LoginStorage.AddRange(user);
                        db.SaveChanges();
                        MessageBox.Show("Аккаунт был создан. Дождитесь потдверждение администратора");
                        this.NavigationService.Navigate(new Uri("Entry_frame.xaml", UriKind.Relative));//Переход на страницу входа
                        }
                        catch(Microsoft.EntityFrameworkCore.DbUpdateException)
                        {
                            MessageBox.Show("Данный сотрудник уже зарегестрирован в базе");
                        }
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
