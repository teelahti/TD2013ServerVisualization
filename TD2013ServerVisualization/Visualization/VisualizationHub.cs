namespace TD2013ServerVisualization.Visualization
{
    using System.Collections.Generic;

    using Microsoft.AspNet.SignalR;
    using Microsoft.AspNet.SignalR.Hubs;

    [HubName("visualization")]
    public class VisualizationHub: Hub
    {
        private readonly IData data;

        public VisualizationHub(): this(RandomData.Instance)
        {
        }

        public VisualizationHub(IData data)
        {
            this.data = data;
        }

        public IEnumerable<VisualizedCounter> GetCounters()
        {
            return this.data.Counters;
        }

        public void Start()
        {
            this.data.StartPublishing();
        }

        public void End()
        {
            this.data.EndPublishing();
        }
    }
}