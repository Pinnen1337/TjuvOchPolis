namespace Tjuv_Polis
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Random random = new Random();
            List<Person> persons = new List<Person>();
            int numberOfEachType = 5;
            int horizontalCitySize = 100;
            int verticalCitySize = 25;

            for (int civilians = 0; civilians < numberOfEachType; civilians++)
            {
                persons.Add(new Civilian(horizontalCitySize, verticalCitySize));
            }

            for (int thiefs = 0; thiefs < numberOfEachType; thiefs++)
            {
                persons.Add(new Thief(horizontalCitySize, verticalCitySize));
            }

            for (int police = 0; police < numberOfEachType; police++)
            {
                persons.Add(new Police(horizontalCitySize, verticalCitySize));
            }

            while(true)
            {
                Console.Clear();

                City city = new City(horizontalCitySize, verticalCitySize);
                city.DrawCity();

                foreach (Person person in persons)
                {
                    Console.SetCursorPosition(person.XPosition, person.YPosition);
                    Console.ForegroundColor = person.Color;
                    Console.Write(person.Symbol);
                    Console.ResetColor();
                    person.Move();
                    Console.SetCursorPosition(0, verticalCitySize + 1); // För Mac

                }

                Thread.Sleep(300);
                //Console.ReadKey();
                //Console.ReadLine();
            }
        }
    }
}
