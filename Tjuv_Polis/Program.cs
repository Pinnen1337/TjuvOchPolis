namespace Tjuv_Polis;

public class Program
{
    static void Main(string[] args)
    {
        Console.CursorVisible = false;

        int numberOfEachType = 10;
        int horizontalCitySize = 100;
        int verticalCitySize = 25;
        int horizontalPrisonSize = 25;
        int verticalPrisonSize = 10;

        int statusRow = verticalCitySize + 2;
        int messageRow = statusRow + numberOfEachType * 3;

        NewsFeed newsFeed = new NewsFeed( 0 , verticalCitySize + 3, 5);

        List<Person> persons = Helper.LoadPersons(10, newsFeed, horizontalCitySize, verticalCitySize);
        StatusList statuslist = new StatusList(persons, horizontalCitySize + horizontalPrisonSize + 4, 0);

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

        while (true)
        {
            city.Move();
            prison.Move();
            statuslist.Write();
            Helper.CheckCollision(persons);

            Thread.Sleep(100);
        }
    }
}