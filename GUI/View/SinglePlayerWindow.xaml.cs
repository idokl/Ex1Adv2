using GUI.Model;
using GUI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GUI
{
    /// <summary>
    /// Interaction logic for SinglePlayer.xaml
    /// </summary>
    public partial class SinglePlayerWindow : Window
    {
        private SinglePlayerViewModel spVM;

        public SinglePlayerWindow()
        {
            InitializeComponent();
            spVM = new SinglePlayerViewModel();
            this.DataContext = spVM;
        }

        private void StartGameButton_Click(object sender, RoutedEventArgs e)
        {
           
        }
    }
}
