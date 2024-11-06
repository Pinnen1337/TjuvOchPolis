

namespace Tjuv_Polis
{
    internal class StatusList
    {
        private List<Person> _persons;
        private readonly int _startDrawAtX;
        private readonly int _startDrawAtY;
        private List<Person> _personsInPrison;

        public StatusList(List<Person> persons, List<Person> personsInPrison, int positionX, int positionY)
        {
            _persons = persons;
            _personsInPrison = personsInPrison;
            _startDrawAtX = positionX;
            _startDrawAtY = positionY;
        }

        internal void Write()
        {
            int rowOffset = 0;
            Type previousType = _persons[0].GetType();

            Console.SetCursorPosition(_startDrawAtX, _startDrawAtY);
            Console.Write(new string($"{"STATUS",7}"));
            Console.SetCursorPosition(_startDrawAtX, _startDrawAtY + 1);
            Console.Write(new string('=', 100));

            foreach (var person in _persons)
            {
                Type currentType = person.GetType();

                if (currentType != previousType)
                {
                    Console.SetCursorPosition(_startDrawAtX, _startDrawAtY + rowOffset + 2);
                    Console.WriteLine(new string('-', 100));
                    previousType = currentType;
                    rowOffset++; // Flytta ner en rad för nästa rad
                }

                Console.SetCursorPosition(_startDrawAtX, _startDrawAtY + rowOffset + 2);
                Console.Write(new string(' ', 120)); // Rensa raden
                Console.SetCursorPosition(_startDrawAtX, _startDrawAtY + rowOffset + 2);
                Console.WriteLine(person.Status());
                rowOffset++;
            }

            foreach (var person in _personsInPrison)
            {
                Console.SetCursorPosition(_startDrawAtX, _startDrawAtY + rowOffset + 3);
                Console.Write(new string(' ', 120)); // Rensa raden
                Console.SetCursorPosition(_startDrawAtX, _startDrawAtY + rowOffset + 3);
                Console.WriteLine(person.Status());
            }

        }
    }
}