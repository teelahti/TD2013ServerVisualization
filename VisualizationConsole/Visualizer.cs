namespace VisualizationConsole
{
    using System;

    public class Visualizer
    {
        private const decimal MaxIncomingValue = 100;
        private readonly decimal charsPerValueUnit = (Console.WindowWidth - 10) / MaxIncomingValue;

        private const char BarCharacter = '#';

        public string Name { get; private set; }

        public int Line { get; private set; }

        public Visualizer(string name, int line)
        {
            this.Name = name;
            this.Line = line;
        }

        public void Update(int value)
        {
            // Clear line
            Console.SetCursorPosition(0, this.Line);
            for (var i = 0; i < Console.WindowWidth; i++)
            {
                Console.Write(" ");
            }

            // Calculate bar length
            var barDisplayLength = (int) (this.charsPerValueUnit * value);
            var bar = new string(BarCharacter, barDisplayLength);

            // Show new line
            Console.SetCursorPosition(0, this.Line);
            Console.WriteLine("{0,9} {1}", this.Name, bar);
        }
    }
}