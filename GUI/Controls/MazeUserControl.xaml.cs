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
        private ObservableCollection<RectItem> RectItems = new ObservableCollection<RectItem>();
        private ObservableCollection<Rectangle> Rectangles = new ObservableCollection<Rectangle>();

        public MazeUserControl()
        {
            InitializeComponent();


            var dfsMazeGenerator = new DFSMazeGenerator();
            var MyMaze = dfsMazeGenerator.Generate(20, 20);
            //MyMaze.Name;
            var SM = new SearchableMaze(MyMaze);

            int size = MyMaze.Rows * MyMaze.Cols;
            Rectangle rect = new Rectangle();
            // rect.Width = 100;
            //rect.Height = 100;
             int widthOfBlock = 40;
            int heightOfBlock = 40;
           // double height = 100;//System.Windows.SystemParameters.PrimaryScreenHeight;
            //double width = 100;//System.Windows.SystemParameters.PrimaryScreenWidth;
           // int widthOfBlock = (int)width / MyMaze.Cols;
            //int heightOfBlock = (int)height / MyMaze.Rows;
            for (int i = 0; i < MyMaze.Rows; i++)
            {
                for (int j = 0; j < MyMaze.Cols; j++)
                {
                    Rectangle rec = new Rectangle();
                    rec.Width = widthOfBlock;
                    rec.Height = heightOfBlock;
                    if (CellType.Free == MyMaze[i, j])
                    {
                        rec.Fill = new SolidColorBrush(System.Windows.Media.Colors.White);
                        rec.Stroke = new SolidColorBrush(System.Windows.Media.Colors.Black);
                    }
                    rec.Fill = new SolidColorBrush(System.Windows.Media.Colors.Black);
                    this.Rectangles.Add(rec);

                }
            }
            /*
                    for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Rectangle rec = new Rectangle();

                    
                    rec.Width = widthOfBlock;
                    rec.Height = heightOfBlock;
                    rec.Fill = new SolidColorBrush(System.Windows.Media.Colors.Black);
                    //rec.X = j * widthOfBlock;
                    //rec.Y = i * heightOfBlock;


                    this.Rectangles.Add(rec);
                }
            }*/
            icRectangles.ItemsSource = Rectangles;


            //this.draw();
            this.DataContext = this;
        }

        public void draw()
        {
            var dfsMazeGenerator = new DFSMazeGenerator();
            var MyMaze = dfsMazeGenerator.Generate(5, 5);
            //MyMaze.Name;
            var SM = new SearchableMaze(MyMaze);
            int size = MyMaze.Rows * MyMaze.Cols;
            //this.RectItems = new ObservableCollection<RectItem>();

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