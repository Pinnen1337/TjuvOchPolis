using System;
namespace Tjuv_Polis
{
	public class Item
	{
		public string KindOfItem { get; set; }

		public Item(string kindOfItem)
		{
			KindOfItem = kindOfItem;
		}
	}

	class Wallet : Item
	{
		public Wallet(string kindOfItem) : base(kindOfItem)
		{
			kindOfItem = "Wallet";
		}
	}

	class Watch : Item
	{
		public Watch(string kindOfItem) : base(kindOfItem)
		{

		}
	}

	class Phone : Item
	{
		public Phone() : base("Phone")
		{

		}
	}

	class Keys : Item
	{
		public Keys(string kindOfItem) : base(kindOfItem)
		{

		}
	}

}

