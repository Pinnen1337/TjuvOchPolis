using System.Runtime.CompilerServices;
using Tjuv_Polis;

namespace Tjuv_Polis
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Random random = new Random();
            List<Person> persons = new List<Person>();
            int numberOfEachType = 10;
            int horizontalCitySize = 100;
            int verticalCitySize = 25;
            int horizontalPrisonSize = 10;
            int verticalPrisonSize = 10;

            int statusRow = verticalCitySize + 2;
            int messageRow = statusRow + numberOfEachType * 3;

            for (int civilians = 0; civilians < numberOfEachType; civilians++)
            {
                persons.Add(new Civilian(horizontalCitySize, verticalCitySize, civilians + 1));
            }

            for (int thiefs = 0; thiefs < numberOfEachType; thiefs++)
            {
                persons.Add(new Thief(horizontalCitySize, verticalCitySize, thiefs + 1));
            }

            for (int police = 0; police < numberOfEachType; police++)
            {
                persons.Add(new Police(horizontalCitySize, verticalCitySize, police + 1));
            }

            City city = new City(horizontalCitySize, verticalCitySize);
            city.DrawCity();

            Prison prison = new Prison(horizontalPrisonSize, verticalPrisonSize);
            prison.DrawPrison(city);

            Console.SetCursorPosition(0, messageRow);
            Console.Write(new string('-', horizontalCitySize));

            while (true)
            {
                foreach (Person person in persons)
                {
                    person.Move();
                }

                for (int i = 0; i < persons.Count; i++)
                {
                    Console.SetCursorPosition(0, statusRow + i);
                    Console.Write(new string(' ', horizontalCitySize)); // Rensa raden
                    Console.SetCursorPosition(0, statusRow + i);
                    Console.WriteLine(persons[i].Status());
                }
                for (int i = 0; i < 4; i++) // rensar nu 4 rader då det har hänt en gång
                {
                    Console.SetCursorPosition(0, messageRow + i + 1);
                    Console.Write(new string(' ', horizontalCitySize));
                }
                //Console.SetCursorPosition(0, messageRow + 1);
                //Console.Write(new string(' ', horizontalCitySize));
                //Console.SetCursorPosition(0, messageRow + 2);
                //Console.Write(new string(' ', horizontalCitySize));
                //Console.SetCursorPosition(0, messageRow + 3);
                //Console.Write(new string(' ', horizontalCitySize));
                Console.SetCursorPosition(0, messageRow + 1);
                Helper.CheckCollision(persons);

                Thread.Sleep(0);
            }
        }
    }
}