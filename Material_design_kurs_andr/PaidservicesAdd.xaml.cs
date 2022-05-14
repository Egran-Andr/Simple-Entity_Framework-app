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
        public PaidservicesAdd(int id, string fio, int workerid)
        {
            var db = HospitalkursContext.GetContext();
            InitializeComponent();
            Roleid = id;
            Workerfio = fio;
            Workerid = workerid;
            if (Roleid != 3)
            {
                Services_List.IsReadOnly = true;
                Services_List.ItemsSource= db.ServiceSchedule.Where(c => c.WorkerId == workerid).ToList();
                Workercombo.Visibility = Visibility.Collapsed;
                WorkerLabel.Visibility = Visibility.Collapsed;
            }
            else
            {
                Services_List.ItemsSource = db.ServiceSchedule.ToList();
            }
            Workercombo.ItemsSource = db.FioId.Select(n => n.Fio).ToList();
            PatientCombo.ItemsSource =db.PatientInfo.Select(n=>n.PatientName).ToList();
            Servisecombo.ItemsSource =db.Service.Select(n=>n.ServiceName).ToList();
        }

        private void ToMenuPage_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new DoctorMenupage(Roleid, Workerfio, Workerid));
        }
    }
}
