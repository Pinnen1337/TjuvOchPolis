using System;

namespace Tjuv_Polis
{
    internal class Person
    {
        public int XPosition { get; set; }
        public int YPosition { get; set; }
        public int MovementX { get; set; }
        public int MovementY { get; set; }
        public char Symbol { get; set; }
        public ConsoleColor Color { get; set; }

        public Person(int horizontalSpace, int verticalSpace)
        {
            Random random = new Random();
            XPosition = random.Next(2, horizontalSpace - 1);
            YPosition = random.Next(2, verticalSpace - 1);
            MovementX = random.Next(-1, 2);
            MovementY = random.Next(-1, 2);
        }

        public void Move()
        {
            XPosition = XPosition + MovementX;
            YPosition = YPosition + MovementY;
        } 
    }

    class Civilian : Person
    {
        public Civilian(int horizontalSpace, int verticalSpace) : base(horizontalSpace, verticalSpace)
        {
            Symbol = 'C';
            Color = ConsoleColor.Green;
        }
    }

    class Thief : Person
    {
        public Thief(int horizontalSpace, int verticalSpace) : base(horizontalSpace, verticalSpace)
        {
            Symbol = 'T';
            Color = ConsoleColor.Red;
        }
    }

    class Police : Person
    {
        public Police(int horizontalSpace, int verticalSpace) : base(horizontalSpace, verticalSpace)
        {
            Symbol = 'P';
            Color = ConsoleColor.Blue;
        }
    }
}
