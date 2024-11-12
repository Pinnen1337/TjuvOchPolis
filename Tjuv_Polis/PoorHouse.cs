namespace Tjuv_Polis;

public class PoorHouse
{
    public int HorisontalWallLength { get; set; }
    public int VerticalWallLength { get; set; }
    public List<Person> PersonsInPoorHouse { get; set; }
    public int StartDrawPoorHouseXAt { get; set; }
    public int StartDrawPoorHouseYAt { get; set; }
    public City CityNextToPoorHouse { get; set; }

    public PoorHouse(int horisontalSize, int verticalSize, City city, Prison prison)
    {
        HorisontalWallLength = horisontalSize;
        VerticalWallLength = verticalSize;
        PersonsInPoorHouse = new List<Person>();
        StartDrawPoorHouseXAt = city.HorisontalWallLength;
        StartDrawPoorHouseYAt = prison.VerticalWallLength;
    }

    public void Move()
    {
        foreach (Person person in PersonsInPoorHouse)
        {
            Console.SetCursorPosition(person.XPosition, person.YPosition);
            Console.Write(' '); // Ritar ut ett blanksteg där personen tidigare var.

            int newXPosition = person.XPosition + person.MovementX;
            int newYPosition = person.YPosition + person.MovementY;

            if (newXPosition < StartDrawPoorHouseXAt + 2)
            {
                newXPosition = StartDrawPoorHouseXAt + person.HorizontalSpace - 2;
            }

            if (newYPosition < StartDrawPoorHouseYAt + 5) //
            {
                newYPosition = person.VerticalSpace + StartDrawPoorHouseYAt + 5;
            }

            if (newYPosition >= person.VerticalSpace + StartDrawPoorHouseYAt + 5)
            {
                newYPosition = StartDrawPoorHouseYAt + 5;
            }

            if (newXPosition >= StartDrawPoorHouseXAt + person.HorizontalSpace - 1)
            {
                newXPosition = StartDrawPoorHouseXAt + 2;
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

    public void ReleaseCivilan()
    {
        List<Person> civiliansToRelease = new List<Person>();

        foreach (Person poorCivilian in PersonsInPoorHouse)
        {
            if (poorCivilian is Civilian civilian && civilian.DoneTheTime())
            {
                Console.SetCursorPosition(civilian.XPosition, civilian.YPosition);
                Console.Write(' '); // Ritar ut ett blanksteg där personen tidigare var.

                civilian.XPosition = Random.Shared.Next(2, CityNextToPoorHouse.HorisontalWallLength - 1);
                civilian.YPosition = Random.Shared.Next(2, CityNextToPoorHouse.VerticalWallLength); // Sätt en startposition inom city

                civilian.HorizontalSpace = CityNextToPoorHouse.HorisontalWallLength;
                civilian.VerticalSpace = CityNextToPoorHouse.VerticalWallLength;

                civilian.IsPoor = false;
                civilian.NewsFeed.AddMessageAndWriteQueue($"Civilian {civilian.ID} is released from poor house.", ConsoleColor.Green);
                civiliansToRelease.Add(poorCivilian);
            }
        }
        CityNextToPoorHouse.PersonsInCity.AddRange(civiliansToRelease);
        CityNextToPoorHouse.PersonsInCity.Sort();
        foreach (Civilian civilian in civiliansToRelease)
        {
            PersonsInPoorHouse.Remove(civilian);
        }
    }
}

