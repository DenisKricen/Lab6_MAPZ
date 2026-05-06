using System;

public class Program
{
    public static void Main()
    {
        Player player = Player.Instance;
        SpaceSimFacade gameFacade = new SpaceSimFacade();

        Console.WriteLine($"Initial Status - Health: {player.Health}, Money: {player.Money}, Weapon: {player.EquippedWeapon.Name}.");

        Console.WriteLine("\n------------------------------------------------");
        Console.WriteLine("\n>>> Transition to dangerous sector.");
        ISectorFactory sectorFactory = new CriminalSectorFactory();
        
        // Pirates encounter
        IEnemy pirate = sectorFactory.CreateEnemy();
        int pirateChoice = pirate.GetMenu();
        try
        {
            gameFacade.ResolveCombatEncounter(player, pirate, pirateChoice);
        } catch(PlayerIsDead e)
        {
            Console.WriteLine($"{e.Message}");
            return;
        }

        // Black merchant encounter
        IMerchant blackMarketTrader = sectorFactory.CreateMerchant();
        int marketChoice = blackMarketTrader.GetMenu();
        gameFacade.ResolveTradeEncounter(player, blackMarketTrader, marketChoice);

        Console.WriteLine("\n------------------------------------------------");
        Console.WriteLine(">>> Transition to peaceful sector.");
        sectorFactory = new PeacefulSectorFactory();
        
        // Impire patrol encounter
        IEnemy patrol = sectorFactory.CreateEnemy();
        int patrolChoice = patrol.GetMenu();
        try
        {
            gameFacade.ResolveCombatEncounter(player, patrol, patrolChoice);
        } catch(PlayerIsDead e)
        {
            Console.WriteLine($"{e.Message}");
            return;
        }

        // Merchant encounter
        IMerchant merchant = sectorFactory.CreateMerchant();
        int merchantChoice = merchant.GetMenu();
        gameFacade.ResolveTradeEncounter(player, merchant, merchantChoice);

        // Final stats
        Console.WriteLine($"\nFinal Status - Health: {player.Health}, Money: {player.Money}.");
        Console.WriteLine($"Weapon Specs: \nName- {player.EquippedWeapon.Name}\nDamage- {player.EquippedWeapon.Damage}\nAmmo- {player.EquippedWeapon.Ammo}\nAccuracy- {player.EquippedWeapon.Accuracy}%");    
    }
}