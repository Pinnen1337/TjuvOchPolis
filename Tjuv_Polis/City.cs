using System;
namespace Tjuv_Polis
{
    public class City
    {
        public int HorisontalWallLength { get; set; }
        public int VerticalWallLength { get; set; }
        public List<Person> PersonsInCity { get; set; }

    public City(int horisontalSize, int verticalSize, List<Person> personsInCity)
        {
            HorisontalWallLength = horisontalSize;
            VerticalWallLength = verticalSize;
            PersonsInCity = personsInCity;
        }

        public void Move()
        {
            foreach (Person person in PersonsInCity)
            {
            Console.SetCursorPosition(person.XPosition, person.YPosition);
            Console.Write(' '); // Ritar ut ett blanksteg där personen tidigare var.

            int newXPosition = person.XPosition + person.MovementX;
            int newYPosition = person.YPosition + person.MovementY;

            if (newXPosition < 1)
            {
                newXPosition = person.HorizontalSpace - 2;
            }
            if (newYPosition < 1)
            {
                newYPosition = person.VerticalSpace - 1;
            }
            if (newYPosition >= person.VerticalSpace)
            {
                newYPosition = 2;
            }
            if (newXPosition >= person.HorizontalSpace - 1)
            {
                newXPosition = 2;
            }

            person.XPosition = newXPosition;
            person.YPosition = newYPosition;

            // Rita personen på nya positionen
            Console.SetCursorPosition(person.XPosition, person.YPosition);
            Console.ForegroundColor = person.Color;
            Console.Write(person.Symbol);
            Console.ResetColor();

            }
        }

        public void DrawCity()
        {
            DrawTopWall();
            DrawLowerWall();
            DrawWalls();
        }

        private void DrawTopWall()
        {
            Console.SetCursorPosition(0, 0);
            for (int i = 0; i < HorisontalWallLength; i++)
            {
                Console.Write("=");
            }
        }
        private void DrawLowerWall()
        {
            Console.SetCursorPosition(0, VerticalWallLength + 1);
            for (int i = 0; i < HorisontalWallLength; i++)
            {
                Console.Write("="); //Golv
            }
        }

        private void DrawWalls()
        {
            int cursorPositionLeft = 0;
            int cursorPositionTop = 1;
            for (int wall = 0; wall < VerticalWallLength; wall++)
            {
                Console.SetCursorPosition(cursorPositionLeft, cursorPositionTop); 
                Console.Write("░");  //Vänster vägg
                Console.SetCursorPosition(cursorPositionLeft + HorisontalWallLength -1, cursorPositionTop);
                Console.WriteLine("░"); //Höger vägg
                cursorPositionTop++;
            }
        }
    }

}

