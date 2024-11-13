
namespace Tjuv_Polis;

public class City
{
    public int HorisontalWallLength { get; set; }
    public int VerticalWallLength { get; set; }
    public List<Person> PersonsInCity { get; set; }
    public Prison PrisonNextToCity { get; set; }
    public PoorHouse PoorHouseNextToCity { get; set; }


    public City(int horisontalSize, int verticalSize, List<Person> personsInCity)
    {
        HorisontalWallLength = horisontalSize;
        VerticalWallLength = verticalSize;
        PersonsInCity = personsInCity;
    }

    public void Move()
    {
        foreach (Person person in PersonsInCity)
        {
            Console.SetCursorPosition(person.XPosition, person.YPosition);
            Console.Write(' '); // Ritar ut ett blanksteg där personen tidigare var.

            int newXPosition = person.XPosition + person.MovementX;
            int newYPosition = person.YPosition + person.MovementY;

            if (newXPosition < 1)
            {
                newXPosition = person.HorizontalSpace - 2;
            }
            if (newYPosition < 1)
            {
                newYPosition = person.VerticalSpace - 1;
            }
            if (newYPosition >= person.VerticalSpace + 1)
            {
                newYPosition = 2;
            }
            if (newXPosition >= person.HorizontalSpace - 1)
            {
                newXPosition = 2;
            }

            person.XPosition = newXPosition;
            person.YPosition = newYPosition;

            // Rita personen på nya positionen
            Console.SetCursorPosition(person.XPosition, person.YPosition);
            Console.ForegroundColor = person.Color;
            Console.Write(person.Symbol);
            Console.ResetColor();

        }
    }

    public void DrawCity()
    {
        DrawTopWall();
        DrawLowerWall();
        DrawWalls();
    }

    private void DrawTopWall()
    {
        Console.SetCursorPosition(0, 0);
        for (int i = 0; i < HorisontalWallLength; i++)
        {
            Console.Write("=");
        }
    }
    private void DrawLowerWall()
    {
        Console.SetCursorPosition(0, VerticalWallLength + 1);
        for (int i = 0; i < HorisontalWallLength; i++)
        {
            Console.Write("="); //Golv
        }
    }

    private void DrawWalls()
    {
        int cursorPositionLeft = 0;
        int cursorPositionTop = 1;
        for (int wall = 0; wall < VerticalWallLength; wall++)
        {
            Console.SetCursorPosition(cursorPositionLeft, cursorPositionTop);
            Console.Write("░");  //Vänster vägg
            Console.SetCursorPosition(cursorPositionLeft + HorisontalWallLength - 1, cursorPositionTop);
            Console.WriteLine("░"); //Höger vägg
            cursorPositionTop++;
        }
    }
    public void CheckCollision()
    {
        foreach (Person thisPerson in PersonsInCity)
        {
            foreach (Person otherPerson in PersonsInCity)
            {
                if (thisPerson != otherPerson)
                {
                    if (thisPerson.XPosition == otherPerson.XPosition && thisPerson.YPosition == otherPerson.YPosition)
                    {
                        if (thisPerson is Civilian currentcivilian && otherPerson is Thief thief)
                        {
                            thief.Steal(currentcivilian);

                        }
                        else if (thisPerson is Thief currentthief && otherPerson is Police police)
                        {
                            if (currentthief.StolenItems.Count > 0)
                            {
                            police.ConfiscateAllItems(currentthief);
                            police.Arrest(currentthief);
                            }
                            //else // Todo. Kolla om det går att få ut ett meddelande om Polis och Tjuv möts när tjuven inte har tagit något.
                            //{
                            //    _newsFeed.AddMessageAndWriteQueue($"Police {police.ID} interacted with Thief {currentThief.ID} but found no stolen items.");
                            //}

                        }
                        else if (thisPerson is Police currentpolice && otherPerson is Civilian civilian)
                        {
                            currentpolice.Greet(civilian);
                        }
                    }
                }
            }
        }
    }

    internal void MoveArrestedToPrison()
    {
        List<Person> prisonTransport = new List<Person>();
        foreach (Person thisPerson in PersonsInCity)
        {
            if (thisPerson is Thief thief && thief.IsArrested == true)
            {
                // Justera tjuvens position och utrymme för fängelset
                thief.XPosition = Random.Shared.Next(PrisonNextToCity.StartDrawPrisonAt + 2, PrisonNextToCity.StartDrawPrisonAt + PrisonNextToCity.HorisontalWallLength);
                thief.YPosition = Random.Shared.Next(2, 2 + PrisonNextToCity.VerticalWallLength); // Sätt en startposition inom fängelset
                thief.HorizontalSpace = PrisonNextToCity.HorisontalWallLength;
                thief.VerticalSpace = PrisonNextToCity.VerticalWallLength;
                thief.SentenceStart = DateTime.Now;
                thief.SentenceEnd = DateTime.Now.AddSeconds(thief.SentenceInSeconds);
                prisonTransport.Add(thisPerson);
            }
        }
        PrisonNextToCity.PersonsInPrison.AddRange(prisonTransport);
        foreach (Thief thief in prisonTransport)
        {
            PersonsInCity.Remove(thief);
        }
    }
    internal void MoveCivilianToPoorHouse()
    {
        List<Person> poorTransport = new List<Person>();
        foreach (Person thisPerson in PersonsInCity)
        {
            if (thisPerson is Civilian civilian && civilian.IsPoor == true)
            {
                // Justera fattigmans position och utrymme för fattigstugan
                civilian.XPosition = Random.Shared.Next(PoorHouseNextToCity.StartDrawPoorHouseXAt + 2, PoorHouseNextToCity.StartDrawPoorHouseXAt + PoorHouseNextToCity.HorisontalWallLength);
                civilian.YPosition = Random.Shared.Next(5 + PrisonNextToCity.VerticalWallLength, 4 + PrisonNextToCity.VerticalWallLength + PoorHouseNextToCity.VerticalWallLength); // Sätt en startposition inom poor house
                civilian.HorizontalSpace = PoorHouseNextToCity.HorisontalWallLength;
                civilian.VerticalSpace = PoorHouseNextToCity.VerticalWallLength;
                civilian.PovertyStart = DateTime.Now;
                civilian.PovertyEnd = DateTime.Now.AddSeconds(15);
                poorTransport.Add(thisPerson);
            }
        }
        PoorHouseNextToCity.PersonsInPoorHouse.AddRange(poorTransport);
        foreach (Civilian civilian in poorTransport)
        {
            PersonsInCity.Remove(civilian);
        }
    }
}

