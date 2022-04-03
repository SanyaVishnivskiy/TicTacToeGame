namespace TicTacToeGame;

public interface IGame
{
    GameFinished FinishedInfo { get; }
    IBoardReader GetBoard();
    bool MakeMove(int x, int y);
    void Reset();
    IPlayerInfo[] GetPlayers();
    IPlayerInfo GetCurrentPlayer();
}

internal class Game : IGame
{
    private Board _board;
    private Player[] _players;
    private int _currentPlayerIndex;

    private Player CurrentPlayer => _players[_currentPlayerIndex];

    public GameFinished FinishedInfo { get; private set; }

    public Game(int boardSize, char[] players)
    {
        Init(boardSize, players);
    }

    private void Init(int boardSize, char[] players)
    {
        var transformedPlayers = players
            .Select(x => new Player(x))
            .ToArray();
        Init(boardSize, transformedPlayers);
    }

    private void Init(int boardSize, Player[] players)
    {
        _board = new Board(boardSize);
        _players = players;
        _currentPlayerIndex = 0;
        FinishedInfo = GameFinished.NotFinished();
    }

    public IBoardReader GetBoard()
    {
        return _board;
    }

    public bool MakeMove(int x, int y)
    {
        if (FinishedInfo.Finished)
        {
            return false;
        }

        if (!_board.SetValue(x, y, CurrentPlayer.Symbol))
        {
            return false;
        }

        if (_board.CheckPlayerWon(x, y, CurrentPlayer.Symbol))
        {
            FinishedInfo = GameFinished.FinishedWithWinner(CurrentPlayer.Symbol);
            return true;
        }

        if (_board.IsFull())
        {
            FinishedInfo = GameFinished.FinishedWithDraw();
            return true;
        }

        _currentPlayerIndex = (_currentPlayerIndex + 1) % _players.Length;
        return true;
    }

    public void Reset()
    {
        Init(_board.Size, _players);
    }

    public IPlayerInfo[] GetPlayers()
    {
        return _players;
    }

    public IPlayerInfo GetCurrentPlayer()
    {
        return CurrentPlayer;
    }
}
