using System;
using System.Security.Cryptography.X509Certificates;

namespace Tjuv_Polis;

internal class Person
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

    public Person(int horizontalSpace, int verticalSpace, int iD, Inventory inventory)
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
    }

    public void Move()
    {
        Console.SetCursorPosition(XPosition, YPosition);
        Console.Write(" ");
        // Rensa tidigare position genom att skriva ut ett blanksteg
        Console.SetCursorPosition(XPosition, YPosition);
        Console.Write(' '); // Ritar ut ett blanksteg där personen tidigare var.

        int newXPosition = XPosition + MovementX;
        int newYPosition = YPosition + MovementY;

        if (newXPosition < 1)
        {
            newXPosition = HorizontalSpace - 2;
        }
        if (newYPosition < 1)
        {
            newYPosition = VerticalSpace - 1;
        }
        if (newYPosition >= VerticalSpace)
        {
            newYPosition = 2;
        }
        if (newXPosition >= HorizontalSpace - 1)
        {
            newXPosition = 2;
        }

        XPosition = newXPosition;
        YPosition = newYPosition;

        // Rita personen på nya positionen
        Console.SetCursorPosition(XPosition, YPosition);
        Console.ForegroundColor = Color;
        Console.Write(Symbol);
        Console.ResetColor();
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
    public Civilian(int horizontalSpace, int verticalSpace, int iD) : base(horizontalSpace, verticalSpace, iD, new Inventory())
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
    public Thief(int horizontalSpace, int verticalSpace, int iD) : base(horizontalSpace, verticalSpace, iD, new Inventory())
    {
        Symbol = 'T';
        Color = ConsoleColor.Red;
    }
    public void Steal(Civilian civilian)
    {
        if (civilian.PersonalInventory.Items.Count > 0)
        {
            int randomIndex = Random.Shared.Next(civilian.PersonalInventory.Items.Count);
            Item itemToSteal = civilian.PersonalInventory.Items[randomIndex];
            StolenItems.Add(itemToSteal);
            civilian.PersonalInventory.RemoveItem(itemToSteal);
            

            Console.WriteLine($"Thief {ID} stole {itemToSteal.KindOfItem} from Civilian {civilian.ID}.");
            Console.ReadKey();
        }
    }
    public override string Status()
    {
            string stolenItemsStatus = StolenItems.Count > 0
                ? string.Join(" ", StolenItems.Select(item => "[" + item.KindOfItem + "]"))
                : "";
            return $"{base.Status()}{stolenItemsStatus}";
    }
}

class Police : Person
{
    List<Item> ConfiscatedItems { get; set; } = new List<Item>();
    public Police(int horizontalSpace, int verticalSpace, int iD) : base(horizontalSpace, verticalSpace, iD, new Inventory())
    {
        Symbol = 'P';
        Color = ConsoleColor.Blue;
    }
    public void Confiscate(Thief thief)
    {
        if (thief.StolenItems.Count > 0)
        {
            string stolenItemsAsString = "";

            //for (int item = 0; item < thief.StolenItems.Count; item++)
            //{
            //    ConfiscatedItems.Add(thief.StolenItems[item]);
            //    thief.StolenItems.Remove(thief.StolenItems[item]);
            //    stolenItemsAsString += thief.StolenItems[item].KindOfItem + ", ";
            //}

            foreach (Item item in thief.StolenItems.ToList())
                {
                ConfiscatedItems.Add(item);
                stolenItemsAsString += item.KindOfItem + ", ";
            }

            // Töm listan efteråt
            thief.StolenItems.Clear();

            stolenItemsAsString = stolenItemsAsString.TrimEnd(',', ' ');


            Console.WriteLine($"Police {ID} confiscated {stolenItemsAsString} from Thief {thief.ID}.");
            Console.ReadKey();
        }
    }
    public override string Status()
    {
        string confiscatedItemsStatus = ConfiscatedItems.Count > 0
            ? string.Join(" ", ConfiscatedItems.Select(item => "[" + item.KindOfItem + "]"))
            : "";
        return $"{base.Status()}{confiscatedItemsStatus}";
    }
}


//public void Collision(Person other)
//{
//    if (XPosition == other.XPosition && YPosition == other.YPosition)
//    {

//        if (this is Civilian civilian && other is Thief thief)
//        {
//            Console.WriteLine($"Thief {thief.ID} stole from Civilian {civilian.ID}!");

//        }
//        else if (this is Thief thief && other is Police police)
//        {
//            Console.WriteLine($"Police {police.ID} caught Thief {thief.ID}!");

//        }
//        else if (this is Police police && other is Civilian civilian)
//        {
//            Console.WriteLine($"Police {police.ID} interacted with Civilian {civilian.ID}.");

//        }
//    }
//    Console.ReadKey();

//}
