namespace TD2013ServerVisualization.Visualization
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Threading;

    using Microsoft.AspNet.SignalR;
    using Microsoft.AspNet.SignalR.Hubs;

    public class RandomData : IData
    {
        private readonly static Lazy<RandomData> SingleInstance = new Lazy<RandomData>(() => new RandomData());

        private readonly Lazy<IHubConnectionContext> clientsInstance = new Lazy<IHubConnectionContext>(() => GlobalHost.ConnectionManager.GetHubContext<VisualizationHub>().Clients);

        private readonly ConcurrentDictionary<string, VisualizedCounter> counters = new ConcurrentDictionary<string, VisualizedCounter>();

        private const int UpdateInterval = 200;

        private Timer timer;

        private bool updating;

        private readonly static object UpdateLock = new object();

        public RandomData()
        {
            new List<VisualizedCounter>
                {
                    new VisualizedCounter("CPU"),
                    new VisualizedCounter("Network")
                }.ForEach(v => this.counters.TryAdd(v.Name, v));
        }

        public static RandomData Instance
        {
            get
            {
                return SingleInstance.Value;
            }
        }

        public IEnumerable<VisualizedCounter> Counters
        {
            get
            {
                return this.counters.Values;
            }
        }

        public void StartPublishing()
        {
            this.timer = new Timer(this.UpdateDummyData, null, UpdateInterval, UpdateInterval);
        }

        private void UpdateDummyData(object state)
        {
            if (this.updating)
            {
                return;
            }

            lock (UpdateLock)
            {
                if (!this.updating)
                {
                    this.updating = true;
                }

                foreach (var c in this.counters.Values)
                {
                    c.ChangeToNewRandomValue();
                    this.BroadcastNewCounterValue(c);
                }

                this.updating = false;
            }
        }

        public void EndPublishing()
        {
            // Concurrency problem here, do not care in demo
            if (this.timer != null)
            {
                this.timer.Dispose();
            }
        }

        private IHubConnectionContext Clients
        {
            get { return this.clientsInstance.Value; }
        }

        private void BroadcastNewCounterValue(VisualizedCounter counter)
        {
            this.Clients.All.counterValueChanged(counter);
        }
    }
}