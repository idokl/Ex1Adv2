using System.Collections.Generic;
using MazeLib;
using SearchAlgorithmsLib;

namespace Adapter
{
    public class SearchableMaze : ISearchable
    {
        private readonly PointState GoalPosition;
        private readonly PointState InitialPosition;

        public SearchableMaze(Maze maze)
        {
            MyMaze = maze;
            GoalPosition = new PointState(maze.GoalPos);
            InitialPosition = new PointState(maze.InitialPos);
        }

        public Maze MyMaze { get; }

        public List<State> getAllPossibleStates(State s)
        {
            var currentPosition = (s as PointState).CurrentPosition;
            var currRow = currentPosition.Row;
            var currCol = currentPosition.Col;
            var accessiblePositionStates = new List<State>();
            Position up, down, right, left;
            var costOfNeighbor = s.Cost + 1;
            if (currRow + 1 != MyMaze.Rows)
            {
                down = new Position(currRow + 1, currCol);
                if (CellType.Free == MyMaze[currRow + 1, currCol])
                    accessiblePositionStates.Add(new PointState(down, costOfNeighbor));
            }
            if (currRow != 0)
            {
                up = new Position(currRow - 1, currCol);
                if (CellType.Free == MyMaze[currRow - 1, currCol])
                    accessiblePositionStates.Add(new PointState(up, costOfNeighbor));
            }
            if (currCol != 0)
            {
                left = new Position(currRow, currCol - 1);
                if (CellType.Free == MyMaze[currRow, currCol - 1])
                    accessiblePositionStates.Add(new PointState(left, costOfNeighbor));
            }
            if (currCol + 1 != MyMaze.Cols)
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
            return MyMaze.ToString() == sm.MyMaze.ToString()
                   && GoalPosition.ToString() == sm.GoalPosition.ToString()
                   && InitialPosition.ToString() == sm.InitialPosition.ToString();
        }

        public override bool Equals(object sm)
        {
            return Equals(sm as SearchableMaze);
        }

        public override int GetHashCode()
        {
            return MyMaze.GetHashCode() * 101 + GoalPosition.GetHashCode() * 103 + InitialPosition.GetHashCode() * 107;
        }
    }
}