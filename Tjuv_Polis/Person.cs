using System;

namespace Tjuv_Polis
{
    internal class Person
    {
        public int ID { get; set; }
        public int XPosition { get; set; }
        public int YPosition { get; set; }
        public int MovementX { get; set; }
        public int MovementY { get; set; }
        public int HorizontalSpace { get; set; }
        public int VerticalSpace { get; set; }
        public char Symbol { get; set; }
        public ConsoleColor Color { get; set; }
        public Inventory Inventory { get; set; }

        public Person(int horizontalSpace, int verticalSpace, int iD, Inventory inventory)
        {
            HorizontalSpace = horizontalSpace;
            VerticalSpace = verticalSpace;

            Random random = new Random();
            XPosition = random.Next(2, horizontalSpace - 1);
            YPosition = random.Next(2, verticalSpace - 1);
            MovementX = random.Next(-1, 2);
            MovementY = random.Next(-1, 2);

            ID = iD;
            Inventory = inventory;
        }

        public void Move()
        {
            Console.SetCursorPosition(XPosition, YPosition);
            Console.Write(" ");
            // Rensa tidigare position genom att skriva ut ett blanksteg
            Console.SetCursorPosition(XPosition, YPosition);
            Console.Write(' '); // Ritar ut ett blanksteg där personen tidigare var.

            int newXPosition = XPosition + MovementX;
            int newYPosition = YPosition + MovementY;

            if (newXPosition < 1)
            {
                newXPosition = HorizontalSpace - 2;
            }
            if (newYPosition < 1)
            {
                newYPosition = VerticalSpace - 1;
            }
            if (newYPosition >= VerticalSpace)
            {
                newYPosition = 2;
            }
            if (newXPosition >= HorizontalSpace - 1)
            {
                newXPosition = 2;
            }

            XPosition = newXPosition;
            YPosition = newYPosition;

            // Rita personen på nya positionen
            Console.SetCursorPosition(XPosition, YPosition);
            Console.ForegroundColor = Color;
            Console.Write(Symbol);
            Console.ResetColor();
        }
        public virtual string Status()
        {
            // Kontrollera om inventariet har några föremål
            string inventoryStatus = Inventory.Items.Count > 0
                ? string.Join(", ", Inventory.Items.Select(item => item.KindOfItem))
                : "Empty inventory";

            return $"{GetType().Name} {ID}: [{inventoryStatus}] ({XPosition}, {YPosition})";
        }
    }

    class Civilian : Person
    {
        public Civilian(int horizontalSpace, int verticalSpace, int iD) : base(horizontalSpace, verticalSpace, iD, new Inventory())
        {
            Symbol = 'C';
            Color = ConsoleColor.Green;

        }
            }

    class Thief : Person
    {
        public Thief(int horizontalSpace, int verticalSpace, int iD) : base(horizontalSpace, verticalSpace, iD, new Inventory())
        {
            Symbol = 'T';
            Color = ConsoleColor.Red;
        }
    }

    class Police : Person
    {
        public Police(int horizontalSpace, int verticalSpace, int iD) : base(horizontalSpace, verticalSpace, iD, new Inventory())
        {
            Symbol = 'P';
            Color = ConsoleColor.Blue;
        }
    }
}
