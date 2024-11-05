
namespace Tjuv_Polis;

public class Helper
{
    public List<Person> PersonsInCity { get; set; }
    public List<Person> Prisoners { get; set; }

    public Helper(List<Person> personsInCity, List<Person> prisoners)
    {
        PersonsInCity = personsInCity;
        Prisoners = prisoners;
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
                            police.ConfiscateAndArrest(currentthief);

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
}
