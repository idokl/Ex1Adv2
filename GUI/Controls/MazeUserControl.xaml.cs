using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Adapter;
using GUI.Model;
using GUI.ViewModel;
using MazeGeneratorLib;
using MazeLib;

namespace GUI.Controls
{
    /// <summary>
    /// Interaction logic for MazeUserControl.xaml
    /// </summary>
    public partial class MazeUserControl : UserControl
    {
        //  private MazeUserControlViewModel mazeUserControlViewModel;
        private ObservableCollection<RectItem> RectItems { get; set; }

        public MazeUserControl()
        {
            InitializeComponent();
            this.draw();
            this.DataContext = this;
        }

        public void draw()
        {
            var dfsMazeGenerator = new DFSMazeGenerator();
            var MyMaze = dfsMazeGenerator.Generate(5, 5);
            //MyMaze.Name;
            var SM = new SearchableMaze(MyMaze);
            int size = MyMaze.Rows * MyMaze.Cols;
            this.RectItems = new ObservableCollection<RectItem>();

            double height = System.Windows.SystemParameters.PrimaryScreenHeight;
            double width = System.Windows.SystemParameters.PrimaryScreenWidth;
            int widthOfBlock = (int) width / MyMaze.Cols;
            int heightOfBlock = (int) height / MyMaze.Rows;
            for (int i = 0; i < MyMaze.Rows; i++)
            {
                for (int j = 0; j < MyMaze.Cols; j++)
                {
                    RectItem rec = new RectItem();
                    
                    if (CellType.Free == MyMaze[i, j])
                    {
                        rec.WhiteColor = true;
                    }
                    rec.Width = widthOfBlock;
                    rec.Height = heightOfBlock;
                    rec.X = j * widthOfBlock;
                    rec.Y = i * heightOfBlock;
                  

                    this.RectItems.Add(rec);
                }
            }
        }
    }
}