namespace Tjuv_Polis;

public class Program
{
    static void Main(string[] args)
    {
        Console.CursorVisible = false;

        //int numberOfEachType = 10;
        int numberOfCivilians = 15;
        int numberOfThiefs = 10;
        int numberOfPolice = 5;
        int horizontalCitySize = 100;
        int verticalCitySize = 25;
        int horizontalPrisonSize = 25;
        int verticalPrisonSize = 10;
        int horizontalPoorHouseSize = 25;
        int verticalPoorHouseSize = 10;
        int horizontalPoliceStationSize = 25;
        int verticalPoliceStationSize = 10;

        Console.Clear();

        NewsFeed newsFeed = new NewsFeed(0, verticalCitySize + 3, 5);

        List<Person> personsInCity = PersonFactory.Create(numberOfCivilians, numberOfThiefs, numberOfPolice, newsFeed, horizontalCitySize, verticalCitySize);

        City city = new City(horizontalCitySize, verticalCitySize, personsInCity);
        Prison prison = new Prison(horizontalPrisonSize, verticalPrisonSize, city);
        PoorHouse poorHouse = new PoorHouse(horizontalPoorHouseSize, verticalPoorHouseSize, city, prison);
        PoliceStation policeStation = new PoliceStation(horizontalPoliceStationSize, verticalPoliceStationSize, city, prison, poorHouse);
        StatusList statuslist = new StatusList(personsInCity, prison.PersonsInPrison, poorHouse.PersonsInPoorHouse, policeStation.PersonsInPoliceStation, horizontalCitySize + horizontalPrisonSize + 4, 0);

        city.PrisonNextToCity = prison;
        prison.CityNextToPrison = city;
        city.PoorHouseNextToCity = poorHouse;
        poorHouse.CityNextToPoorHouse = city;
        city.PoliceStationNextToCity = policeStation;
        policeStation.CityNextToPoliceStation = city;

        city.DrawCity();
        prison.DrawPrison();
        poorHouse.DrawPoorHouse(city, prison);
        policeStation.DrawPoliceStation(city, prison, poorHouse);
        



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
                poorHouse.Move();
                policeStation.Move();
                statuslist.Write();
                city.CheckCollision();
                city.MoveArrestedToPrison();
                city.MoveCivilianToPoorHouse();
                city.MovePoliceToPoliceStation();
                prison.ReleasePrisoners();
                poorHouse.ReleaseCivilan();
                policeStation.ReleasePolice();
            }

            Thread.Sleep(100);
        }
    }
}