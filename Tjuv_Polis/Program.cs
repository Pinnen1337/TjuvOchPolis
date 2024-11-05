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

        // Skapa först lifePrisoners-listan
        List<Person> lifePrisoners = new List<Person>();

        // Skapa persons-listan först (utan helper, eftersom helper ännu inte existerar)
        List<Person> persons = PersonLoader.Load(numberOfCivilians, numberOfThiefs, numberOfPolice, newsFeed, horizontalCitySize, verticalCitySize);

        // Skapa sedan Helper-instansen, nu när både persons och lifePrisoners existerar
        Helper helper = new Helper(persons, lifePrisoners);

        // Tilldela helper till alla personer om det behövs (alternativt kan du uppdatera PersonLoader att ta en null helper som uppdateras senare)
        foreach (var person in persons)
        {
            person.Helper = helper;
        }

        StatusList statuslist = new StatusList(persons, horizontalCitySize + horizontalPrisonSize + 4, 0);

        City city = new City(horizontalCitySize, verticalCitySize, persons);
        city.DrawCity();

        Prison prison = new Prison(horizontalPrisonSize, verticalPrisonSize, lifePrisoners, city);
        prison.DrawPrison();

        PoorHouse poorhouse = new PoorHouse(horizontalPrisonSize, verticalPrisonSize);
        poorhouse.DrawPoorHouse(city, prison);

        bool isPaused = false;
        bool isProgramRunning = true;

        while (isProgramRunning)
        {
            if (Console.KeyAvailable)
            {
                ConsoleKey key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.Spacebar)
                {
                    isPaused = !isPaused;
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
                helper.CheckCollision();
            }

            Thread.Sleep(100);
        }
    }
}
