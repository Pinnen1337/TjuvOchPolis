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
                        if (thisPerson is Civilian civilian && otherPerson is Thief thief)
                        {
                            thief.Steal(civilian);

                        }
                        else if (thisPerson is Thief thief2 && otherPerson is Police police)
                        {
                            police.Confiscate(thief2);

                        }
                        else if (thisPerson is Police police2 && otherPerson is Civilian civilian2)
                        {
                            Console.WriteLine($"Police {police2.ID} interacted with Civilian {civilian2.ID}.");

                        }
                        //                if (personsAtSameLocation.Count == 0)
                        //                {
                        //                    personsAtSameLocation.Add(thisPerson);
                        //                    personsAtSameLocation.Add(otherPerson);
                        //                }
                        //                else
                        //                {
                        //                    personsAtSameLocation.Add(otherPerson);
                        //                }
                        //            }
                        //        }
                        //    }
                        //    collidingPersons.Add(personsAtSameLocation);
                        //}
                        //return (collidingPersons.Count != 0 );
                    }
                }
            }
        }
        return false;
    }
}
