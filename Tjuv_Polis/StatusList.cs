﻿namespace Tjuv_Polis
{
    internal class StatusList
    {
        private List<Person> _persons;
        private readonly int _startDrawAtX;
        private readonly int _startDrawAtY;
        private List<Person> _personsInPrison;
        private List<Person> _personsInPoorHouse;
        private List<Person> _personsInPoliceStation;

        public StatusList(List<Person> personsInCity, List<Person> personsInPrison, List<Person> personsInPoorHouse, List<Person> personsInPoliceStation, int positionX, int positionY)
        {
            _persons = personsInCity;
            _personsInPrison = personsInPrison;
            _personsInPoorHouse = personsInPoorHouse;
            _personsInPoliceStation = personsInPoliceStation;
            _startDrawAtX = positionX;
            _startDrawAtY = positionY;
        }

        internal void Write()
        {
            int rowOffset = 0;
            Type previousType = _persons[0].GetType();

            Console.SetCursorPosition(_startDrawAtX, _startDrawAtY);
            Console.Write(new string($"{"STATUS:",7} CIVILIANS: {NumberOfCiviliansInCity(),2:D}\t  POLICE:{NumberOfPoliceInCity(),2:D}\t THIEFS: {NumberOfThiefsInCity(),2:D}\t PRISONERS:{_personsInPrison.Count,2:D}\t   POVERTY:{_personsInPoorHouse.Count,2:D}\t  STATION{_personsInPoliceStation.Count, 2:D}"));
            Console.SetCursorPosition(_startDrawAtX, _startDrawAtY + 1);
            Console.Write(new string('=', 100));

            //for (int i = 0; i < 3; i++) // 2 standard för 2.
            //{
            //    Välj rätt lista baserat på värdet av `i`
            //    var currentList = (i == 1) ? _persons : _personsInPrison; // 0 standard

            for (int i = 0; i < 4; i++) // 3 nu 4
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
                    case 3:
                        currentList = _personsInPoliceStation;
                        break;
                    default:
                        currentList = null;
                        break;
                }

                foreach (Person person in currentList)
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

            // Gruppera objekten efter typ och räknar antalet av varje typ
            var groupedItems = Person.ProvideAidItems
                .GroupBy(item => item.KindOfItem)
                .Select(group => new { Item = group.Key, Count = group.Count() })
                .ToList();

            // Skapa en sträng där varje item skrivs ut med antalet före eller efter
            string itemsWithCount = string.Join("", groupedItems.Select(group => $"[{group.Count}[{group.Item}] "));

            Console.SetCursorPosition(_startDrawAtX, _startDrawAtY + rowOffset + 3);
            Console.WriteLine(new string(' ', 100));
            Console.SetCursorPosition(_startDrawAtX, _startDrawAtY + rowOffset + 3);

            rowOffset++; // Flytta ner en rad för nästa rad

            // Skriv ut resultatet på samma rad
            Console.SetCursorPosition(_startDrawAtX, _startDrawAtY + rowOffset + 3);
            Console.WriteLine("Items in Police Station:\t " + itemsWithCount);

            Console.SetCursorPosition(_startDrawAtX, _startDrawAtY + rowOffset + 4); // nytt todo test
            Console.Write(new string(' ', 120)); // Rensa raden
            Console.SetCursorPosition(0, _startDrawAtY + rowOffset + 4);

            Console.SetCursorPosition(_startDrawAtX, _startDrawAtY + rowOffset + 5); // nytt todo test
            Console.Write(new string(' ', 120)); // Rensa raden
            Console.SetCursorPosition(0, _startDrawAtY + rowOffset + 5);

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