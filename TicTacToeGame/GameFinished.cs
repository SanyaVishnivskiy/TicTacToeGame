namespace TicTacToeGame;

public class GameFinished
{
    public bool Finished { get; }
    public FinishedType FinishedType { get; }
    public char Winner { get; }

    private GameFinished(bool finished, FinishedType finishedType, char winner)
    {
        Finished = finished;
        FinishedType = finishedType;
        Winner = winner;
    }

    public static GameFinished NotFinished()
    {
        return new GameFinished(false, FinishedType.NotFinished, default);
    }

    public static GameFinished FinishedWithWinner(char winner)
    {
        return new GameFinished(true, FinishedType.Win, winner);
    }

    public static GameFinished FinishedWithDraw()
    {
        return new GameFinished(true, FinishedType.Draw, default);
    }
}

public enum FinishedType
{
    NotFinished,
    Draw,
    Win,
}
