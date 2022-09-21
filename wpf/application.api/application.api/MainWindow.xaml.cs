using System.Windows;

namespace application.api
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.MouseLeftButtonDown += delegate { DragMove(); };
            InitializeComponent();
        }
    }
}
