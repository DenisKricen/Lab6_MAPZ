public class PirateShip : IEnemy
{
    public int Damage { get; set; }
    public int LootMoney { get; set; }
    public int EscapeDamage { get; set; }
    public int EscapeCost { get; set; }

    public int GetMenu()
    {
        Console.WriteLine("[COMBAT] Space pirate attack.");
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
        Console.WriteLine("[COMBAT] Imperial patrol encounter.");
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
    private Weapon baseWeapon;
    private int baseWeaponPrice;

    // By default fair trade
    private ITradingStrategy currentStrategy = new FairTradeStrategy();

    // Delegate logic to stategy
    public Weapon WeaponItem 
    { 
        get => currentStrategy.GetWeapon(baseWeapon); 
        set => baseWeapon = value; 
    }
    
    public int WeaponPrice 
    { 
        get => currentStrategy.CalculatePrice(baseWeaponPrice); 
        set => baseWeaponPrice = value; 
    }
    
    public int RepairKitPrice { get; set; }
    public int BuffPrice      { get; set; }

    public IMerchant SetStrategy(ITradingStrategy newStrategy)
    {
        this.currentStrategy = newStrategy;
        return this; 
    }

    public int GetMenu()
    {
        int res = 0;

        Console.WriteLine("[SHOP] Black market accessed.");
        Console.WriteLine($"1. Buy weapon: {WeaponItem.Name} for {WeaponPrice}");
        Console.WriteLine($"2. Buy health buff (50 hp) for {BuffPrice}");
        Console.WriteLine("3. Leave.");
        Console.Write("Input: ");
        
        int.TryParse(Console.ReadLine(), out int choice);

        switch(choice) {
            case 1: res = 1; break;
            case 2: res = 3; break;
            case 3: res = 4; break;
        }

        return res;
    }

    public string GetActionMessage(int choice)
    {
        switch (choice)
        {
            case 1: return "Illegal weapon purchased.";
            case 3: return "Health buff used.";
            case 4: return "Leaving the black market trader.";
            default: return "UNKOWN OPTION FOR BLACK MARKET TRADER.";
        }
    }
}

// Proxy usage
public class BlackMarketGuardProxy : IMerchant
{
    private IMerchant trader;
    private Player player;

    public Weapon WeaponItem => trader.WeaponItem;
    public int WeaponPrice => trader.WeaponPrice;
    public int RepairKitPrice => trader.RepairKitPrice;
    public int BuffPrice => trader.BuffPrice;

    public BlackMarketGuardProxy(IMerchant realTrader, Player realPlayer)
    {
        trader = realTrader;
        player = realPlayer;
    }

    public IMerchant SetStrategy(ITradingStrategy newStrategy)
    {
        // No implementation
        return this;
    }

    public int GetMenu()
    {

        if (player.Money < 1100) 
        {
            Console.WriteLine("[GUARD] You have no enough money to buy stuff here");
            return 4; 
        }
        
        // If everything okay - delegating work to the real trader
        return trader.GetMenu();
    }

    public string GetActionMessage(int choice)
    {
        if (choice == 4)
        {
            return "You are leaving";
        }

        return trader.GetActionMessage(choice);
    }
}

public class Merchant : IMerchant
{
    public Weapon? WeaponItem { get; set; }
    public int WeaponPrice { get; set; }
    public int RepairKitPrice { get; set; }
    public int BuffPrice { get; set; }

    public IMerchant SetStrategy(ITradingStrategy newStrategy)
    {
        // No realisation
        return this;
    }

    public int GetMenu()
    {
        int res=0;

        Console.WriteLine("[SHOP] Trading station docked.");
        Console.WriteLine($"1. Buy standard repair kit (150hp) for {RepairKitPrice}");
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