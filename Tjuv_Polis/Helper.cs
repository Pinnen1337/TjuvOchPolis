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
                        if (thisPerson is Civilian currentcivilian && otherPerson is Thief thief)
                        {
                            thief.Steal(currentcivilian);

                        }
                        else if (thisPerson is Thief currentthief && otherPerson is Police police)
                        {
                            police.ConfiscateAndArrest(currentthief);

                        }
                        else if (thisPerson is Police currentpolice && otherPerson is Civilian civilian)
                        {
                            Console.WriteLine($"Police {currentpolice.ID} interacted with Civilian {civilian.ID}.");

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
