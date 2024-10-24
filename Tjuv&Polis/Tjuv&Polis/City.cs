using System;
namespace Tjuv_Polis
{
    public class City
    {
        public int HorisontalWallLength { get; set; }
        public int VerticalWallLength { get; set; }

        public City(int horisontalSize, int verticalSize)
        {
            HorisontalWallLength = horisontalSize;
            VerticalWallLength = verticalSize;
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

