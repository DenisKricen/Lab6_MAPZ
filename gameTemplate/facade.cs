using System.Reflection.Emit;

public class SpaceSimFacade
{
    private CombatSystem combat;
    private EconomySystem economy;

    public SpaceSimFacade()
    {
        combat = new CombatSystem();
        economy = new EconomySystem();
    }

    public void ResolveCombatEncounter(Player player, IEnemy enemy, int choice)
    {
        string message = enemy.GetActionMessage(choice);

        switch (choice)
        {
            
            case 1: // Fight
                try
                {
                    combat.TakeDamage(player, enemy.Damage, message);
                    economy.AddMoney(player, enemy.LootMoney);
                    
                } catch (PlayerIsDead e)
                {
                    throw e;
                }
                break;

            case 2: // Escape / Bribe

                if (economy.TrySpendMoney(player, enemy.EscapeCost))
                {
                    Console.WriteLine($"[Economy] Paid {enemy.EscapeCost} credits.");
                }
                else
                {
                    Console.WriteLine("[Combat] Not enough money for a bribe. You will have to fight.");
                    goto case 1;
                }
                break;
        }
    }

    public void ResolveTradeEncounter(Player player, IMerchant merchant, int choice)
    {
        string message = merchant.GetActionMessage(choice);

        switch (choice)
        {
            case 1: // Buy a weapon (PROTOTYPE USAGE)
                if (economy.TrySpendMoney(player, merchant.WeaponPrice))
                {
                    player.EquippedWeapon = merchant.WeaponItem.Clone();
                    Console.WriteLine($"[Trade] {message} Installed: {player.EquippedWeapon.Name}");
                } else
                {
                    Console.WriteLine($"Player do not have enough money for this weapon. Has: {player.Money}, required: {merchant.WeaponPrice}.");
                }
                break;

            case 2: // Buy a repair kit
                if (economy.TrySpendMoney(player, merchant.RepairKitPrice))
                {
                    combat.Heal(player, player.MaxHealth, message);
                } else
                {
                    Console.WriteLine($"[Trade] Player do not have enough money for repair kit. Has: {player.Money}, required: {merchant.WeaponPrice}.");
                }
                break;

            case 3: // Buy a buff
                if (economy.TrySpendMoney(player, merchant.BuffPrice))
                {
                    player.MaxHealth += 50;
                    Console.WriteLine($"[Trade] {message} Buff received.");
                } else
                {
                    Console.WriteLine($"[Trade] Player do not have enough money for buff. Has: {player.Money}, required: {merchant.WeaponPrice}.");
                }
                break;
            case 4: // Leave
                Console.WriteLine("[Trade] You're leaving");
                return;

            default:
                Console.WriteLine("UNKOWN OPTION FOR TRADE ENCOUNTER.");
                break;
        }
    }
}