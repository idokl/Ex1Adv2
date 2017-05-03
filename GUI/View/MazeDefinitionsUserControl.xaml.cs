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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GUI.View
{
    /// <summary>
    /// Interaction logic for MazeDefinitionsUserControl.xaml
    /// </summary>
    public partial class MazeDefinitionsUserControl : UserControl
    {
        public MazeDefinitionsUserControl()
        {
            InitializeComponent();
        }
    }
}
// before         </Grid>
// <Button x:Name="btnStart" Grid.Column="0" Grid.Row="3" HorizontalAlignment="Center" Grid.ColumnSpan="2" Click="StartGameButton_Click" Margin="5" Padding="5">Start Game</Button>