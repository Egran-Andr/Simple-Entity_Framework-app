using Material_design_kurs_andr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Material_design_kurs_andr
{
    /// <summary>
    /// Логика взаимодействия для Worker_department.xaml
    /// </summary>
    public partial class Worker_department : Page
    {
        public int Roleid;
        public string Workerfio;
        public int Workerid;
        public HospitalkursContext db= HospitalkursContext.GetContext();
        public Worker_department(int id, string fio, int workerid)
        {
            Roleid = id;
            Workerfio = fio;
            Workerid = workerid;
            InitializeComponent();
            Department.ItemsSource = db.HospitalDepatment.Select(n => n.DepartmentName).ToList();
            Worker.ItemsSource = db.FioId.Select(n => n.Fio).ToList();
            DepartmentList.ItemsSource = db.WorkersDepartment.ToList();
            DepartmentList.IsReadOnly = true;
        }

        private void Department_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Department.SelectedIndex < 0)
            {
                Department.Text = "Выберите отдел:";
            }
            else
            {
                Department.Text = Department.SelectedItem.ToString();
                string department = Department.SelectedItem.ToString();
                List<HospitalDepatment> departmentlist = db.HospitalDepatment.Where(c => c.DepartmentName == department).ToList();
                int departmentid = departmentlist[0].Departmentid;
                DepartmentList.ItemsSource = db.WorkersDepartment.Where(n => n.DepartmentId == departmentid).ToList().AsParallel();
                DepartmentList.Columns[2].Visibility = Visibility.Hidden;
                DepartmentList.Columns[3].Visibility = Visibility.Hidden;
            }
        }

        private void Worker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Worker.SelectedIndex < 0)
            {
                Worker.Text = "Работника:";
            }
            else
            {
                Worker.Text = Worker.SelectedItem.ToString();
            }
        }

        private void GotoEntry_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new DoctorMenupage(Roleid, Workerfio, Workerid));
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DepartmentList.Columns[2].Visibility = Visibility.Hidden;
            DepartmentList.Columns[3].Visibility = Visibility.Hidden;
        }

        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            if (Department.Text != null && Worker.Text != null)
            {
                List<FioId> a = db.FioId.Where(c => c.Fio == Worker.Text.ToString()).ToList();
                List<HospitalDepatment> b = db.HospitalDepatment.Where(c => c.DepartmentName == Department.Text.ToString()).ToList();
                if (a.Count == 0 || b.Count == 0)
                {
                    MessageBox.Show("Поля не могут быть пустыми");
                }
                else
                {
                    WorkersDepartment newworker = new WorkersDepartment() { WorkerId = a[0].IdWorkers,DepartmentId = b[0].Departmentid};
                    try
                    {
                        db.Add(newworker);
                        db.SaveChanges();
                        MessageBox.Show("Сотрудник приписан к отделению");
                        DepartmentList.ItemsSource = db.WorkersDepartment.Where(n => n.DepartmentId == b[0].Departmentid).ToList();
                        DepartmentList.Columns[2].Visibility = Visibility.Hidden;
                        DepartmentList.Columns[3].Visibility = Visibility.Hidden;
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
            }
            else
            {
                MessageBox.Show("Все поля обязательны к заполнению!");
            }
        }
        private void Date_workers_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Worker_datework(Roleid, Workerfio, Workerid));
        }
    }
}
