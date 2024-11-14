using System;
namespace Tjuv_Polis
{
    public class PoliceStation
    {
        public int HorisontalWallLength { get; set; }
        public int VerticalWallLength { get; set; }
        public List<Person> PersonsInPoliceStation { get; set; }
        public int StartDrawPoliceStationXAt { get; set; }
        public int StartDrawPoliceStationYAt { get; set; }
        public City CityNextToPoliceStation { get; set; }

        public PoliceStation(int horisontalSize, int verticalSize, City city, Prison prison, PoorHouse poorHouse)
        {
            HorisontalWallLength = horisontalSize;
            VerticalWallLength = verticalSize;
            PersonsInPoliceStation = new List<Person>();
            StartDrawPoliceStationXAt = city.HorisontalWallLength;
            StartDrawPoliceStationYAt = prison.VerticalWallLength + poorHouse.VerticalWallLength;
        }

        public void Move()
        {
            foreach (Person person in PersonsInPoliceStation)
            {
                Console.SetCursorPosition(person.XPosition, person.YPosition);
                Console.Write(' '); // Ritar ut ett blanksteg där personen tidigare var.

                int newXPosition = person.XPosition + person.MovementX;
                int newYPosition = person.YPosition + person.MovementY;

                if (newXPosition < StartDrawPoliceStationXAt + 2)
                {
                    newXPosition = StartDrawPoliceStationXAt + person.HorizontalSpace - 2;
                }

                if (newYPosition < StartDrawPoliceStationYAt + 8)
                {
                    newYPosition = person.VerticalSpace + StartDrawPoliceStationYAt + 7;
                }

                if (newYPosition >= person.VerticalSpace + StartDrawPoliceStationYAt + 8)
                {
                    newYPosition = StartDrawPoliceStationYAt + 8;
                }

                if (newXPosition >= StartDrawPoliceStationXAt + person.HorizontalSpace)
                {
                    newXPosition = StartDrawPoliceStationXAt + 2;
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

        public void ReleasePolice()
        {
            List<Person> policeToRelease = new List<Person>();

            foreach (Person fullPolice in PersonsInPoliceStation)
            {
                if (fullPolice is Police police && police.DoneTheTime())
                {
                    Console.SetCursorPosition(police.XPosition, police.YPosition);
                    Console.Write(' '); // Ritar ut ett blanksteg där personen tidigare var.

                    police.XPosition = Random.Shared.Next(2, CityNextToPoliceStation.HorisontalWallLength - 1);
                    police.YPosition = Random.Shared.Next(2, CityNextToPoliceStation.VerticalWallLength); // Sätt en startposition inom city

                    police.HorizontalSpace = CityNextToPoliceStation.HorisontalWallLength;
                    police.VerticalSpace = CityNextToPoliceStation.VerticalWallLength;

                    police.IsFull = false;
                    police.NewsFeed.AddMessageAndWriteQueue($"Police {police.ID} has return from policestation.", ConsoleColor.Green);
                    policeToRelease.Add(fullPolice);
                }
            }
            CityNextToPoliceStation.PersonsInCity.AddRange(policeToRelease);
            CityNextToPoliceStation.PersonsInCity.Sort();
            foreach (Police police in policeToRelease)
            {
                PersonsInPoliceStation.Remove(police);
            }
        }
    }

}