namespace TicTacToeGame
{
    public class TicTacToeGameSample
    {
        public static void Play3x3With2Players()
        {
            PlayConsoleGame(3, new[] { 'X', 'O' });
        }

        public static void Play5x5With4Players()
        {
            PlayConsoleGame(5, new[] { 'X', 'O', 'H', 'T' });
        }

        private static void PlayConsoleGame(int boardSize, char[] players)
        {
            var game = new Game(boardSize, players);

            var consoleGame = new ConsoleGame(game);
            consoleGame.Play();
        }
    }
}
