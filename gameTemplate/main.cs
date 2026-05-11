using System;
using System.IO;

public class Program
{
    public static void Main()
    {
        Player player = Player.Instance;
        SpaceSimFacade gameFacade = new SpaceSimFacade();
        
        CommandHistory history = new CommandHistory();

        Console.WriteLine($"Initial Status - Health: {player.Health}, Money: {player.Money}, Weapon: {player.EquippedWeapon.Name}.");
        Console.WriteLine("\n------------------------------------------------");

        do
        {
            // Transition to challenge space sector
            TransitionCommand dangerJump = new TransitionCommand(new UltraChallengeCriminalSectorFactory(new CriminalSectorFactory()));
            history.ExecuteCommand(dangerJump);
            // Decorator usage: 50% normal behaviour and 50% for special mob
            ISectorFactory sectorFactory = dangerJump.GeneratedFactory;
            
            // Pirates encounter
            IEnemy pirate = sectorFactory.CreateEnemy();
            try
            {
                history.ExecuteCommand(new CombatCommand(gameFacade, player, pirate, pirate.GetMenu()));
            } 
            catch(PlayerIsDead e)
            {
                Console.WriteLine($"{e.Message}");
                break;
            }

            // Black merchant encounter
            IMerchant blackMarketTrader = sectorFactory.CreateMerchant();

            // Proxy usage: check player's wealth before allow to buy stuff
            IMerchant blackTraderProxy = new BlackMarketGuardProxy(blackMarketTrader, player);
            history.ExecuteCommand(new TradeCommand(gameFacade, player, blackTraderProxy, blackTraderProxy.GetMenu()));

            // Transition to peasful sector
            TransitionCommand peaceJump = new TransitionCommand(new PeacefulSectorFactory());
            history.ExecuteCommand(peaceJump);
            sectorFactory = peaceJump.GeneratedFactory;
                        
            // Impire patrol encounter
            IEnemy patrol = sectorFactory.CreateEnemy();
            int patrolChoice = patrol.GetMenu();
            try
            {
                history.ExecuteCommand(new CombatCommand(gameFacade, player, patrol, patrolChoice));
            } 
            catch(PlayerIsDead e)
            {
                Console.WriteLine($"{e.Message}");
                break;
            }

            // Merchant encounter
            IMerchant merchant = sectorFactory.CreateMerchant();
            int merchantChoice = merchant.GetMenu();
            
            history.ExecuteCommand(new TradeCommand(gameFacade, player, merchant, merchantChoice));

        } while(false);

        // Final stats
        Console.WriteLine($"\nFinal Status - Health: {player.Health}, Money: {player.Money}.");
        Console.WriteLine($"Weapon Specs: \nName- {player.EquippedWeapon.Name}\nDamage- {player.EquippedWeapon.Damage}\nAmmo- {player.EquippedWeapon.Ammo}\nAccuracy- {player.EquippedWeapon.Accuracy}%");    
        
        
        // Save journal history
        var originalConsoleOut = Console.Out;
        using (var stringWriter = new StringWriter())
        {
            Console.SetOut(stringWriter);
            history.PrintJournal();
            
            Console.SetOut(originalConsoleOut);
            string journalText = stringWriter.ToString();
            
            // Console.Write(journalText);
            try
            {
                File.WriteAllText("lastGameJournal.txt", journalText);
                string separator = $"\n================================================\n===         GAME       : {DateTime.Now:yyyy-MM-dd HH:mm:ss} ===\n================================================\n";
                File.AppendAllText("journal.txt", separator + journalText);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n[Error. Cant save file]: {ex.Message}");
            }
        }
    }
}