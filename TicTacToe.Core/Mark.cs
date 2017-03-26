namespace TicTacToe.Core
{
    using System;
    using Headspring;

    public abstract class Mark : Enumeration<Mark>
    {
        public static Mark X = new XMark();
        public static Mark O = new OMark();
        public static Mark None = new NoMark();

        protected Mark(int value, string displayName) : base(value, displayName)
        {
        }

        public abstract Mark Toggle();

        private class XMark : Mark
        {
            public XMark() : base(1, "X")
            {                
            }

            public override Mark Toggle()
            {
                return Mark.O;
            }
        }

        private class OMark : Mark
        {
            public OMark() : base(-1, "O")
            {
            }

            public override Mark Toggle()
            {
                return Mark.X;
            }
        }

        private class NoMark : Mark
        {
            public NoMark() : base(0, " ")
            {
            }

            public override Mark Toggle()
            {
                throw new ApplicationException("Something has gone wrong with your game");
            }
        }
    }
}
