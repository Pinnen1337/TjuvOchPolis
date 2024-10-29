namespace Tjuv_Polis;

internal class Helper
{
    public static bool CheckCollision(List<Person> persons)
    {
        List<Person> personsAtSameLocation = new List<Person>();
        List<List<Person>> collidingPersons = new List<List<Person>>();
        foreach (Person thisPerson in persons)
        {
            foreach (Person otherPerson in persons)
            {
                if (thisPerson != otherPerson)
                {
                    if (thisPerson.XPosition == otherPerson.XPosition && thisPerson.YPosition == otherPerson.YPosition)
                    {
                        if (personsAtSameLocation.Count == 0)
                        {
                            personsAtSameLocation.Add(thisPerson);
                            personsAtSameLocation.Add(otherPerson);
                        }
                        else
                        {
                            personsAtSameLocation.Add(otherPerson);
                        }
                    }
                }
            }
            collidingPersons.Add(personsAtSameLocation);
        }
        return (collidingPersons.Count != 0 );
    }
}