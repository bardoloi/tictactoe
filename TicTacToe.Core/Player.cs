namespace TicTacToe.Core
{
    using System;
    using Headspring;

    public abstract class Player : Enumeration<Player>
    {
        private Player(int value, string displayName) : base(value, displayName)
        {
        }

        public abstract Player Toggle();

        private class Player1 : Player
        {
            public Player1() : base(1, "Player 1")
            {                
            }

            public override Player Toggle()
            {
                return Player.Two;
            }
        }

        private class Player2 : Player
        {
            public Player2() : base(-1, "Player 2")
            {
            }

            public override Player Toggle()
            {
                return Player.One;
            }
        }

        private class NoPlayer : Player
        {
            public NoPlayer() : base(0, "Nobody")
            {
            }

            public override Player Toggle()
            {
                throw new ApplicationException("Something has gone wrong with your game");
            }
        }

        public static Player One = new Player1();
        public static Player Two = new Player2();
        public static Player None = new NoPlayer();
    }
}
