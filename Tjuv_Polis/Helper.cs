
namespace Tjuv_Polis;

public class Helper
{
    public static List<Person> LoadPersons(int numberOfEachType, NewsFeed newsFeed, int horizontalCitySize, int verticalCitySize)
    {
        List<Person> persons = new List<Person>();
        for (int civilians = 0; civilians < numberOfEachType; civilians++)
        {
            persons.Add(new Civilian(horizontalCitySize, verticalCitySize, civilians + 1, newsFeed));
        }

        for (int thiefs = 0; thiefs < numberOfEachType; thiefs++)
        {
            persons.Add(new Thief(horizontalCitySize, verticalCitySize, thiefs + 1, newsFeed));
        }

        for (int police = 0; police < numberOfEachType; police++)
        {
            persons.Add(new Police(horizontalCitySize, verticalCitySize, police + 1, newsFeed));
        }
        return persons;
    }

    public static List<Person> LoadPersons(int numberOfCivilians, int numberOfThiefs, int numberOfPolice, NewsFeed newsFeed, int horizontalCitySize, int verticalCitySize)
    {
        List<Person> persons = new List<Person>();
        for (int civilians = 0; civilians < numberOfCivilians; civilians++)
        {
            persons.Add(new Civilian(horizontalCitySize, verticalCitySize, civilians + 1, newsFeed));
        }

        for (int thiefs = 0; thiefs < numberOfThiefs; thiefs++)
        {
            persons.Add(new Thief(horizontalCitySize, verticalCitySize, thiefs + 1, newsFeed));
        }

        for (int police = 0; police < numberOfPolice; police++)
        {
            persons.Add(new Police(horizontalCitySize, verticalCitySize, police + 1, newsFeed));
        }
        return persons;
    }
}
