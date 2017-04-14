using MazeLib;
using SearchAlgorithmsLib;
using System.Collections.Generic;

namespace Adapter
{
    public class SearchableMaze : ISearchable
    {
        public Maze MyMaze { get; }
        private PointState GoalPosition;
        private PointState InitialPosition;

        public SearchableMaze(Maze maze)
        {
            this.MyMaze = maze;
            GoalPosition = new PointState(maze.GoalPos);
            InitialPosition = new PointState(maze.InitialPos);
        }
        
        public List<State> getAllPossibleStates(State s)
        {
            MazeLib.Position currentPosition = (s as PointState).CurrentPosition;
            int currRow = currentPosition.Row;
            int currCol = currentPosition.Col;
            List<State> accessiblePositionStates = new List<State>();
            Position up, down, right, left;
            double costOfNeighbor = s.Cost + 1;
            if (!((currRow + 1) == MyMaze.Rows))
            {
                down = new Position(currRow + 1, currCol);
                if (CellType.Free == MyMaze[currRow + 1, currCol])
                    accessiblePositionStates.Add(new PointState(down, costOfNeighbor));
            }
            if (!((currRow) == 0))
            {
                up = new Position(currRow - 1, currCol);
                if (CellType.Free == MyMaze[currRow - 1, currCol])
                    accessiblePositionStates.Add(new PointState(up, costOfNeighbor));
            }
            if (!((currCol) == 0))
            {
                left = new Position(currRow, currCol - 1);
                if (CellType.Free == MyMaze[currRow, currCol - 1])
                    accessiblePositionStates.Add(new PointState(left, costOfNeighbor));
            }
            if (!((currCol+1) == MyMaze.Cols))
            {
                right = new Position(currRow, currCol + 1);
                if (CellType.Free == MyMaze[currRow, currCol + 1])
                    accessiblePositionStates.Add(new PointState(right, costOfNeighbor));
            }

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

        public bool Equals(SearchableMaze sm)
        {
            return ((this.MyMaze.ToString() == sm.MyMaze.ToString())
                && (this.GoalPosition.ToString() == sm.GoalPosition.ToString())
                && (this.InitialPosition.ToString() == sm.InitialPosition.ToString()));
        }

        public override bool Equals(object sm) => Equals(sm as SearchableMaze);

        public override int GetHashCode() => MyMaze.GetHashCode() * 101 + GoalPosition.GetHashCode() * 103 + InitialPosition.GetHashCode() * 107;
    }
}
