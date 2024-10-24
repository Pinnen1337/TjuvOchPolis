using System;

namespace Tjuv_Polis
{
    internal class Person
    {
        public int XPosition { get; set; }
        public int YPosition { get; set; }
        public char Symbol { get; set; }
        public ConsoleColor Color { get; set; }

        public Person()
        {
            Random random = new Random();
            XPosition = random.Next(2, 99);
            YPosition = random.Next(2, 24);
        }
    }

    class Civilian : Person
    {
        public Civilian() : base()
        {
            Symbol = 'C';
            Color = ConsoleColor.Green;
        }
    }

    class Thief : Person
    {
        public Thief() : base()
        {
            Symbol = 'T';
            Color = ConsoleColor.Red;
        }
    }

    class Police : Person
    {
        public Police() : base()
        {
            Symbol = 'P';
            Color = ConsoleColor.Blue;
        }
    }
}
