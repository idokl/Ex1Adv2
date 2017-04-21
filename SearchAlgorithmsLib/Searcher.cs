// ***********************************************************************
// Assembly         : SearchAlgorithmsLib
// Author           : IDO1
// Created          : 04-05-2017
//
// Last Modified By : IDO1
// Last Modified On : 04-21-2017
// ***********************************************************************
// <copyright file="Searcher.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// Class Searcher.
    /// </summary>
    /// <seealso cref="SearchAlgorithmsLib.ISearcher" />
    public abstract class Searcher : ISearcher
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Searcher"/> class.
        /// </summary>
        public Searcher()
        {
            evaluatedNodes = 0;
        }

        /// <summary>
        /// Gets or sets the evaluated nodes.
        /// </summary>
        /// <value>The evaluated nodes.</value>
        public int evaluatedNodes { get; set; }

        // ISearcher’s methods:
        /// <summary>
        /// Gets the number of nodes evaluated.
        /// </summary>
        /// <returns>System.Int32.</returns>
        public int getNumberOfNodesEvaluated()
        {
            return evaluatedNodes;
        }

        /// <summary>
        /// Searches the specified searchable.
        /// </summary>
        /// <param name="searchable">The searchable.</param>
        /// <returns>Solution.</returns>
        public abstract Solution search(ISearchable searchable);
    }
}