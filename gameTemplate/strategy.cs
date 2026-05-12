using System;

public interface ITradingStrategy
{
    int CalculatePrice(int basePrice);
    Weapon GetWeapon(Weapon baseWeapon);
}

public class FairTradeStrategy : ITradingStrategy
{
    public int CalculatePrice(int basePrice) 
    {
        return basePrice;
    }

    public Weapon GetWeapon(Weapon baseWeapon) 
    {
        return baseWeapon;
    }
}

public class ScamTradeStrategy : ITradingStrategy
{
    private Random rand = new Random();

    public int CalculatePrice(int basePrice)
    {
        double price = basePrice;

        if (rand.NextDouble() < 0.5) 
        {
            price *= 1.2;
        }
        return (int)price;
    }

    public Weapon GetWeapon(Weapon baseWeapon)
    {

        Weapon brokenWeapon = baseWeapon.Clone();
        brokenWeapon.Name = baseWeapon.Name;
        brokenWeapon.Damage = Math.Min(1, baseWeapon.Damage / 2);
        brokenWeapon.Ammo = Math.Min(1, (int)Math.Ceiling(baseWeapon.Ammo * 0.75));
        brokenWeapon.Accuracy = Math.Min(10, baseWeapon.Accuracy - 30);
        
        return brokenWeapon;
        
    }
}