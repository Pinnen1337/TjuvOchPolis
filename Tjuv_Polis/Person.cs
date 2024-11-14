namespace Tjuv_Polis;

public class Person : IComparable
{
    public int ID { get; set; }
    public int XPosition { get; set ; }
    public int YPosition { get; set; }
    public int MovementX { get; set; }
    public int MovementY { get; set; }
    public int HorizontalSpace { get; set; }
    public int VerticalSpace { get; set; }
    public char Symbol { get; protected set; }
    public ConsoleColor Color { get; protected set; }
    public Inventory PersonalInventory { get; set; }
    public NewsFeed NewsFeed { get; set; }
    public static List<Item> ProvideAidItems { get; set; } = new List<Item>();

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
        this.NewsFeed = newsFeed;
    }
    public virtual string Status()
    {
        // Kontrollera om inventariet har några föremål
        string inventoryStatus = PersonalInventory.Items.Count > 0
            ? string.Join(" ", PersonalInventory.Items.Select(item => "[" + item.KindOfItem + "]"))
            : "";
        return $"{GetType().Name} {ID}: \t ({XPosition}, {YPosition}) \t {inventoryStatus}";
    }

    public int CompareTo(object? obj)
    {
        if (obj == null) return 1;

        if (obj is Person otherPerson)
        {
            // First compare by type
            Type thisType = this.GetType();
            Type otherType = otherPerson.GetType();

            // Assign priority values (Civilian = 3, Thief = 2, Police = 1)
            int thisTypePriority = GetTypePriority(thisType);
            int otherTypePriority = GetTypePriority(otherType);

            // If types are different, return their priority difference
            if (thisTypePriority != otherTypePriority)
            {
                return thisTypePriority.CompareTo(otherTypePriority);
            }

            // If types are same, compare by ID
            return this.ID.CompareTo(otherPerson.ID);
        }

        throw new ArgumentException("Object is not a Person");
    }

    private int GetTypePriority(Type type)
    {
        return type.Name switch
        {
            nameof(Civilian) => 1,
            nameof(Thief) => 2,
            nameof(Police) => 3,
            _ => 0
        };
    }

}

class Civilian : Person
{
    public bool IsPoor { get; set; }
    public DateTime? PovertyStart { get; set; }
    public DateTime? PovertyEnd { get; set; }

    public Civilian(int horizontalSpace, int verticalSpace, int iD, NewsFeed newsFeed) : base(horizontalSpace, verticalSpace, iD, new Inventory(), newsFeed)
    {
        Symbol = 'C';
        Color = ConsoleColor.Green;
        IsPoor = false;

        PersonalInventory.Items.Add(new Wallet());
        PersonalInventory.Items.Add(new Watch());
        PersonalInventory.Items.Add(new Phone());
        PersonalInventory.Items.Add(new Keys());
    }
    public override string Status()
    {
        string ItemsStatus = PersonalInventory.Items.Count == 0
            ? string.Join(" ", PersonalInventory.Items.Select(item => "[" + item.KindOfItem + "]"))
            : "";
        return $"{base.Status()}{(IsPoor ? "[In PoorHouse]" : "")}{ItemsStatus}";
    }
    public bool DoneTheTime()
    {
        if (DateTime.Now > this.PovertyEnd)
        {
            List<Type> requiredItems = new List<Type>();

            requiredItems.Add(typeof(Wallet));
            requiredItems.Add(typeof(Watch));
            requiredItems.Add(typeof(Phone));
            requiredItems.Add(typeof(Keys));

            // Skapa en lista för de föremål som verkligen kan tilldelas
            List<string> itemsGiven = new List<string>();

            foreach (Type itemType in requiredItems)
            {
                Item item = ProvideAidItems.FirstOrDefault(i => i.GetType() == itemType);

                if (item != null)
                {
                    PersonalInventory.Items.Add(item);
                    ProvideAidItems.Remove(item);

                    itemsGiven.Add(item.KindOfItem);
                }
            }

            if (itemsGiven.Count > 0)
            {
                string itemsList = string.Join(", ", itemsGiven);
                NewsFeed.AddMessageAndWriteQueue($"Civilian {ID} received {itemsList} upon leaving PoorHouse.", ConsoleColor.Cyan);
            }
            return true;
        }
        return false;
    }
}

class Thief : Person
{
    public List<Item> StolenItems { get; set; } = new List<Item>();
    public bool IsArrested { get; set; }
    public DateTime? SentenceStart { get; set; }
    public DateTime? SentenceEnd { get; set; }
    public int SentenceInSeconds { get; set; }

    public Thief(int horizontalSpace, int verticalSpace, int iD, NewsFeed newsFeed) : base(horizontalSpace, verticalSpace, iD, new Inventory(), newsFeed)
    {

        Symbol = 'T';
        Color = ConsoleColor.Red;
        IsArrested = false;

    }

    public void Steal(Civilian civilian)
    {
        if (!IsArrested && StolenItems.Count < 8 && civilian.PersonalInventory.Items.Count > 0)
        {
            int randomIndex = Random.Shared.Next(civilian.PersonalInventory.Items.Count);
            Item itemToSteal = civilian.PersonalInventory.Items[randomIndex];
            StolenItems.Add(itemToSteal);
            civilian.PersonalInventory.RemoveItem(itemToSteal);

            NewsFeed.AddMessageAndWriteQueue($"Thief {ID} stole {itemToSteal.KindOfItem} from Civilian {civilian.ID}.", ConsoleColor.Yellow);
        }
    }

    public override string Status()
    {
        string stolenItemsStatus = StolenItems.Count > 0
            ? string.Join(" ", StolenItems.Select(item => "[" + item.KindOfItem + "]"))
            : "";
        return $"{base.Status()}{(IsArrested ? "[In Prison]" : "")}{stolenItemsStatus}";
    }
    public bool DoneTheTime()
    {
        if (DateTime.Now > this.SentenceEnd)
        {
            return true;
        }
        return false;
    }

}

class Police : Person
{
    public bool IsFull { get; set; }
    public DateTime? DepositStart { get; set; }
    public DateTime? DepositEnd { get; set; }

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
                thief.SentenceInSeconds += 5;
            }

            // Töm listan efteråt
            thief.StolenItems.Clear();

            stolenItemsAsString = stolenItemsAsString.TrimEnd(',', ' ');

            NewsFeed.AddMessageAndWriteQueue($"Police {ID} confiscated {stolenItemsAsString} from Thief {thief.ID}.", ConsoleColor.Green);
        }
        
    }

    public void Arrest(Thief thief)
    {
        thief.IsArrested = true;

        NewsFeed.AddMessageAndWriteQueue($"Thief {thief.ID} has been sent to prison for {thief.SentenceInSeconds} seconds!", ConsoleColor.Red);
        // TODO: Now this thief must be removed from the list of persons in the City and added to the list of persons prison.
    }

    public override string Status()
    {
        string confiscatedItemsStatus = ConfiscatedItems.Count > 0
            ? string.Join(" ", ConfiscatedItems.Select(item => "[" + item.KindOfItem + "]"))
            : "";
        return $"{base.Status()}{(IsFull ? "[In Policestation]" : "")}{confiscatedItemsStatus}";
    }

    public void ProvideAid(Civilian civilian)
    {
        if (civilian.PersonalInventory.Items.Count == 0)
        {
            civilian.IsPoor = true;
            NewsFeed.AddMessageAndWriteQueue($"Civilian {civilian.ID} has been sent to Poor House for 15 seconds!", ConsoleColor.Magenta);
        }
    }
    public void Deposit()
    {
        if (ConfiscatedItems.Count > 8)
        {
            IsFull = true;
            foreach (Item item in ConfiscatedItems.ToList())
            {
                ProvideAidItems.Add(item);
            }
            ConfiscatedItems.Clear();

            NewsFeed.AddMessageAndWriteQueue($"Police {ID} has gone to the police station for 10 seconds!", ConsoleColor.Blue);
        }
    }
    public bool DoneTheTime()
    {
        if (DateTime.Now > this.DepositEnd)
        {
            return true;
        }
        return false;
    }
}
