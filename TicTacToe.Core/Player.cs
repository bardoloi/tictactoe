namespace TicTacToe.Core
{
    using Headspring;

    public class Player : Enumeration<Player>
    {
        private Player(int value, string displayName) : base(value, displayName)
        {
        }

        public Player Toggle()
        {
            if (this == Player2)
                return Player1;
            if (this == Player1)
                return Player2;
            return None;
        }

        public static Player Player1 = new Player(1, "Player 1");
        public static Player Player2 = new Player(-1, "Player 2");
        public static Player None = new Player(0, "Nobody");
    }
}
