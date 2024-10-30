namespace Tjuv_Polis;

internal class Helper
{
    public static bool CheckCollision(List<Person> persons)
    {
        List<List<Person>> collidingPersonsGrouped = new List<List<Person>>(); // List of lists of all Persons involved in collisions grouped by collision location
        List<Person> collidingPersons = new List<Person>(); // List to keep track of persons already involved in a collision

        foreach (Person thisPerson in persons)
        {
            if (collidingPersons.Contains(thisPerson)) // skip the collision check if this Person is involved in an "other" collision
            { 
                continue;
            }

            List<Person> personsAtSameLocation = new List<Person>();
            personsAtSameLocation.Add(thisPerson);

            foreach (Person otherPerson in persons)
            {
                if (thisPerson != otherPerson)
                {
                    if (thisPerson.XPosition == otherPerson.XPosition
                        && thisPerson.YPosition == otherPerson.YPosition
                        && !personsAtSameLocation.Contains(otherPerson))
                    {
                        personsAtSameLocation.Add(otherPerson);
                        collidingPersons.Add(thisPerson); // Add the Person as involved in a collision
                    }
                }
            }

            if (personsAtSameLocation.Count > 1)
            {
                collidingPersonsGrouped.Add(personsAtSameLocation);
            }
        }
        return (collidingPersonsGrouped.Count != 0);
    }
}

