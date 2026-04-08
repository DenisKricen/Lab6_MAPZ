using System;

public interface ISectorFactory
{
    IEntity CreateEnemy();
    IEntity CreateMerchant();
}

public class PeacefulSectorFactory : ISectorFactory
{
    public IEntity CreateEnemy() => new EmpirePatrol();
    public IEntity CreateMerchant() => new Merchant();
}

public class CriminalSectorFactory : ISectorFactory
{
    public IEntity CreateEnemy() => new PirateShip();
    public IEntity CreateMerchant() => new BlackMarketTrader();
}