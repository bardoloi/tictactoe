namespace TicTacToe.Tests
{
    using System;
    using Core;
    using Shouldly;

    public class BoardTests
    {
        public void should_create_board_of_side_3_by_default()
        {
            var board = new Board();
            board.Side.ShouldBe(3);
        }

        public void should_create_board_of_correct_size()
        {
            var board = new Board(5);
            board.Side.ShouldBe(5);
        }

        public void new_board_should_be_empty()
        {
            var board = new Board();

            for (var x = 0; x < board.Side; x++)
                for (var y = 0; y < board.Side; y++)
                    board.IsCellEmpty(x, y).ShouldBe(true);
        }

        public void should_put_correct_player_on_cell_for_each_move()
        {
            var board = new Board();

            int x = 0, y = 2;
            board.AddMove(x, y);
            board.PlayerInCell(x, y).ShouldBe(Player.One);

            x = 1;
            y = 0;
            board.AddMove(x, y);
            board.PlayerInCell(x, y).ShouldBe(Player.Two);

            x = 1;
            y = 1;
            board.AddMove(x, y);
            board.PlayerInCell(x, y).ShouldBe(Player.One);

            x = 2;
            y = 2;
            board.AddMove(x, y);
            board.PlayerInCell(x, y).ShouldBe(Player.Two);
        }

        public void should_update_status_and_winner_when_player_1_wins()
        {
            var board = new Board();

            board.AddMove(0, 0); // p1
            board.Status.ShouldBe(BoardStatus.InProgress);
            board.Winner.ShouldBe(Player.None);

            board.AddMove(1, 0);
            board.Status.ShouldBe(BoardStatus.InProgress);
            board.Winner.ShouldBe(Player.None);

            board.AddMove(0, 1); // p1
            board.Status.ShouldBe(BoardStatus.InProgress);
            board.Winner.ShouldBe(Player.None);

            board.AddMove(1, 1);
            board.Status.ShouldBe(BoardStatus.InProgress);
            board.Winner.ShouldBe(Player.None);

            board.AddMove(0, 2); // p1 <--- just won
            board.Status.ShouldBe(BoardStatus.Won);
            board.Winner.ShouldBe(Player.One);
        }

        public void should_update_status_and_winner_when_player_2_wins()
        {
                var board = new Board();

                board.AddMove(0, 0);
                board.Status.ShouldBe(BoardStatus.InProgress);
                board.Winner.ShouldBe(Player.None);

                board.AddMove(1, 0); // p2
                board.Status.ShouldBe(BoardStatus.InProgress);
                board.Winner.ShouldBe(Player.None);

                board.AddMove(0, 1);
                board.Status.ShouldBe(BoardStatus.InProgress);
                board.Winner.ShouldBe(Player.None);

                board.AddMove(1, 1); // p2
                board.Status.ShouldBe(BoardStatus.InProgress);
                board.Winner.ShouldBe(Player.None);

                board.AddMove(2, 2);
                board.Status.ShouldBe(BoardStatus.InProgress);
                board.Winner.ShouldBe(Player.None);

                board.AddMove(1, 2); // p2 <----- just won
                board.Status.ShouldBe(BoardStatus.Won);
            board.Winner.ShouldBe(Player.Two);
        }

        public void should_update_status_and_winner_when_board_is_drawn()
        {
            var board = new Board();

            board.AddMove(0, 0); // p1
            board.AddMove(0, 1);
            board.AddMove(0, 2); // p1            
            board.AddMove(1, 1);
            board.AddMove(2, 1); // p1
            board.AddMove(1, 0);
            board.AddMove(1, 2); // p1
            board.AddMove(2, 2); // tie game

            board.Status.ShouldBe(BoardStatus.Drawn);
            board.Winner.ShouldBe(Player.None);
        }

        public void should_throw_exception_when_adding_move_if_board_is_already_drawn()
        {
            var board = new Board();

            board.AddMove(0, 0); // p1
            board.AddMove(0, 1);
            board.AddMove(0, 2); // p1            
            board.AddMove(1, 1);
            board.AddMove(2, 1); // p1
            board.AddMove(1, 0);
            board.AddMove(1, 2); // p1
            board.AddMove(2, 2); // tie game

            Should.Throw<ApplicationException>(() =>
            {
                board.AddMove(2, 0);
            });
        }

        public void should_reject_move_if_cell_already_occupied()
        {
            var board = new Board();

            int x = 0, y = 2;

            // add move to unoccupied cell: succeeds
            Should.NotThrow(() =>
            {
                board.AddMove(x, y);
            });

            // add move to occupied cell: fails
            Should.Throw<ArgumentException>(() =>
            {
                board.AddMove(x, y);
            });
        }

        public void should_reject_move_if_it_lands_outside_board()
        {
            var board = new Board();

            Should.Throw<ArgumentException>(() =>
            {
                board.AddMove(-1, -1);
            });
            Should.Throw<ArgumentException>(() =>
            {
                board.AddMove(-1, 0);
            });
            Should.Throw<ArgumentException>(() =>
            {
                board.AddMove(0, -2);
            });
            Should.Throw<ArgumentException>(() =>
            {
                board.AddMove(0, 3);
            });
            Should.Throw<ArgumentException>(() =>
            {
                board.AddMove(3, 2);
            });
            Should.Throw<ArgumentException>(() =>
            {
                board.AddMove(3, 5);
            });
        }

        public void should_detect_antidiagonal_win()
        {
            var board = new Board();
            board.AddMove(1, 1); // p1
            board.AddMove(0, 0);
            board.AddMove(2, 2); // p1
            board.AddMove(0, 1);
            board.AddMove(0, 2); // p1
            board.AddMove(1, 2);
            board.AddMove(2, 0); // p1 <-- win

            board.Status.ShouldBe(BoardStatus.Won);
            board.Winner.ShouldBe(Player.One);
        }

        public void should_detect_draw_scenario_by_looking_ahead()
        {
            var board = new Board();

            board.AddMove(0, 0); // p1
            board.AddMove(1, 1);
            board.AddMove(0, 1); // p1            
            board.AddMove(0, 2);
            board.AddMove(2, 0); // p1
            board.AddMove(1, 0);
            board.AddMove(1, 2); // p1 <--- tie game should be detected here

            board.Status.ShouldBe(BoardStatus.Drawn);
            board.Winner.ShouldBe(Player.None);
        }
    }
}
