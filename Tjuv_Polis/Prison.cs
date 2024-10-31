﻿using System;
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

        public void DrawPrison(City city)
        {
            DrawTopWall(city);
            DrawLowerWall(city);
            DrawWalls(city);
        }

        private void DrawTopWall(City city)
        {
            Console.SetCursorPosition(city.HorisontalWallLength + 1, 0);
            Console.WriteLine("Prison");
            Console.SetCursorPosition(city.HorisontalWallLength + 1, 1);
            for (int i = 0; i < HorisontalWallLength; i++)
            {
                Console.Write("=");
            }
        }
        private void DrawLowerWall(City city)
        {
            Console.SetCursorPosition(city.HorisontalWallLength + 1, VerticalWallLength + 2);
            for (int i = 0; i < HorisontalWallLength; i++)
            {
                Console.Write("="); //Golv
            }
        }

        private void DrawWalls(City city)
        {
            int cursorPositionLeft = city.HorisontalWallLength + 1;
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

