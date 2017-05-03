using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GUI.Model;

namespace GUI.ViewModel
{
    class MazeUserControlViewModel
    {
        public ObservableCollection<RectItem> RectItems { get; set; }
        private MazeUserControlModel mazeUserControlModel;

        public MazeUserControlViewModel()
        {
            this.mazeUserControlModel = new MazeUserControlModel(this);
        }
    }
}
