using System;
using System.Collections.Generic;

public interface ICommand
{
    void Execute();
    string GetLogMessage();
}

// Stores manages commands and stores history
public class CommandHistory
{
    private List<ICommand> history = new List<ICommand>();

    public void ExecuteCommand(ICommand command)
    {
        history.Add(command);
        command.Execute();
    }

    public void PrintJournal()
    {
        if (history.Count == 0)
        {
            Console.WriteLine("The onboard log is empty");
            return;
        }

        foreach (var cmd in history)
        {
            Console.WriteLine($"- {cmd.GetLogMessage()}");
        }
    }
}

public class TransitionCommand : ICommand
{
    private DateTime timestamp;

    public ISectorFactory GeneratedFactory { get; private set; }

    public TransitionCommand(ISectorFactory factory)
    {
        this.GeneratedFactory = factory;
        this.timestamp = DateTime.Now;
    }

    public void Execute()
    {
        string sectorName = GeneratedFactory.GetType().Name;
        Console.WriteLine($"\n>>> Transition to {sectorName}\n");
    }

    public string GetLogMessage()
    {
        string sectorName = GeneratedFactory.GetType().Name;
        return $"[{timestamp:HH:mm:ss}] Warp jump completed. Arrived at: {sectorName}.";
    }
}

public class CombatCommand : ICommand
{
    private SpaceSimFacade facade;
    private Player player;
    private IEnemy enemy;
    private int choice;
    private DateTime timestamp;

    public CombatCommand(SpaceSimFacade facade, Player player, IEnemy enemy, int choice)
    {
        this.facade = facade;
        this.player = player;
        this.enemy = enemy;
        this.choice = choice;
        this.timestamp = DateTime.Now;
    }

    public void Execute()
    {
        facade.ResolveCombatEncounter(player, enemy, choice);
    }

    public string GetLogMessage()
    {
        string enemyType = enemy.GetType().Name;
        return $"[{timestamp:HH:mm:ss}] Combat encounter initiated with {enemyType}. Action code: {choice}.";
    }
}

public class TradeCommand : ICommand
{
    private SpaceSimFacade facade;
    private Player player;
    private IMerchant merchant;
    private int choice;
    private DateTime timestamp;

    public TradeCommand(SpaceSimFacade facade, Player player, IMerchant merchant, int choice)
    {
        this.facade = facade;
        this.player = player;
        this.merchant = merchant;
        this.choice = choice;
        this.timestamp = DateTime.Now;
    }

    public void Execute()
    {
        facade.ResolveTradeEncounter(player, merchant, choice);
    }

    public string GetLogMessage()
    {
        string merchantType = merchant.GetType().Name;
        return $"[{timestamp:HH:mm:ss}] Trade session with {merchantType}. Action code: {choice}.";
    }
}