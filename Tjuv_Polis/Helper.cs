namespace Tjuv_Polis;

internal class Helper
{
    public static bool CheckCollision(List<Person> persons)
    {
        //List<Person> personsAtSameLocation = new List<Person>();
        //List<List<Person>> collidingPersons = new List<perssons>();
        foreach (Person thisPerson in persons)
        {
            foreach (Person otherPerson in persons)
            {
                if (thisPerson != otherPerson)
                {
                    if (thisPerson.XPosition == otherPerson.XPosition && thisPerson.YPosition == otherPerson.YPosition)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }
}