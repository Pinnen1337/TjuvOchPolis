using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tjuv_Polis
{
    public class PoorHouse
    {
        public int HorisontalWallLength { get; set; }
        public int VerticalWallLength { get; set; }

        public PoorHouse(int horisontalSize, int verticalSize)
        {
            HorisontalWallLength = horisontalSize;
            VerticalWallLength = verticalSize;
        }

        public void DrawPoorHouse(City city, Prison prison)
        {
            DrawTopWall(city, prison);
            DrawLowerWall(city, prison);
            DrawWalls(city, prison);
        }

        private void DrawTopWall(City city, Prison prison)
        {
            Console.SetCursorPosition(city.HorisontalWallLength + 1, prison.VerticalWallLength + 3);
            Console.WriteLine("Poorhouse");
            Console.SetCursorPosition(city.HorisontalWallLength + 1, prison.VerticalWallLength + 4);
            for (int i = 0; i < HorisontalWallLength; i++)
            {
                Console.Write("=");
            }
        }
        private void DrawLowerWall(City city, Prison prison)
        {
            Console.SetCursorPosition(city.HorisontalWallLength + 1, VerticalWallLength + prison.VerticalWallLength + 5);
            for (int i = 0; i < HorisontalWallLength; i++)
            {
                Console.Write("="); //Golv
            }
        }

        private void DrawWalls(City city, Prison prison)
        {
            int cursorPositionLeft = city.HorisontalWallLength + 1;
            int cursorPositionTop = prison.VerticalWallLength + 5;
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
