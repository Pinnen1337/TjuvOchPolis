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
    }
}

