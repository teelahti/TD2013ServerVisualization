namespace VisualizationConsole
{
    using System;
    using System.Collections.Generic;

    using Microsoft.AspNet.SignalR.Client.Hubs;

    class Program
    {
        private readonly static Dictionary<string, Visualizer> Counters = new Dictionary<string, Visualizer>();

        private static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Give target server address as first parameter");
                return;
            }

            var connection = new HubConnection(args[0]);
            WireUp(connection);
            Console.ReadLine();
        }

        private static async void WireUp(HubConnection connection)
        {
            var hub = connection.CreateHubProxy("visualization");

            try
            {
                await connection.Start();
                Console.Clear();
                Console.WriteLine("Connected, waiting for data");

                await hub.Invoke<IEnumerable<Counter>>("GetCounters").ContinueWith(
                    task =>
                    {
                        int i = 5;

                        foreach (var c in task.Result)
                        {
                            Counters.Add(c.Name, new Visualizer(c.Name, i));
                            i += 2;
                        }

                        // All set up, listen for server side broadcast
                        hub.On("counterValueChanged", CounterValueChanged);
                    });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to start: {0}", ex.GetBaseException());
            }
        }

        private static void CounterValueChanged(dynamic counter)
        {
            Counters[(string)counter.Name].Update((int)counter.Value);
        }
    }
}
