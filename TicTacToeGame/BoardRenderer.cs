using System.Text;

namespace TicTacToeGame
{
    internal class BoardRenderer
    {
        private const ConsoleColor DefaultConsoleColor = ConsoleColor.White;
        private const ConsoleColor SelectedConsoleColor = ConsoleColor.Yellow;
        private const ConsoleColor GameFinishedConsoleColor = ConsoleColor.Green;

        private const char BorderCell = '#';

        private const int SideBorderWidth = 1;
        private const int BorderBetweenCellsWidth = 1;
        private const int CellWidth = 3;

        private readonly IGame _game;

        private Cell _selectedCell;

        public BoardRenderer(IGame game)
        {
            _game = game;
            _selectedCell = GetCellPosition(0, 0);
        }

        public void Render()
        {
            var board = _game.GetBoard().ReadBoard();
            var boardSymbols = BuildBoardSymbols(board);

            Console.Clear();
            RenderTitle();
            RenderBoard(boardSymbols);
            if (_game.FinishedInfo.Finished)
            {
                Console.WriteLine();
                RenderFinishedInfo();
            }
            Console.WriteLine();
            RenderPlayersInfo();
            Console.WriteLine();
            RenderControls();
        }

        private BoardSymbolInfo[,] BuildBoardSymbols(char[,] board)
        {
            var boardSize = CalculateBoardSize(board);
            var boardInfo = new BoardSymbolInfo[boardSize, boardSize];
            FillBoardInfoWithSymbols(boardInfo, board);
            ColorizeBoardSymbols(boardInfo);
            return boardInfo;
        }

        private int CalculateBoardSize(char[,] board)
        {
            var size = board.GetLength(0);
            return size * CellWidth // every cell is 3 characters wide
                + (size - 1) * BorderBetweenCellsWidth // boarders between cells are 3 characters wide
                + SideBorderWidth * 2; // 2 characters for one boarder
        }

        private void FillBoardInfoWithSymbols(BoardSymbolInfo[,] boardInfo, char[,] board)
        {
            FillEverythingWithBorder(boardInfo);
            for (var row = 0; row < board.GetLength(0); row++)
            {
                for (var column = 0; column < board.GetLength(1); column++)
                {
                    var cell = GetCellPosition(row, column);
                    FillCellToBoardInfo(boardInfo, cell, board[row, column]);
                }
            }
        }

        private void FillEverythingWithBorder(BoardSymbolInfo[,] boardInfo)
        {
            for (var row = 0; row < boardInfo.GetLength(0); row++)
            {
                for (var column = 0; column < boardInfo.GetLength(1); column++)
                {
                    boardInfo[row, column] = new BoardSymbolInfo(BorderCell, DefaultConsoleColor);
                }
            }
        }

        private Cell GetCellPosition(int row, int column)
        {
            var leftX = SideBorderWidth + (row * (CellWidth + BorderBetweenCellsWidth));
            var topY = SideBorderWidth + (column * (CellWidth + BorderBetweenCellsWidth));
            return new Cell(leftX, topY, row, column, CellWidth);
        }

        private void FillCellToBoardInfo(BoardSymbolInfo[,] boardInfo, Cell cell, char cellValue)
        {
            char[,] cellSymbol = CellValues.Get(cellValue, cell.Width);
            char[,] rotatedCellSymbol = RotateCellSymbol(cellSymbol);
            for (var row = cell.LeftX; row < cell.LeftX + cell.Width; row++)
            {
                for (var column = cell.TopY; column < cell.TopY + cell.Width; column++)
                {
                    var cellSymbolX = row - cell.LeftX;
                    var cellSymbolY = column - cell.TopY;
                    var boardSymbolValue = rotatedCellSymbol[cellSymbolX, cellSymbolY];
                    boardInfo[row, column] = new BoardSymbolInfo(boardSymbolValue, DefaultConsoleColor);
                }
            }
        }

        private char[,] RotateCellSymbol(char[,] cellSymbol)
        {
            var rotatedCellSymbol = new char[cellSymbol.GetLength(0), cellSymbol.GetLength(1)];
            for (var row = 0; row < cellSymbol.GetLength(0); row++)
            {
                for (var column = 0; column < cellSymbol.GetLength(1); column++)
                {
                    rotatedCellSymbol[row, column] = cellSymbol[column, row];
                }
            }
            return rotatedCellSymbol;
        }

        private void ColorizeBoardSymbols(BoardSymbolInfo[,] boardInfo)
        {
            for (int row = _selectedCell.LeftX - 1; row < _selectedCell.LeftX  + _selectedCell.Width + 1; row++)
            {
                for (int column = _selectedCell.TopY - 1; column < _selectedCell.TopY + _selectedCell.Width + 1; column++)
                {
                    boardInfo[row, column].SetColor(SelectedConsoleColor);
                }
            }
        }

        private void RenderTitle()
        {
            Console.WriteLine("Tic Tac Toe Game");
        }

        private void RenderBoard(BoardSymbolInfo[,] boardSymbols)
        {
            var rendeningInfo = GetTextChunksWithColor(boardSymbols);

            foreach (var chunk in rendeningInfo)
            {
                SetConsoleColor(chunk.Color);
                Console.Write(chunk.Text);
            }

            SetConsoleColor(DefaultConsoleColor);
        }

        private List<RenderingInfo> GetTextChunksWithColor(BoardSymbolInfo[,] board)
        {
            var boardString = new StringBuilder();
            var currentColor = DefaultConsoleColor;
            var result = new List<RenderingInfo>();

            for (int row = 0; row < board.GetLength(0); row++)
            {
                for (int column = 0; column < board.GetLength(1); column++)
                {
                    var symbol = board[column, row];
                    if (symbol.Color != currentColor)
                    {
                        result.Add(new RenderingInfo(boardString.ToString(), currentColor));
                        boardString = new StringBuilder();
                        currentColor = currentColor == DefaultConsoleColor 
                            ? SelectedConsoleColor
                            : DefaultConsoleColor;
                    }
                    boardString.Append(symbol.Symbol);
                }
                boardString.AppendLine();
            }

            result.Add(new RenderingInfo(boardString.ToString(), currentColor));

            return result;
        }

        private void RenderFinishedInfo()
        {
            SetConsoleColor(GameFinishedConsoleColor);

            Console.WriteLine("Game finished!");
            var finishedInfo = _game.FinishedInfo;
            if (finishedInfo.FinishedType == FinishedType.Win)
            {
                Console.WriteLine($"Player {finishedInfo.Winner} won!");
                return;
            }

            if (finishedInfo.FinishedType == FinishedType.Draw)
            {
                Console.WriteLine("It's a Draw!");
                return;
            }

            SetConsoleColor(DefaultConsoleColor);
        }

        private void RenderPlayersInfo()
        {
            var currentPlayer = _game.GetCurrentPlayer();
            foreach (var player in _game.GetPlayers())
            {
                if (player != currentPlayer)
                {
                    Console.WriteLine($"Player {player.Symbol}");
                    continue;
                }

                SetConsoleColor(SelectedConsoleColor);
                Console.WriteLine($"Player {player.Symbol} move is now");
                SetConsoleColor(DefaultConsoleColor);
            }
        }

        private void RenderControls()
        {
            Console.WriteLine("Controls:");
            Console.WriteLine("Arrow keys - move between cells");
            Console.WriteLine("Enter - make move");
            Console.WriteLine("R - restart game");
            Console.WriteLine("Esc - exit");
        }

        private void SetConsoleColor(ConsoleColor color)
        {
            Console.ForegroundColor = color;
        }

        public Cell GetSelectedCell()
        {
            return _selectedCell;
        }

        public void MoveSelectedCell(int currentRowOffset, int currentColumnOffset)
        {
            var newRow = _selectedCell.Row + currentRowOffset;
            var newColumn = _selectedCell.Column + currentColumnOffset;
            if (CheckCorrectPosition(newRow, newColumn))
            {
                _selectedCell = GetCellPosition(newRow, newColumn);
            }
        }

        private bool CheckCorrectPosition(int row, int column)
        {
            var board = _game.GetBoard().ReadBoard();
            return row >= 0 && row < board.GetLength(0)
                && column >= 0 && column < board.GetLength(1);
        }
    }
}

internal class BoardSymbolInfo
{
    public char Symbol { get; }
    public ConsoleColor Color { get; private set; }

    public BoardSymbolInfo(char symbol, ConsoleColor color)
    {
        Symbol = symbol;
        Color = color;
    }

    public void SetColor(ConsoleColor color)
    {
        Color = color;
    }

    public override string ToString()
    {
        return $"{Symbol}, {Color}";
    }
}

internal class RenderingInfo
{
    public string Text { get; }
    public ConsoleColor Color { get; }

    public RenderingInfo(string text, ConsoleColor color)
    {
        Text = text;
        Color = color;
    }

    public override string ToString()
    {
        return $"{Text}, {Color}";
    }
}
