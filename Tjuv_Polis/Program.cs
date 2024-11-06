namespace Tjuv_Polis;

public class Program
{
    static void Main(string[] args)
    {
        Console.CursorVisible = false;

        int numberOfEachType = 10;
        int numberOfCivilians = 15;
        int numberOfThiefs = 10;
        int numberOfPolice = 5;
        int horizontalCitySize = 100;
        int verticalCitySize = 25;
        int horizontalPrisonSize = 25;
        int verticalPrisonSize = 10;

        Console.Clear();

        NewsFeed newsFeed = new NewsFeed(0, verticalCitySize + 3, 5);

        List<Person> persons = Helper.LoadPersons(numberOfCivilians, numberOfThiefs, numberOfPolice, newsFeed, horizontalCitySize, verticalCitySize);

        List<Person> lifePrisoners = new List<Person>();
        //Thief lifer = new(horizontalPrisonSize, verticalPrisonSize, 666, newsFeed);
        //lifer.XPosition = horizontalCitySize + 3;
        //lifer.YPosition = 5;
        //lifePrisoners.Add(lifer);


        City city = new City(horizontalCitySize, verticalCitySize, persons);
        Prison prison = new Prison(horizontalPrisonSize, verticalPrisonSize, lifePrisoners, city);
        StatusList statuslist = new StatusList(persons, prison.PersonsInPrison, horizontalCitySize + horizontalPrisonSize + 4, 0);

        city.PrisonNextToCity = prison;

        city.DrawCity();
        prison.DrawPrison();

        PoorHouse poorhouse = new PoorHouse(horizontalPrisonSize, verticalPrisonSize);

        poorhouse.DrawPoorHouse(city, prison);

        bool isPaused = false;
        bool isProgramRunning = true;

        while (isProgramRunning)
        {
            // Kolla om mellanslag har tryckts för att starta/pausa simuleringen
            if (Console.KeyAvailable)
            {
                ConsoleKey key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.Spacebar)
                {
                    isPaused = !isPaused; // Växla mellan paus och start
                }
                if (key == ConsoleKey.Q)
                {
                    isProgramRunning = false;
                }
            }

            if (!isPaused)
            {
                city.Move();
                prison.Move();
                statuslist.Write();
                city.CheckCollision();
                city.MoveArrestedToPrison();
            }

            Thread.Sleep(100);
        }
    }
}