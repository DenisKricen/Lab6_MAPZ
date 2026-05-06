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
        merchant.BuffPrice = 0;
        return merchant;
    }


    
}