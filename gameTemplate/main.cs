using System;


public class Program
{
    public static void Main()
    {
        Player player = Player.Instance;
        Console.WriteLine($"Initial Status - Health: {player.Health}, Money: {player.Money}, Weapon: {player.EquippedWeapon.Name}.");

        Console.WriteLine("\n>>> Transition to dangerous sector.");
        
        ISectorFactory criminalFactory = new CriminalSectorFactory();
        
        IEntity pirate = criminalFactory.CreateEnemy();
        int pirateChoice = pirate.GetMenu();
        switch (pirateChoice)
        {
            case 1: 
                player.Health -= 30;
                Console.WriteLine($"Pirates destroyed. Player took damage. Current health: {player.Health}."); 
                break;
            case 2: 
                player.Money -= 100;
                Console.WriteLine($"Pirates bribed. Money deducted. Current money: {player.Money}."); 
                break;
            default: 
                Console.WriteLine("Action skipped."); 
                break;
        }

        IEntity blackMarket = criminalFactory.CreateMerchant();
        int marketChoice = blackMarket.GetMenu();
        switch (marketChoice)
        {
            case 1: 
                player.Money -= 200;

                int randomDamage = Random.Shared.Next(60, 101);
                int randomAmmo = Random.Shared.Next(30, 71);
                int randomAccuracy = Random.Shared.Next(80, 101);
                
                player.EquippedWeapon = new WeaponBuilder()
                    .SetName("Heavy Plasma Cannon")
                    .SetDamage(randomDamage)
                    .SetAmmo(randomAmmo)
                    .SetAccuracy(randomAccuracy)
                    .Build();
                
                Console.WriteLine($"Weapon obtained: {player.EquippedWeapon.Name}. Current money: {player.Money}."); 
                break;
            case 2: 
                Console.WriteLine("Left black market."); 
                break;
            default: 
                Console.WriteLine("Action skipped."); 
                break;
        }

        Console.WriteLine("\n------------------------------------------------");
        Console.WriteLine(">>> Transition to peaceful sector.");
        
        ISectorFactory peacefulFactory = new PeacefulSectorFactory();
        
        IEntity patrol = peacefulFactory.CreateEnemy();
        int patrolChoice = patrol.GetMenu();
        switch (patrolChoice)
        {
            case 1: 
                player.Health -= 40;
                Console.WriteLine($"Patrol engaged. Player took damage. Current health: {player.Health}."); 
                break;
            case 2: 
                player.Health -= 10;
                Console.WriteLine($"Escaped patrol with minor damage. Current health: {player.Health}."); 
                break;
            default: 
                Console.WriteLine("Action skipped."); 
                break;
        }

        IEntity merchant = peacefulFactory.CreateMerchant();
        int merchantChoice = merchant.GetMenu();
        switch (merchantChoice)
        {
            case 1: 
                player.Money -= 50;
                player.Health = 100;
                Console.WriteLine($"Repair kit obtained. Health restored to {player.Health}. Current money: {player.Money}."); 
                break;
            case 2: 
                Console.WriteLine("Left trading station."); 
                break;
            default: 
                Console.WriteLine("Action skipped."); 
                break;
        }

        Console.WriteLine($"\nFinal Status - Health: {player.Health}, Money: {player.Money}.");
        Console.WriteLine($"Weapon Specs: \nName- {player.EquippedWeapon.Name}\nDamage- {player.EquippedWeapon.Damage}\nAmmo- {player.EquippedWeapon.Ammo}\nAccuracy- {player.EquippedWeapon.Accuracy}%");    
    }
}