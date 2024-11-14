namespace Tjuv_Polis;

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
	public Wallet() : base("Wallet")
	{

	}
}

class Watch : Item
{
	public Watch() : base("Watch")
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
	public Keys() : base("Keys")
	{

	}
}