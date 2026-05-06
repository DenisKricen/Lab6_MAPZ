using System;

public class Weapon
{
    public string Name { get; set; }
    public int Damage { get; set; }
    public int Ammo { get; set; }
    public int Accuracy { get; set; }
    public int Price { get; set; }

    public Weapon Clone()
    {
        return (Weapon)this.MemberwiseClone();
    }
}

public class WeaponBuilder
{
    private Weapon weapon;

    public WeaponBuilder()
    {
        weapon = new Weapon();
    }

    public WeaponBuilder SetName(string name)
    {
        weapon.Name = name;
        return this;
    }

    public WeaponBuilder SetDamage(int damage)
    {
        weapon.Damage = damage;
        return this;
    }

    public WeaponBuilder SetAmmo(int ammo)
    {
        weapon.Ammo = ammo;
        return this;
    }

    public WeaponBuilder SetAccuracy(int accuracyPercent)
    {
        weapon.Accuracy = accuracyPercent;
        return this;
    }

    public WeaponBuilder SetPrice(int price)
    {
        weapon.Price = price;
        return this;
    }

    public Weapon Build()
    {
        Weapon result = weapon;
        weapon = new Weapon(); 
        return result;
    }

}

static class WeaponManager
{
    private static Random random = new Random();

    public static Weapon GenerateRandomWeapon()
    {
        int weaponType = random.Next(0, 3);
        int rarity = random.Next(0, 3);

        string rarityName = rarity switch
        {
            0 => "Regular",
            1 => "Epic",
            2 => "Legendary",
            _ => "Regular"
        };

        int baseDamage = 0;
        int baseAccuracy = 0;
        int baseAmmo = 0;
        string weaponName = "";

        switch (weaponType)
        {
            case 0: // Blaster
                baseDamage = 25;
                baseAccuracy = 70;
                baseAmmo = 30;
                weaponName = $"Blaster {rarityName}";
                break;
            case 1: // Laser
                baseDamage = 35;
                baseAccuracy = 80;
                baseAmmo = 50;
                weaponName = $"Laser {rarityName}";
                break;
            case 2: // Plasma cannon
                baseDamage = 45;
                baseAccuracy = 90;
                baseAmmo = 60;
                weaponName = $"Plasma Cannon {rarityName}";
                break;
        }

        int damageBonus = 0;
        int accuracyBonus = 0;

        switch (rarity)
        {
            case 0: // Regular: +0-30 damage, +0-15 accuracy
                damageBonus = random.Next(0, 31);
                accuracyBonus = random.Next(0, 16);
                break;
            case 1: // Epic: +15-50 damage, +0-10 accuracy
                damageBonus = random.Next(15, 51);
                accuracyBonus = random.Next(0, 11);
                break;
            case 2: // Legendary: +30-70 damage, +0-10 accuracy
                damageBonus = random.Next(30, 71);
                accuracyBonus = random.Next(0, 11);
                break;
        }

        int finalDamage = baseDamage + damageBonus;
        int finalAccuracy = baseAccuracy + accuracyBonus;

        // Estimate price, witch depends on rarity, damage and accuracy
        int rarityMultiplier = rarity switch
        {
            0 => 100,      // Regular base price: 100
            1 => 250,      // Epic base price: 250
            2 => 500,      // Legendary base price: 500
            _ => 100
        };

        int damagePrice = finalDamage * 5;      // 5 for each damage 
        int accuracyPrice = finalAccuracy * 2;  // 2 for each accuracy
        int totalPrice = rarityMultiplier + damagePrice + accuracyPrice;

        var builder = new WeaponBuilder();
        return builder
            .SetName(weaponName)
            .SetDamage(finalDamage)
            .SetAccuracy(finalAccuracy)
            .SetAmmo(baseAmmo)
            .SetPrice(totalPrice)
            .Build();
    }
}