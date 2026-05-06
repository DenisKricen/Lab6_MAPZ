using System;

public interface ISectorFactory
{
    IEnemy CreateEnemy();
    IMerchant CreateMerchant();
}

public class PeacefulSectorFactory : ISectorFactory
{
    private static Random random = new Random();

    public IEnemy CreateEnemy()
    {
        int damage = random.Next(30, 71);
        int lootMoney = (int)(damage * 2.5);
        int escapeDamage = (int)(damage * 2 / 3.0);
        int escapeCost = (int)(damage * 1.5);

        return new EmpirePatrol
        {
            Damage = damage,
            LootMoney = lootMoney,
            EscapeDamage = escapeDamage,
            EscapeCost = escapeCost
        };
    }

    public IMerchant CreateMerchant()
    {
        var merchant = new Merchant();
        merchant.WeaponItem = null;
        merchant.WeaponPrice = 0;
        merchant.RepairKitPrice = 100;
        merchant.BuffPrice = 0;
        return merchant;
    }
}

public class CriminalSectorFactory : ISectorFactory
{
    private static Random random = new Random();

    public IEnemy CreateEnemy()
    {
        int damage = random.Next(30, 71);
        int lootMoney = (int)(damage * 2.5);
        int escapeDamage = (int)(damage * 2 / 3.0);
        int escapeCost = (int)(damage * 1.5);

        return new PirateShip
        {
            Damage = damage,
            LootMoney = lootMoney,
            EscapeDamage = escapeDamage,
            EscapeCost = escapeCost
        };
    }

    public IMerchant CreateMerchant()
    {
        var merchant = new BlackMarketTrader();
        var weapon = WeaponManager.GenerateRandomWeapon();
        merchant.WeaponItem = weapon;
        merchant.WeaponPrice = weapon.Price;
        merchant.RepairKitPrice = 0;
        merchant.BuffPrice = 100;
        return merchant;
    }
    
}


// Decorator template
public abstract class SectorFactoryDecorator : ISectorFactory {

    protected ISectorFactory wrapper = null;

    protected SectorFactoryDecorator(ISectorFactory factory) {
        wrapper = factory;
    } 

    public virtual IEnemy CreateEnemy() {
        return wrapper.CreateEnemy();
    }

    public virtual IMerchant CreateMerchant() {
        return wrapper.CreateMerchant();
    }
}

// Decorator factory that sometimes delegates to the base factory and
// sometimes generates "ultra" hard encounters (enemy or merchant).
public class UltraChallengeCriminalSectorFactory : SectorFactoryDecorator
{
    private static Random random = new Random();

    public UltraChallengeCriminalSectorFactory(ISectorFactory factory) : base(factory) {}

    public override IEnemy CreateEnemy()
    {
        // 50% chance to use base behavior
        if (random.NextDouble() < 0.5)
            return wrapper.CreateEnemy();

        // Otherwise create an ultra-hard pirate ship
        Console.WriteLine("[SPECIAL ENEMY] Mega pirate ship is coming");
        int damage = random.Next(100, 201);
        int lootMoney = damage * 4;
        int escapeDamage = (int)(damage * 2 / 3.0);
        int escapeCost = damage * 3;

        return new PirateShip
        {
            Damage = damage,
            LootMoney = lootMoney,
            EscapeDamage = escapeDamage,
            EscapeCost = escapeCost
        };
    }

    public override IMerchant CreateMerchant()
    {
        // 50% chance to use base behavior
        if (random.NextDouble() < 0.5)
            return wrapper.CreateMerchant();

        Console.WriteLine("[Me] Something off with this merchant...");
        var merchant = new BlackMarketTrader();
        var weapon = WeaponManager.GenerateRandomWeapon();
        weapon.Damage = random.Next(10, 21);        
        weapon.Accuracy = random.Next(60, 71);      
        weapon.Ammo = random.Next(15, 26);       

        merchant.WeaponItem = weapon;
        merchant.WeaponPrice = weapon.Price;
        merchant.RepairKitPrice = 0;
        merchant.BuffPrice = 100;
        return merchant;
    }
}
