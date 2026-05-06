using System;

public class Program
{
    public static void Main()
    {

        Player player = Player.Instance;
        SpaceSimFacade gameFacade = new SpaceSimFacade();

        do
        {

            Console.WriteLine($"Initial Status - Health: {player.Health}, Money: {player.Money}, Weapon: {player.EquippedWeapon.Name}.");

            Console.WriteLine("\n------------------------------------------------");
            Console.WriteLine("\n>>> Transition to dangerous sector.");
            // ISectorFactory sectorFactory = new CriminalSectorFactory();

            // Decorator usage: 50% normal behaviour and 50% for special mob
            ISectorFactory sectorFactory = new UltraChallengeCriminalSectorFactory(new CriminalSectorFactory());
            
            // Pirates encounter
            IEnemy pirate = sectorFactory.CreateEnemy();
            int pirateChoice = pirate.GetMenu();
            try
            {
                gameFacade.ResolveCombatEncounter(player, pirate, pirateChoice);
            } catch(PlayerIsDead e)
            {
                Console.WriteLine($"{e.Message}");
                break;
            }

            // Black merchant encounter


            IMerchant blackMarketTrader = sectorFactory.CreateMerchant();

            // Proxy usage: check player's wealth before allow to buy stuff
            IMerchant blackTraderProxy = new BlackMarketGuardProxy(blackMarketTrader, player);

            int marketChoice = blackTraderProxy.GetMenu();
            gameFacade.ResolveTradeEncounter(player, blackTraderProxy, marketChoice);

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
                break;
            }

            // Merchant encounter
            IMerchant merchant = sectorFactory.CreateMerchant();
            int merchantChoice = merchant.GetMenu();
            gameFacade.ResolveTradeEncounter(player, merchant, merchantChoice);

        } while(false);


        // Final stats
        Console.WriteLine($"\nFinal Status - Health: {player.Health}, Money: {player.Money}.");
        Console.WriteLine($"Weapon Specs: \nName- {player.EquippedWeapon.Name}\nDamage- {player.EquippedWeapon.Damage}\nAmmo- {player.EquippedWeapon.Ammo}\nAccuracy- {player.EquippedWeapon.Accuracy}%");    
    }
}