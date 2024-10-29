using System;
namespace Tjuv_Polis
{
	public class Inventory
	{
        public List<Item> Items { get; set; }

        public Inventory()
        {
            Items = new List<Item>
        {
            new Wallet(),
            new Watch(),
            new Phone(),
            new Keys()
        };
        }

        public override string ToString()
        {
            if (Items.Count == 0) return "Empty inventory";
            return string.Join(", ", Items.Select(item => item.KindOfItem));
        }
    }
}

