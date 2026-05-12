using System;

public abstract class DefenseHandler
{
    private DefenseHandler nextHandler;

    public DefenseHandler SetNext(DefenseHandler handler)
    {
        this.nextHandler = handler;
        return handler; 
    }

    public virtual void HandleDamage(Player player, int damage)
    {
        if (nextHandler != null && damage > 0)
        {
            nextHandler.HandleDamage(player, damage);
        }
    }
}

public class ShieldHandler : DefenseHandler
{
    public override void HandleDamage(Player player, int damage)
    {
        if (player.Shield > 0)
        {
            int absorbed = Math.Min(player.Shield, damage);
            player.Shield -= absorbed;
            damage -= absorbed;

            Console.WriteLine($"[Defense] Shield took {absorbed} damage. Remaining  durability: {player.Shield}");
        }

        if(damage > 0)
            base.HandleDamage(player, damage);
    }
}

public class ArmorHandler : DefenseHandler
{
    public override void HandleDamage(Player player, int damage)
    {
        if (player.Armor > 0 && damage > 0)
        {

            int absorbed = Math.Min(player.Armor, damage);
            player.Armor-= absorbed;
            damage -= absorbed;


            Console.WriteLine($"[Defense] Armor took {absorbed} damage. Remaining durability: {player.Armor}");
        }

        if(damage > 0)
            base.HandleDamage(player, damage);
    }
}

public class HealthHandler : DefenseHandler
{
    public override void HandleDamage(Player player, int damage)
    {
        if (damage > 0)
        {
            player.Health -= damage;
            Console.WriteLine($"[Defense] Main health took {damage} direct damage. Remaining health: {player.Health}");

            if (player.Health <= 0)
            {
                throw new PlayerIsDead("No HP have been left. Player is dead."); 
            }
        }
    }
}