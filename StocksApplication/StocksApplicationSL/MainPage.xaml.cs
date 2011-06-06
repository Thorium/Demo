using System.Windows.Controls;

namespace StocksApplicationSL
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
            this.DataContext = new StocksViewModel.MainViewModel();
        }
    }
}
