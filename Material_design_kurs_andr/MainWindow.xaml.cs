using Kurs_Andreev;
using System.Windows;

namespace Material_design_kurs_andr
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Content = new Entry_frame();
        }
    }
}
