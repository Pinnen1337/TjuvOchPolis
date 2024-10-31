using System;
namespace Tjuv_Polis
{
	public class Inventory
	{
        public List<Item> Items { get; set; }

        public Inventory()
        {
            Items = new List<Item>();
        }
        public void AddItem(Item item)
        {
            Items.Add(item);
        }
        public bool RemoveItem(Item item)
        {
            return Items.Remove(item);
        }

        public override string ToString()
        {
            if (Items.Count == 0) return "Empty inventory";
            return string.Join(", ", Items.Select(item => item.KindOfItem));
        }
    }
}

