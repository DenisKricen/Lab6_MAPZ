using System;

public class Weapon
{
    public string Name { get; set; } = "None";
    public int Damage { get; set; }
    public int Ammo { get; set; }
    public int Accuracy { get; set; }
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

    public Weapon Build()
    {
        Weapon result = weapon;
        weapon = new Weapon(); 
        return result;
    }
}