namespace TD2013ServerVisualization.Visualization
{
    using System.Collections.Generic;

    public interface IData
    {
        void StartPublishing();

        void EndPublishing();

        IEnumerable<VisualizedCounter> Counters { get; }
    }
}