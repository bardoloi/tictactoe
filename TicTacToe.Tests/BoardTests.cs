namespace TicTacToe.Tests
{
    using System;
    using Core;
    using Shouldly;

    public class BoardTests
    {
        public void new_board_should_be_empty()
        {
            var newBoard = new Board();

            for(var x = 0; x < newBoard.Side; x++)
                for(var y = 0; y < newBoard.Side; y++)
                    newBoard.IsCellEmpty(x, y).ShouldBe(true);
        }

        public void new_board_should_have_InProgress_status()
        {
            var newBoard = new Game().Board;
            newBoard.Status.ShouldBe(BoardStatus.InProgress);
        }

        public void should_reject_move_if_cell_already_occupied()
        {
            var board = new Board();

            int x = 0, y = 2;

            // add move to unoccupied cell: succeeds
            Should.NotThrow(() =>
            {
                board.AddMoveToCell(x, y, Player.One);
            });

            // add move to occupied cell: fails regardless of which player tries
            Should.Throw<ArgumentException>(() =>
            {
                board.AddMoveToCell(x, y, Player.Two);
            });
            Should.Throw<ArgumentException>(() =>
            {
                board.AddMoveToCell(x, y, Player.One);
            });
        }

        public void should_reject_move_if_it_lands_outside_board()
        {
            var board = new Board();

            Should.Throw<ArgumentException>(() =>
            {
                board.AddMoveToCell(-1, -1, Player.One);
            });
            Should.Throw<ArgumentException>(() =>
            {
                board.AddMoveToCell(-1, 0, Player.One);
            });
            Should.Throw<ArgumentException>(() =>
            {
                board.AddMoveToCell(0, -2, Player.One);
            });
            Should.Throw<ArgumentException>(() =>
            {
                board.AddMoveToCell(0, 3, Player.One);
            });
            Should.Throw<ArgumentException>(() =>
            {
                board.AddMoveToCell(3, 2, Player.One);
            });
            Should.Throw<ArgumentException>(() =>
            {
                board.AddMoveToCell(3, 5, Player.One);
            });
        }
    }
}
