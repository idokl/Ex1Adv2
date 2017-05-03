using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adapter;
using MazeGeneratorLib;
using MazeLib;
using GUI.ViewModel;

namespace GUI.Model
{
    class MazeUserControlModel
    {
        private MazeUserControlViewModel mazeUserControlViewModel;
        public MazeUserControlModel(MazeUserControlViewModel mazeUserControlViewModel)
        {
            this.mazeUserControlViewModel = mazeUserControlViewModel;
        }

        public void init()
        {
            var dfsMazeGenerator = new DFSMazeGenerator();
            var MyMaze = dfsMazeGenerator.Generate(20, 20);
            //MyMaze.Name;
            var SM = new SearchableMaze(MyMaze);
            int size = MyMaze.Rows * MyMaze.Cols;
            this.mazeUserControlViewModel.RectItems = new ObservableCollection<RectItem>();

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
                    rec.Y = i = heightOfBlock;
                    this.mazeUserControlViewModel.RectItems.Add(rec);
                }
            }
        }
    }
}
