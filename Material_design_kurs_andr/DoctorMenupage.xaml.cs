using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для DoctorMenupage.xaml
    /// </summary>
    public partial class DoctorMenupage : Page
    {
        public int Roleid;
        public string Workerfio;
        public int Workerid;
        public DoctorMenupage(int id, string fio,int workerid)
        {
            InitializeComponent();
            Roleid = id;
            Workerfio = fio;
            Workerid = workerid;
            if (Roleid !=3)
            {
                Worker_department.Visibility = Visibility.Collapsed;
                Worker_add.Visibility = Visibility.Collapsed;
            }
            
        }

        private void BackToEntry_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Entry_frame.xaml", UriKind.Relative));//Переход на страницу входа
        }

        private void RegistrationAcsess_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(Roleid.ToString() + " " + Workerfio+" "+Workerid.ToString());
        }

        private void Doctor_assigments_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Doctor_apointments(Roleid, Workerfio, Workerid));
        }

        private void Worker_department_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Worker_department(Roleid, Workerfio, Workerid));
        }

        private void Worker_add_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
