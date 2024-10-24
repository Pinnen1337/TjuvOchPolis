using System;
namespace Tjuv_Polis
{
    public class City
    {
        public int Roof { get; set; }
        public int Wall { get; set; }
        public int Space { get; set; }
        public int Floor { get; set; }

        public City(int roof, int wall, int space, int floor)
        {
            Roof = roof;
            Wall = wall;
            Space = space;
            Floor = floor;
        }

        public void DrawCity()
        {
            DrawRoof();
            DrawWalls();
            DrawFloor();
        }

        private void DrawRoof()
        {
            for (int i = 0; i < Roof; i++)
            {
                Console.Write("=");
            }
            Console.WriteLine();
        }

        private void DrawWalls()
        {
            for (int wall = 0; wall < 25; wall++)
            {
                Console.Write("░");  //Vänster vägg

                for (int space = 0; space < 98; space++)
                {
                    Console.Write(" "); //Mellanrum
                }
                Console.WriteLine("░"); //Höger vägg
            }
        }

        private void DrawFloor()
        {
            for (int floor = 0; floor < 100; floor++)
            {
                Console.Write("="); //Golv
            }
            Console.WriteLine();
        }

    }

}

