

namespace Tjuv_Polis
{
    internal class StatusList
    {
        private List<Person> _persons;
        private readonly int _startDrawAtX;
        private readonly int _startDrawAtY;

        public StatusList(List<Person> persons, int positionX, int positionY)
        {
            _persons = persons;
            _startDrawAtX = positionX;
            _startDrawAtY = positionY;
        }

        internal void Write()
        {
            int rowOffset = 0;
            foreach (var person in _persons)
            {
                Console.SetCursorPosition(_startDrawAtX, _startDrawAtY + rowOffset);
                Console.Write(new string(' ', 50)); // Rensa raden
                Console.SetCursorPosition(_startDrawAtX, _startDrawAtY + rowOffset);
                Console.WriteLine(person.Status());
                rowOffset++;
            }
        }
    }
}