namespace TicTacToeGame
{
    public interface IBoardReader
    {
        public char[,] ReadBoard();
    }
    
    internal class Board : IBoardReader
    {
        private const char EmptyValue = ' ';

        private readonly char[,] _board;

        public int Size { get; }

        public Board(int size)
        {
            Size = size;
            _board = new char[size, size];
            ClearBoard(_board);
        }

        private void ClearBoard(char[,] board)
        {
            for (var i = 0; i < board.GetLength(0); i++)
            {
                for (var j = 0; j < board.GetLength(1); j++)
                {
                    board[i, j] = EmptyValue;
                }
            }
        }

        public char GetValue(int x, int y)
        {
            return _board[x, y];
        }

        public bool SetValue(int x, int y, char value)
        {
            if (!CanSetValue(x, y))
            {
                return false;
            }

            _board[x, y] = value;
            return true;
        }

        private bool CanSetValue(int x, int y)
        {
            return x >= 0 && x < Size
                && y >= 0 && y < Size
                && IsEmpty(x, y);
        }

        public bool IsEmpty(int x, int y)
        {
            return _board[x, y] == EmptyValue;
        }

        public bool IsFull()
        {
            for (var i = 0; i < Size; i++)
            {
                for (var j = 0; j < Size; j++)
                {
                    if (IsEmpty(i, j))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public bool CheckPlayerWon(int x, int y, char value)
        {
            var row = 0;
            var column = 0;
            var diagonal = 0;
            var antiDiagonal = 0;

            for (var i = 0; i < Size; i++)
            {
                if (_board[x, i] == value)
                {
                    row++;
                }

                if (_board[i, y] == value)
                {
                    column++;
                }

                if (_board[i, i] == value)
                {
                    diagonal++;
                }

                if (_board[i, Size - i - 1] == value)
                {
                    antiDiagonal++;
                }
            }

            return row == Size || column == Size || diagonal == Size || antiDiagonal == Size;
        }

        public char[,] ReadBoard()
        {
            return _board;
        }
    }
}