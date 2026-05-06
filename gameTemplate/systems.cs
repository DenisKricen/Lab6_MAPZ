public class CombatSystem
{
    public void TakeDamage(Player player, int amount, string message)
    {
        if(amount < 0)
        {
            Console.WriteLine($"[Error] Unit gave negative damage somehow: {amount}");
            return;
        }

        if (player.Health <= amount)
        {
            player.Health -= amount;
            Console.WriteLine("[Dead] You have no health left, you are dead.");
            throw new PlayerIsDead();
        }


        player.Health -= amount;
        Console.WriteLine($"[Combat] {message} Received {amount} damage. Remaining HP: {player.Health}");
    }

    public void Heal(Player player, int amount, string message)
    {
        player.Health = Math.Min(player.MaxHealth, player.Health + amount);
        Console.WriteLine($"[Healing] {message} Health: {player.Health}");
    }
}

public class EconomySystem
{
    public bool TrySpendMoney(Player player, int amount)
    {
        if (amount >= 0 && player.Money >= amount)
        {
            player.Money -= amount;
            return true;
        }
        return false; 
    }

    public void AddMoney(Player player, int amount)
    {
        if (amount >= 0)
        {
            player.Money += amount;
            Console.WriteLine($"[Economy] Received {amount} credits. Balance: {player.Money}");
        } else
        {
            Console.WriteLine($"[Error] Apparently there is some bag in the system. Tryed to add '{amount} to players account.'");
        }
    }
}