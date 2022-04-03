namespace TicTacToeGame
{
    internal class Player : IPlayerInfo
    {
        public char Symbol { get; }

        public Player(char symbol)
        {
            Symbol = symbol;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (ReferenceEquals(this, null) || ReferenceEquals(obj, null))
            {
                return false;
            }

            if (GetType() != obj.GetType())
            {
                return false;
            }

            return Symbol == ((Player)obj).Symbol;
        }

        public override int GetHashCode()
        {
            if (this is null || Symbol == default(char))
            {
                return 0;
            }

            return Symbol.GetHashCode();
        }
    }
}