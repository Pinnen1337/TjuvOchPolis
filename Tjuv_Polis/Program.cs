namespace Tjuv_Polis;

public class Program
{
    static void Main(string[] args)
    {
        Console.CursorVisible = false;
        Random random = new Random();
        List<Person> persons = new List<Person>();

        int numberOfEachType = 5;
        int horizontalCitySize = 100;
        int verticalCitySize = 25;
        int horizontalPrisonSize = 25;
        int verticalPrisonSize = 10;

        int statusRow = verticalCitySize + 2;
        int messageRow = statusRow + numberOfEachType * 3;

        NewsFeed newsFeed = new NewsFeed( 0 , verticalCitySize + numberOfEachType * 3 + 5, 5);

        for (int civilians = 0; civilians < numberOfEachType; civilians++)
        {
            persons.Add(new Civilian(horizontalCitySize, verticalCitySize, civilians + 1, newsFeed));
        }

        for (int thiefs = 0; thiefs < numberOfEachType; thiefs++)
        {
            persons.Add(new Thief(horizontalCitySize, verticalCitySize, thiefs + 1, newsFeed));
        }

        for (int police = 0; police < numberOfEachType; police++)
        {
            persons.Add(new Police(horizontalCitySize, verticalCitySize, police + 1, newsFeed));
        }


        List<Person> lifePrisoners = new List<Person>();
        Thief lifer = new(horizontalPrisonSize, verticalPrisonSize, 666, newsFeed);
        lifer.XPosition = horizontalCitySize + 3;
        lifer.YPosition = 5;


        lifePrisoners.Add(lifer);


        City city = new City(horizontalCitySize, verticalCitySize, persons);
        city.DrawCity();

        Prison prison = new Prison(horizontalPrisonSize, verticalPrisonSize, lifePrisoners, city);
        prison.DrawPrison();

        PoorHouse poorhouse = new PoorHouse(horizontalPrisonSize, verticalPrisonSize);
        poorhouse.DrawPoorHouse(city, prison);

        Console.SetCursorPosition(0, messageRow);
        Console.Write(new string('-', horizontalCitySize));

        while (true)
        {
            city.Move();
            prison.Move();

            //for (int i = 0; i < persons.Count; i++)
            //{
            //    Console.SetCursorPosition(0, statusRow + i);
            //    Console.Write(new string(' ', horizontalCitySize)); // Rensa raden
            //    Console.SetCursorPosition(0, statusRow + i);
            //    Console.WriteLine(persons[i].Status());
            //}
            //for (int i = 0; i < 4; i++) // rensar nu 4 rader då det har hänt en gång
            //{
            //    Console.SetCursorPosition(0, messageRow + i + 1);
            //    Console.Write(new string(' ', horizontalCitySize));
            //}

            //Console.SetCursorPosition(0, messageRow + 1);
            Helper.CheckCollision(persons);

            Thread.Sleep(100);
        }
    }
}