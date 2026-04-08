public interface IEntity
{
    int GetMenu(); 
}

public class EmpirePatrol : IEntity
{
    public int GetMenu()
    {
        Console.WriteLine("\n[COMBAT] Imperial patrol encounter.");
        Console.WriteLine("1. Fire.");
        Console.WriteLine("2. Attempt escape.");
        Console.Write("Input: ");
        
        int.TryParse(Console.ReadLine(), out int choice);
        return choice;
    }
}

public class Merchant : IEntity
{
    public int GetMenu()
    {
        Console.WriteLine("\n[SHOP] Trading station docked.");
        Console.WriteLine("1. Buy repair kit (50 credits).");
        Console.WriteLine("2. Leave.");
        Console.Write("Input: ");

        int.TryParse(Console.ReadLine(), out int choice);
        return choice;
    }
}

public class PirateShip : IEntity
{
    public int GetMenu()
    {
        Console.WriteLine("\n[COMBAT] Space pirate attack.");
        Console.WriteLine("1. Fire.");
        Console.WriteLine("2. Bribe (100 credits).");
        Console.Write("Input: ");

        int.TryParse(Console.ReadLine(), out int choice);
        return choice;
    }
}

public class BlackMarketTrader : IEntity
{
    public int GetMenu()
    {
        Console.WriteLine("\n[SHOP] Black market accessed.");
        Console.WriteLine("1. Buy weapons (500 credits).");
        Console.WriteLine("2. Leave.");
        Console.Write("Input: ");
        
        int.TryParse(Console.ReadLine(), out int choice);
        return choice;
    }
}