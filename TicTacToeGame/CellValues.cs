namespace TicTacToeGame
{
    internal class CellValues
    {
        public static char[,] Get(char value, int width)
        {
            return char.ToUpper(value) switch
            {
                'X' => GetX(width),
                'O' => GetO(width),
                'H' => GetH(width),
                'T' => GetT(width),
                ' ' => GetEmpty(width),
                _ => GetEmpty(width),
            };
        }

        private static char[,] GetX(int width)
        {
            if (width == 5)
            {
                return new char[,]
                {
                    { ' ', ' ', ' ', ' ', ' ' },
                    {' ', '\\', ' ', '/', ' '},
                    {' ', ' ', 'X', ' ', ' '},
                    {' ', '/', ' ', '\\', ' ',},
                    {' ', ' ', ' ', ' ', ' '},
                };
            }
            
            if (width == 3)
            {
                return new char[,]
                {
                    {'\\', ' ', '/'},
                    {' ', 'X', ' '},
                    {'/', ' ', '\\'}
                };
            }

            return new char[,] { { 'X' } };
        }

        private static char[,] GetO(int width)
        {
            if (width == 5)
            {
                return new char[,]
                {
                    { ' ', ' ', ' ', ' ', ' ' },
                    {' ', '/', '-', '\\', ' '},
                    {' ', '|', ' ', '|', ' '},
                    {' ', '\\', '_', '/', ' '},
                    {' ', ' ', ' ', ' ', ' '},
                };
            }

            if (width == 3)
            {
                return new char[,]
                {
                    {'/', '-', '\\'},
                    {'|', ' ', '|'},
                    {'\\', '_', '/'}
                };
            }

            return new char[,] { { 'O' } };
        }

        private static char[,] GetH(int width)
        {
            if (width == 5)
            {
                return new char[,]
                {
                    { ' ', ' ', ' ', ' ', ' ' },
                    {' ', '|', ' ', '|', ' '},
                    {' ', '|', '-', '|', ' '},
                    {' ', '|', ' ', '|', ' '},
                    {' ', ' ', ' ', ' ', ' '},
                };
            }

            if (width == 3)
            {
                return new char[,]
                {
                    {'|', ' ', '|'},
                    {'|', '-', '|'},
                    {'|', ' ', '|'}
                };
            }

            return new char[,] { { 'H' } };
        }

        private static char[,] GetT(int width)
        {
            if (width == 5)
            {
                return new char[,]
                {
                    { ' ', ' ', ' ', ' ', ' ' },
                    {' ', '-', '-', '-', ' '},
                    {' ', ' ', '|', ' ', ' '},
                    {' ', ' ', '|', ' ', ' '},
                    {' ', ' ', ' ', ' ', ' '},
                };
            }

            if (width == 3)
            {
                return new char[,]
                {
                    {'-', '-', '-'},
                    {' ', '|', ' '},
                    {' ', '|', ' '}
                };
            }

            return new char[,] { { 'T' } };
        }

        private static char[,] GetEmpty(int width)
        {
            var result = new char[width, width];
            for (var i = 0; i < width; i++)
            {
                for (var j = 0; j < width; j++)
                {
                    result[i, j] = ' ';
                }
            }

            return result;
        }
    }
}