namespace Tjuv_Polis;

public class Person
{
    public int ID { get; set; }
    public int XPosition { get; set; }
    public int YPosition { get; set; }
    public int MovementX { get; set; }
    public int MovementY { get; set; }
    public int HorizontalSpace { get; set; }
    public int VerticalSpace { get; set; }
    public char Symbol { get; set; }
    public ConsoleColor Color { get; set; }
    public Inventory PersonalInventory { get; set; }
    protected NewsFeed _newsFeed;

    public Person(int horizontalSpace, int verticalSpace, int iD, Inventory inventory, NewsFeed newsFeed)
    {
        HorizontalSpace = horizontalSpace;
        VerticalSpace = verticalSpace;

        Random random = new Random();
        XPosition = random.Next(2, horizontalSpace - 1);
        YPosition = random.Next(2, verticalSpace - 1);
        MovementX = random.Next(-1, 2);
        MovementY = random.Next(-1, 2);

        ID = iD;
        PersonalInventory = inventory;
        _newsFeed = newsFeed;

    }

    public virtual string Status()
    {
        // Kontrollera om inventariet har några föremål
        string inventoryStatus = PersonalInventory.Items.Count > 0
            ? string.Join(" ", PersonalInventory.Items.Select(item => "[" + item.KindOfItem + "]"))
            : "";
        return $"{GetType().Name} {ID}: \t ({XPosition}, {YPosition}) \t {inventoryStatus}";
    }
}

class Civilian : Person
{
    public Civilian(int horizontalSpace, int verticalSpace, int iD, NewsFeed newsFeed) : base(horizontalSpace, verticalSpace, iD, new Inventory(), newsFeed)
    {
        Symbol = 'C';
        Color = ConsoleColor.Green;

        PersonalInventory.Items.Add(new Wallet());
        PersonalInventory.Items.Add(new Watch());
        PersonalInventory.Items.Add(new Phone());
        PersonalInventory.Items.Add(new Keys());
    }
}

class Thief : Person
{
    public List<Item> StolenItems { get; set; } = new List<Item>();
    public bool IsArrested { get; set; }

    public Thief(int horizontalSpace, int verticalSpace, int iD, NewsFeed newsFeed) : base(horizontalSpace, verticalSpace, iD, new Inventory(), newsFeed)
    {

        Symbol = 'T';
        Color = ConsoleColor.Red;
        IsArrested = false;

    }

    public void Steal(Civilian civilian)
    {
        if (!IsArrested && civilian.PersonalInventory.Items.Count > 0)
        {

            int randomIndex = Random.Shared.Next(civilian.PersonalInventory.Items.Count);
            Item itemToSteal = civilian.PersonalInventory.Items[randomIndex];
            StolenItems.Add(itemToSteal);
            civilian.PersonalInventory.RemoveItem(itemToSteal);

            _newsFeed.AddMessageAndWriteQueue($"Thief {ID} stole {itemToSteal.KindOfItem} from Civilian {civilian.ID}.");
        }
    }

    public override string Status()
    {
        string stolenItemsStatus = StolenItems.Count > 0
            ? string.Join(" ", StolenItems.Select(item => "[" + item.KindOfItem + "]"))
            : "";
        return $"{base.Status()}{(IsArrested ? "[In Prison]" : "")}{stolenItemsStatus}";
    }


}

class Police : Person
{
    List<Item> ConfiscatedItems { get; set; } = new List<Item>();
    public Police(int horizontalSpace, int verticalSpace, int iD, NewsFeed newsFeed) : base(horizontalSpace, verticalSpace, iD, new Inventory(), newsFeed)
    {
        Symbol = 'P';
        Color = ConsoleColor.Blue;
    }
    public void ConfiscateAllItems(Thief thief)
    {
        if (!thief.IsArrested && thief.StolenItems.Count > 0)
        {
            string stolenItemsAsString = "";

            foreach (Item item in thief.StolenItems.ToList())
            {
                ConfiscatedItems.Add(item);
                stolenItemsAsString += item.KindOfItem + ", ";
            }

            // Töm listan efteråt
            thief.StolenItems.Clear();

            stolenItemsAsString = stolenItemsAsString.TrimEnd(',', ' ');

            _newsFeed.AddMessageAndWriteQueue($"Police {ID} confiscated {stolenItemsAsString} from Thief {thief.ID}.");
        }
    }

    public void Arrest(Thief thief)
    {
        thief.IsArrested = true;

        _newsFeed.AddMessageAndWriteQueue($"Thief {ID} has been sent to prison!");
        // TODO: Now this thief must be removed from the list of persons in the City and added to the list of persons prison.
    }

    public override string Status()
    {
        string confiscatedItemsStatus = ConfiscatedItems.Count > 0
            ? string.Join(" ", ConfiscatedItems.Select(item => "[" + item.KindOfItem + "]"))
            : "";
        return $"{base.Status()}{confiscatedItemsStatus}";
    }

    internal void Greet(Civilian civilian)
    {
        _newsFeed.AddMessageAndWriteQueue($"Police {ID} interacted with Civilian {civilian.ID}.");
    }
}

