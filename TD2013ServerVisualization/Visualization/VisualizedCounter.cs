namespace TD2013ServerVisualization.Visualization
{
    using System;

    public class VisualizedCounter
    {
        private static readonly Random Randomizer = new Random();

        public string Name { get; private set; }
        public int Value { get; set; }

        public VisualizedCounter(string name)
        {
            this.Name = name;
        }

        public void ChangeToNewRandomValue()
        {
            const int Limit = 100;
            const int Variance = 30;

            int start = 0;
            int end = Limit;

            // If this is the first value, take random from the whole range
            if (this.Value == 0)
            {
                this.Value = Randomizer.Next(start, end);
                return;
            }

            start = this.Value - Variance;
            end = this.Value + Variance;

            if (start <= 0)
            {
                start = 1;
                end = end + Math.Abs(start);
            }

            if (end > Limit)
            {
                start = start - (end - Limit);
                end = Limit;
            }

            this.Value = Randomizer.Next(start, end);
        }
    }
}