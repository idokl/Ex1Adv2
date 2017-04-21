namespace SearchAlgorithmsLib
{
    public interface ISearcher
    {
        // the search method
        Solution search(ISearchable searchable);

        // get how many nodes were evaluated by the algorithm
        int getNumberOfNodesEvaluated();
    }
}