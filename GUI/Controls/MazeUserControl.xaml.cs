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
        private ObservableCollection<Path> Rectangles = new ObservableCollection<Path>();

        public MazeUserControl()
        {
            InitializeComponent();


            var dfsMazeGenerator = new DFSMazeGenerator();
            var MyMaze = dfsMazeGenerator.Generate(15,15);
            var SM = new SearchableMaze(MyMaze);
           PointState startPoint =  new PointState(MyMaze.InitialPos);
            PointState endPoint = new PointState(MyMaze.GoalPos);


             int widthOfBlock = 40;
            int heightOfBlock = 40;
            
            for (int i = 0; i < MyMaze.Rows; i++)
             {
                for (int j = 0; j < MyMaze.Cols; j++)
                {

                    var rec = new Path
                    {
                        Data = new RectangleGeometry(new Rect(j * widthOfBlock, i * heightOfBlock, widthOfBlock, heightOfBlock)),
                        Fill = Brushes.Black,
                        Stroke = Brushes.Black,
                        StrokeThickness = 2
                    };

                    if (CellType.Free == MyMaze[i, j])
                    {
                        rec.Fill = Brushes.White;


                    }
                    if(new Position(i,j).Equals(MyMaze.InitialPos))
                    {
                        rec.Fill = Brushes.Blue;
                    }
                    if (new Position(i, j).Equals(MyMaze.GoalPos))
                    {
                        rec.Fill = Brushes.Red;
                    }
                    MyCanvas.Children.Add(rec);


                }
             }
            
         
            //icRectangles.ItemsSource = Rectangles;

            this.DataContext = this;
        }

      
    }
}