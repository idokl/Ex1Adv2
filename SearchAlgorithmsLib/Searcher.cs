namespace SearchAlgorithmsLib
{
    public abstract class Searcher : ISearcher
    {
        public Searcher()
        {
            evaluatedNodes = 0;
        }

        public int evaluatedNodes { get; set; }

        // ISearcher’s methods:
        public int getNumberOfNodesEvaluated()
        {
            return evaluatedNodes;
        }

        public abstract Solution search(ISearchable searchable);
    }
}