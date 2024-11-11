using System;
namespace Tjuv_Polis
{
    public class PoliceStation
    {
        public int HorisontalWallLength { get; set; }
        public int VerticalWallLength { get; set; }

        public PoliceStation(int horisontalSize, int verticalSize)
        {
            HorisontalWallLength = horisontalSize;
            VerticalWallLength = verticalSize;
        }

        public void DrawPoliceStation(City city, Prison prison, PoorHouse poorHouse)
        {
            DrawTopWall(city, prison, poorHouse);
            DrawLowerWall(city, prison, poorHouse);
            DrawWalls(city, prison, poorHouse);
        }

        private void DrawTopWall(City city, Prison prison, PoorHouse poorHouse)
        {
            Console.SetCursorPosition(city.HorisontalWallLength + 1, prison.VerticalWallLength + 3 + poorHouse.VerticalWallLength + 3);
            Console.WriteLine("PoliceStation");
            Console.SetCursorPosition(city.HorisontalWallLength + 1, prison.VerticalWallLength + 3 + poorHouse.VerticalWallLength + 4);
            for (int i = 0; i < HorisontalWallLength; i++)
            {
                Console.Write("=");
            }
        }
        private void DrawLowerWall(City city, Prison prison, PoorHouse poorHouse)
        {
            Console.SetCursorPosition(city.HorisontalWallLength + 1, VerticalWallLength + prison.VerticalWallLength + 4 + poorHouse.VerticalWallLength + 4);
            for (int i = 0; i < HorisontalWallLength; i++)
            {
                Console.Write("="); //Golv
            }
        }

        private void DrawWalls(City city, Prison prison, PoorHouse poorHouse)
        {
            int cursorPositionLeft = city.HorisontalWallLength + 1;
            int cursorPositionTop = prison.VerticalWallLength + 4 + poorHouse.VerticalWallLength + 4;
            for (int wall = 0; wall < VerticalWallLength; wall++)
            {
                Console.SetCursorPosition(cursorPositionLeft, cursorPositionTop);
                Console.Write("░");  //Vänster vägg
                Console.SetCursorPosition(cursorPositionLeft + HorisontalWallLength - 1, cursorPositionTop);
                Console.WriteLine("░"); //Höger vägg
                cursorPositionTop++;
            }
        }
    }

}