using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tjuv_Polis
{
    internal class Prison
    {
        public int HorisontalWallLength { get; set; }
        public int VerticalWallLength { get; set; }

        public Prison(int horisontalSize, int verticalSize)
        {
            HorisontalWallLength = horisontalSize;
            VerticalWallLength = verticalSize;
        }

        public void DrawPrison()
        {
            DrawTopWall();
            DrawLowerWall();
            DrawWalls();
        }

        private void DrawTopWall()
        {
            Console.SetCursorPosition(100 + 1, 0);
            Console.WriteLine("Prison");
            Console.SetCursorPosition(100 + 1, 1);
            for (int i = 0; i < HorisontalWallLength; i++)
            {
                Console.Write("=");
            }
        }
        private void DrawLowerWall()
        {
            Console.SetCursorPosition(100 + 1, VerticalWallLength + 2);
            for (int i = 0; i < HorisontalWallLength; i++)
            {
                Console.Write("="); //Golv
            }
        }

        private void DrawWalls()
        {
            int cursorPositionLeft = 101;
            int cursorPositionTop = 2;
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

