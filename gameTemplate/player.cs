public class Player
{
    private static Player? instance;
    
    public int Health { get; set; }
    public int MaxHealth { get; set; }
    public int Money { get; set; }
    public Weapon EquippedWeapon { get; set; }

    public int Shield { get; set; } = 50; 
    public int Armor { get; set; } = 20;

    private Player()
    {
        Health = 100;
        MaxHealth = 100;
        Money = 1000;
        EquippedWeapon = new WeaponBuilder()
            .SetName("Basic Blaster")
            .SetDamage(20)
            .SetAmmo(100)
            .SetAccuracy(75)
            .Build();

        Console.WriteLine("[Player initialized]");
    }

    public static Player Instance
    {   
        get
        {
            if (instance == null)
            {
                instance = new Player();
            }
            return instance;
        }
    }
}