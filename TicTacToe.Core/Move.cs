namespace TicTacToe.Core
{
    public class Move
    {
        public Move(int playerNumber, int x, int y)
        {
            PlayerNumber = playerNumber;
            X = x;
            Y = y;
        }

        public int PlayerNumber { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}