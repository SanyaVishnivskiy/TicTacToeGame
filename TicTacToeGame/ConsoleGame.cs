namespace TicTacToeGame
{
    public class ConsoleGame
    {
        private IGame _game;
        private BoardRenderer _boardRenderer;
        private bool _exited;

        public ConsoleGame(IGame game)
        {
            _game = game;
            _boardRenderer = new BoardRenderer(game);
        }

        public void Play()
        {
            _exited = false;
            _boardRenderer.Render();
            GameLoop();
        }

        private void GameLoop()
        {
            while (!_exited)
            {
                var input = Console.ReadKey().Key;
                ProcessInput(input);
                _boardRenderer.Render();
            }
        }

        private void ProcessInput(ConsoleKey input)
        {
            switch (input)
            {
                case ConsoleKey.Enter:
                    ProcessMakeMove();
                    return;
                case ConsoleKey.Escape:
                    _exited = true;
                    return;
                case ConsoleKey.R:
                    _game.Reset();
                    return;
                case ConsoleKey.LeftArrow:
                    _boardRenderer.MoveSelectedCell(-1, 0);
                    return;
                case ConsoleKey.UpArrow:
                    _boardRenderer.MoveSelectedCell(0, -1);
                    return;
                case ConsoleKey.RightArrow:
                    _boardRenderer.MoveSelectedCell(1, 0);
                    return;
                case ConsoleKey.DownArrow:
                    _boardRenderer.MoveSelectedCell(0, 1);
                    return;
                default:
                    return;
            }
        }

        private void ProcessMakeMove()
        {
            var selectedCell = _boardRenderer.GetSelectedCell();
            _game.MakeMove(selectedCell.Row, selectedCell.Column);
        }
    }
}
