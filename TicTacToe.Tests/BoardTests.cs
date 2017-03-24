namespace TicTacToe.Tests
{
    using System;
    using Core;
    using Shouldly;

    public class BoardTests
    {
        public void should_begin_game_with_empty_board()
        {
            var game = new Game();

            for(var x = 0; x < game.Board.Side; x++)
                for(var y = 0; y < game.Board.Side; y++)
                    game.Board.IsCellEmpty(x, y).ShouldBe(true);
        }

        public void should_reject_move_if_cell_already_occupied()
        {
            var game = new Game();

            int x = 0, y = 2;

            // add move to unoccupied cell: succeeds
            Should.NotThrow(() =>
            {
                game.AddMove(x, y);
            });

            // try to add move to an occupied cell: fails
            Should.Throw<ArgumentException>(() =>
            {
                game.AddMove(x, y);
            });
        }

        public void should_reject_move_if_it_lands_outside_board()
        {
            var game = new Game();

            Should.Throw<ArgumentException>(() =>
            {
                game.AddMove(-1, 0);
            });
            Should.Throw<ArgumentException>(() =>
            {
                game.AddMove(0, -2);
            });
            Should.Throw<ArgumentException>(() =>
            {
                game.AddMove(0, 3);
            });
            Should.Throw<ArgumentException>(() =>
            {
                game.AddMove(3, 2);
            });
            Should.Throw<ArgumentException>(() =>
            {
                game.AddMove(3,5);
            });
        }
    }
}
