public class PirateShip : IEnemy
{
    public int Damage { get; set; }
    public int LootMoney { get; set; }
    public int EscapeDamage { get; set; }
    public int EscapeCost { get; set; }

    public int GetMenu()
    {
        Console.WriteLine("\n[COMBAT] Space pirate attack.");
        Console.WriteLine("1. Fire.");
        Console.WriteLine("2. Bribe.");
        Console.Write("Input: ");

        int.TryParse(Console.ReadLine(), out int choice);
        return choice;
    }

    public string GetActionMessage(int choice)
    {
        switch (choice)
        {
            case 1:
                return "The firefight with pirates is over.";
            case 2:
                return "The pirates accepted a bribe and flew away.";
            default:
                return "The pirates are confused.";
        }
    }
}

public class EmpirePatrol : IEnemy
{
    public int Damage { get; set; }
    public int LootMoney { get; set; }
    public int EscapeDamage { get; set; }
    public int EscapeCost { get; set; }

    public int GetMenu()
    {
        Console.WriteLine("\n[COMBAT] Imperial patrol encounter.");
        Console.WriteLine("1. Fire.");
        Console.WriteLine("2. Attempt escape.");
        Console.Write("Input: ");
        
        int.TryParse(Console.ReadLine(), out int choice);
        return choice;
    }

    public string GetActionMessage(int choice)
    {
        switch (choice)
        {
            case 1:
                return "The patrol was destroyed, but it was tough.";
            case 2:
                return "Escape from the patrol under heavy fire.";
            default:
                return "The patrol flew past.";
        }
    }
}

public class BlackMarketTrader : IMerchant
{
    public Weapon? WeaponItem { get; set; }
    public int WeaponPrice { get; set; }
    public int RepairKitPrice { get; set; }
    public int BuffPrice { get; set; }

    public int GetMenu()
    {
        int res=0;

        Console.WriteLine("\n[SHOP] Black market accessed.");
        Console.WriteLine("1. Buy weapons.");
        Console.WriteLine("2. Buy health buff.");
        Console.WriteLine("3. Leave.");
        Console.Write("Input: ");
        
        int.TryParse(Console.ReadLine(), out int choice);

        switch(choice) {
            case 1:
                res=1;
                break;
            case 2:
                res=3;
                break;
            case 3:
                res=4;
                break;
        }

        return res;
    }

    public string GetActionMessage(int choice)
    {
        switch (choice)
        {
            case 1:
                return "Illegal weapon purchased.";
            case 3:
                return "Health buff used.";
            case 4:
                return "Leaving the black market trader.";
            default:
                return "UNKOWN OPTION FOR BLACK MARKET TRADER.";
        }
    }
}

public class Merchant : IMerchant
{
    public Weapon? WeaponItem { get; set; }
    public int WeaponPrice { get; set; }
    public int RepairKitPrice { get; set; }
    public int BuffPrice { get; set; }

    public int GetMenu()
    {
        int res=0;

        Console.WriteLine("\n[SHOP] Trading station docked.");
        Console.WriteLine("1. Buy standard repair kit.");
        Console.WriteLine("2. Leave.");
        Console.Write("Input: ");

        int.TryParse(Console.ReadLine(), out int choice);
        
        switch(choice) {
            case 1:
                res=2;
                break;
            case 2:
                res=4;
                break;
        }

        return res;
    }

    public string GetActionMessage(int choice)
    {
        switch (choice)
        {
            case 2:
                return "Standard repair kit purchased. Ship repaired.";
            case 4:
                return "You left the station.";
            default:
                return "WRONG OPTION FOR THE MERCHANT.";
        }
    }
}