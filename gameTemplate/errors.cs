class PlayerIsDead : Exception
{
    public PlayerIsDead() : base("[Game Over] Player is dead")
    {
    }

    public PlayerIsDead(string message) : base(message)
    {
    }

    public PlayerIsDead(string message, Exception innerException) 
        : base(message, innerException)
    {
    }
}