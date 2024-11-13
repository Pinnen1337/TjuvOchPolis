

namespace Tjuv_Polis
{
    internal class StatusList
    {
        private List<Person> _persons;
        private readonly int _startDrawAtX;
        private readonly int _startDrawAtY;
        private List<Person> _personsInPrison;
        private List<Person> _personsInPoorHouse;

        public StatusList(List<Person> personsInCity, List<Person> personsInPrison, List<Person> personsInPoorHouse, int positionX, int positionY)
        {
            _persons = personsInCity;
            _personsInPrison = personsInPrison;
            _personsInPoorHouse = personsInPoorHouse;
            _startDrawAtX = positionX;
            _startDrawAtY = positionY;
        }

        internal void Write()
        {
            int rowOffset = 0;
            Type previousType = _persons[0].GetType();

            Console.SetCursorPosition(_startDrawAtX, _startDrawAtY);
            Console.Write(new string($"{"STATUS:",7}\tCIVILIANS: {NumberOfCiviliansInCity(),2:D}\t    POLICE:{NumberOfPoliceInCity(),2:D}\t   THIEFS: {NumberOfThiefsInCity(),2:D}\t   PRISONERS:{_personsInPrison.Count,2:D}\t    POVERTY:{_personsInPoorHouse.Count,2:D}"));
            Console.SetCursorPosition(_startDrawAtX, _startDrawAtY + 1);
            Console.Write(new string('=', 100));

            //for (int i = 0; i < 3; i++) // 2 standard för 2.
            //{
            //    Välj rätt lista baserat på värdet av `i`
            //    var currentList = (i == 1) ? _persons : _personsInPrison; // 0 standard

            for (int i = 0; i < 3; i++)
            {
                List<Person> currentList;

                // Välj rätt lista baserat på värdet av `i`
                switch (i)
                {
                    case 0:
                        currentList = _persons;
                        break;
                    case 1:
                        currentList = _personsInPrison;
                        break;
                    case 2:
                        currentList = _personsInPoorHouse;
                        break;
                    default:
                        currentList = null;
                        break;
                }

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
                Console.SetCursorPosition(_startDrawAtX, _startDrawAtY + rowOffset + 2);
                Console.Write(new string(' ', 120)); // Rensa raden
                Console.SetCursorPosition(0, _startDrawAtY + rowOffset + 2);
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
    }
}