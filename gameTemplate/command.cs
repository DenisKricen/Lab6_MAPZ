using System;
using System.Collections.Generic;

// 1. Common interface for all commands
public interface ICommand
{
    void Execute();
    string GetLogMessage();
}

// 2. Invoker - class that manages commands and stores history
public class CommandHistory
{
    private List<ICommand> history = new List<ICommand>();

    public void ExecuteCommand(ICommand command)
    {
        // Add the command to history BEFORE execution.
        // If an Exception is thrown during Execute() (for example, the player dies),
        // the last action will still remain in the onboard log.
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

// 3. Command for transitioning between sectors
public class TransitionCommand : ICommand
{
    private string sectorName;
    private DateTime timestamp;

    public TransitionCommand(string sectorName)
    {
        this.sectorName = sectorName;
        this.timestamp = DateTime.Now;
    }

    public void Execute()
    {
        Console.WriteLine($"\n>>> Transition to {sectorName}.");
    }

    public string GetLogMessage()
    {
        return $"[{timestamp:HH:mm:ss}] Warp jump completed. Arrived at: {sectorName}.";
    }
}

// Command for conducting combat
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
        // Get the name of the enemy class (for example, "PirateShip" or "EmpirePatrol")
        string enemyType = enemy.GetType().Name;
        return $"[{timestamp:HH:mm:ss}] Combat encounter initiated with [{enemyType}]. Action code: {choice}.";
    }
}

// 5. Command for trading
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
        // Get the name of the merchant class
        string merchantType = merchant.GetType().Name;
        return $"[{timestamp:HH:mm:ss}] Trade session with [{merchantType}]. Action code: {choice}.";
    }
}