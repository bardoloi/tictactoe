namespace TicTacToe.Tests
{
    using System;
    using Core;
    using Shouldly;

    public class BoardTests
    {
        public void should_initiate_board_of_side_3_by_default()
        {
            var board = new Board();
            board.Side.ShouldBe(3);
        }

        public void should_initiate_board_of_correct_size()
        {
            var board = new Board(5);
            board.Side.ShouldBe(5);
        }

        public void new_board_should_be_empty()
        {
            var board = new Board();

            for(var x = 0; x < board.Side; x++)
                for(var y = 0; y < board.Side; y++)
                    board.IsCellEmpty(x, y).ShouldBe(true);
        }

        public void should_reject_move_if_cell_already_occupied()
        {
            var board = new Board();

            int x = 0, y = 2;

            // add move to unoccupied cell: succeeds
            Should.NotThrow(() =>
            {
                board.AddMoveToCell(x, y);
            });

            // add move to occupied cell: fails
            Should.Throw<ArgumentException>(() =>
            {
                board.AddMoveToCell(x, y);
            });
        }

        public void should_reject_move_if_it_lands_outside_board()
        {
            var board = new Board();

            Should.Throw<ArgumentException>(() =>
            {
                board.AddMoveToCell(-1, -1);
            });
            Should.Throw<ArgumentException>(() =>
            {
                board.AddMoveToCell(-1, 0);
            });
            Should.Throw<ArgumentException>(() =>
            {
                board.AddMoveToCell(0, -2);
            });
            Should.Throw<ArgumentException>(() =>
            {
                board.AddMoveToCell(0, 3);
            });
            Should.Throw<ArgumentException>(() =>
            {
                board.AddMoveToCell(3, 2);
            });
            Should.Throw<ArgumentException>(() =>
            {
                board.AddMoveToCell(3, 5);
            });
        }
    }
}
