

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
            Console.Write(new string($"{"STATUS:",7}\tCIVILIANS: {NumberOfCiviliansInCity(),2:D}\t\tPOLICE:{NumberOfPoliceInCity(),2:D}\t    THIEFS: {NumberOfThiefsInCity(),2:D}\t\tPRISONERS:{_personsInPrison.Count,2:D}"));
            Console.SetCursorPosition(_startDrawAtX, _startDrawAtY + 1);
            Console.Write(new string('=', 100));

            for (int i = 0; i < 2; i++)
            {
                // Välj rätt lista baserat på värdet av `i`
                var currentList = (i == 0) ? _persons : _personsInPrison;

                foreach (var person in currentList)
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
            }
        }
        private int NumberOfCiviliansInCity()
        {
            int count = 0;
            foreach (Person person in _persons)
            { 
                if(person is Civilian)
                    count++;
            }
            return count;
        }
        private int NumberOfThiefsInCity()
        {
            int count = 0;
            foreach (Person person in _persons)
            {
                if (person is Thief)
                    count++;
            }
            return count;
        }
        private int NumberOfPoliceInCity()
        {
            int count = 0;
            foreach (Person person in _persons)
            {
                if (person is Police)
                    count++;
            }
            return count;
        }
    }
}