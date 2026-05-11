public interface IEntity
{
    int GetMenu();
}

public interface IEnemy : IEntity
{
    int Damage { get; }
    int LootMoney { get; }
    int EscapeDamage { get; }
    int EscapeCost { get; }   
    
    string GetActionMessage(int choice);
}

public interface IMerchant : IEntity
{
    Weapon WeaponItem { get; }
    int WeaponPrice { get; }
    int RepairKitPrice { get; }
    int BuffPrice { get; }
    
    string GetActionMessage(int choice);
    IMerchant SetStrategy(ITradingStrategy strategy);
}