using MazeLib;
using SearchAlgorithmsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex1
{
    class SearchableMaze : ISearchable
    {
        public Maze MyMaze { get; }
        private PointState GoalPosition;
        private PointState InitialPosition;

        public SearchableMaze()
        {
            MyMaze = new Maze(3, 3);
            GoalPosition = new PointState(MyMaze.GoalPos);
            InitialPosition = new PointState(MyMaze.InitialPos);
        }

        public List<State> getAllPossibleStates(State s)
        {
            MazeLib.Position currentPosition = (s as PointState).CurrentPosition;
            int currRow = currentPosition.Row;
            int currCol = currentPosition.Col;
            List<State> accessiblePositionStates = new List<State>();
            Position up = new Position(currRow + 1, currCol);
            Position down = new Position(currRow - 1, currCol);
            Position left = new Position(currRow, currCol - 1);
            Position right = new Position(currRow, currCol + 1);
            if (CellType.Free == MyMaze[currRow + 1, currCol])
                accessiblePositionStates.Add(new PointState(up));
            if (CellType.Free == MyMaze[currRow - 1, currCol])
                accessiblePositionStates.Add(new PointState(down));
            if (CellType.Free == MyMaze[currRow, currCol - 1])
                accessiblePositionStates.Add(new PointState(left));
            if (CellType.Free == MyMaze[currRow, currCol + 1])
                accessiblePositionStates.Add(new PointState(right));
            return accessiblePositionStates;
        }

        public State getGoalState()
        {
            return GoalPosition;
        }

        public State getInitialState()
        {
            return InitialPosition;
        }
    }
}
