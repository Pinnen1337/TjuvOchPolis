namespace Tjuv_Polis
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            List<Person> persons = new List<Person>();
            int numberOfEachType = 10;

            for (int civilians = 0; civilians < numberOfEachType; civilians++)
            {
                persons.Add(new Civilian());
            }

            for (int thiefs = 0; thiefs < numberOfEachType; thiefs++)
            {
                persons.Add(new Thief());
            }

            for (int police = 0; police < numberOfEachType; police++)
            {
                persons.Add(new Police());
            }

            while(true)
            {
                Console.Clear();
                RitaStaden();

                foreach (Person person in persons)
                {
                    Console.SetCursorPosition(person.XPosition, person.YPosition); //Ska vi byta ut denna funktion?
                    Console.ForegroundColor = person.Color;
                    Console.Write(person.Symbol);
                    Console.ResetColor();
                }

                //Thread.Sleep(500);
                //Console.ReadKey();
                Console.ReadLine();
            }
        }
        static void RitaStaden()
        {
            for (int tak = 0; tak < 100; tak++)
            {
                Console.Write("=");

            }
            Console.WriteLine();

            for (int i = 0; i < 25; i++)
            {
                Console.Write("░");  //Vänster vägg

                for (int j = 0; j < 98; j++)
                {
                    Console.Write(" "); // Mellanrum
                }
                Console.WriteLine("░"); //Höger vägg
            }


            for (int golv = 0; golv < 100; golv++)
            {
                Console.Write("=");
            }
            Console.WriteLine();
        }
    }
}
