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

        public void should_put_correct_mark_in_cell_for_each_move()
        {
            var board = new Board();

            int x = 0, y = 2;
            board.AddMark(x, y, Mark.X);
            board.MarkInCell(x, y).ShouldBe(Mark.X);

            x = 1;
            y = 0;
            board.AddMark(x, y, Mark.O);
            board.MarkInCell(x, y).ShouldBe(Mark.O);

            x = 1;
            y = 1;
            board.AddMark(x, y, Mark.X);
            board.MarkInCell(x, y).ShouldBe(Mark.X);

            x = 2;
            y = 2;
            board.AddMark(x, y, Mark.O);
            board.MarkInCell(x, y).ShouldBe(Mark.O);
        }

        public void should_throw_exception_if_2_consecutive_moves_use_same_mark()
        {
            var board = new Board();

            board.AddMark(0, 2, Mark.X);

            board.AddMark(1, 1, Mark.O);

            // move cannot use the same mark as previous move
            Should.Throw<ArgumentException>(() => {
               board.AddMark(2, 2, Mark.O);
            });

            board.AddMark(2, 2, Mark.X);

            // move cannot use the same mark as previous move
            Should.Throw<ArgumentException>(() => {
                board.AddMark(2, 0, Mark.X);
            });

            board.AddMark(2, 0, Mark.O);
        }

        public void should_update_status_and_winner_when_player_1_wins()
        {
            var board = new Board();

            board.AddMark(0, 0, Mark.X); // p1
            board.Status.ShouldBe(BoardStatus.InProgress);
            board.Winner.ShouldBe(Mark.None);

            board.AddMark(1, 0, Mark.O);
            board.Status.ShouldBe(BoardStatus.InProgress);
            board.Winner.ShouldBe(Mark.None);

            board.AddMark(0, 1, Mark.X); // p1
            board.Status.ShouldBe(BoardStatus.InProgress);
            board.Winner.ShouldBe(Mark.None);

            board.AddMark(1, 1, Mark.O);
            board.Status.ShouldBe(BoardStatus.InProgress);
            board.Winner.ShouldBe(Mark.None);

            board.AddMark(0, 2, Mark.X); // p1 <--- just won
            board.Status.ShouldBe(BoardStatus.Won);
            board.Winner.ShouldBe(Mark.X);
        }

        public void should_update_status_and_winner_when_player_2_wins()
        {
            var board = new Board();

            board.AddMark(0, 0, Mark.O);
            board.Status.ShouldBe(BoardStatus.InProgress);
            board.Winner.ShouldBe(Mark.None);

            board.AddMark(1, 0, Mark.X); // p2
            board.Status.ShouldBe(BoardStatus.InProgress);
            board.Winner.ShouldBe(Mark.None);

            board.AddMark(0, 1, Mark.O);
            board.Status.ShouldBe(BoardStatus.InProgress);
            board.Winner.ShouldBe(Mark.None);

            board.AddMark(1, 1, Mark.X); // p2
            board.Status.ShouldBe(BoardStatus.InProgress);
            board.Winner.ShouldBe(Mark.None);

            board.AddMark(2, 2, Mark.O);
            board.Status.ShouldBe(BoardStatus.InProgress);
            board.Winner.ShouldBe(Mark.None);

            board.AddMark(1, 2, Mark.X); // p2 <----- just won
            board.Status.ShouldBe(BoardStatus.Won);
            board.Winner.ShouldBe(Mark.X);
        }

        public void should_update_status_and_winner_when_board_is_drawn()
        {
            var board = new Board();

            board.AddMark(0, 0, Mark.X); // p1
            board.AddMark(0, 1, Mark.O);
            board.AddMark(0, 2, Mark.X); // p1            
            board.AddMark(1, 1, Mark.O);
            board.AddMark(2, 1, Mark.X); // p1
            board.AddMark(1, 0, Mark.O);
            board.AddMark(1, 2, Mark.X); // p1
            board.AddMark(2, 2, Mark.O); // tie game

            board.Status.ShouldBe(BoardStatus.Drawn);
            board.Winner.ShouldBe(Mark.None);
        }

        public void should_throw_exception_when_adding_move_if_board_is_already_drawn()
        {
            var board = new Board();

            board.AddMark(0, 0, Mark.O); // p1
            board.AddMark(0, 1, Mark.X);
            board.AddMark(0, 2, Mark.O); // p1            
            board.AddMark(1, 1, Mark.X);
            board.AddMark(2, 1, Mark.O); // p1
            board.AddMark(1, 0, Mark.X);
            board.AddMark(1, 2, Mark.O); // p1
            board.AddMark(2, 2, Mark.X); // tie game

            Should.Throw<ApplicationException>(() =>
            {
                board.AddMark(2, 0, Mark.O);
            });
        }

        public void should_reject_move_if_cell_already_occupied()
        {
            var board = new Board();

            int x = 0, y = 2;

            // add move to unoccupied cell: succeeds
            Should.NotThrow(() =>
            {
                board.AddMark(x, y, Mark.X);
            });

            // add move to occupied cell: fails regardless of what mark is made
            Should.Throw<ArgumentException>(() =>
            {
                board.AddMark(x, y, Mark.X);
            });
            Should.Throw<ArgumentException>(() =>
            {
                board.AddMark(x, y, Mark.O);
            });
        }

        public void should_reject_move_if_it_lands_outside_board()
        {
            var board = new Board();

            Should.Throw<ArgumentException>(() =>
            {
                board.AddMark(-1, -1, Mark.X);
            });
            Should.Throw<ArgumentException>(() =>
            {
                board.AddMark(-1, 0, Mark.X);
            });
            Should.Throw<ArgumentException>(() =>
            {
                board.AddMark(0, -2, Mark.X);
            });
            Should.Throw<ArgumentException>(() =>
            {
                board.AddMark(0, 3, Mark.X);
            });
            Should.Throw<ArgumentException>(() =>
            {
                board.AddMark(3, 2, Mark.X);
            });
            Should.Throw<ArgumentException>(() =>
            {
                board.AddMark(3, 5, Mark.X);
            });
        }

        public void should_detect_antidiagonal_win()
        {
            var board = new Board();
            board.AddMark(1, 1, Mark.O); // p1
            board.AddMark(0, 0, Mark.X);
            board.AddMark(2, 2, Mark.O); // p1
            board.AddMark(0, 1, Mark.X);
            board.AddMark(0, 2, Mark.O); // p1
            board.AddMark(1, 2, Mark.X);
            board.AddMark(2, 0, Mark.O); // p1 <-- win

            board.Status.ShouldBe(BoardStatus.Won);
            board.Winner.ShouldBe(Mark.O);
        }

        public void should_detect_draw_scenario_by_looking_ahead()
        {
            var board = new Board();

            board.AddMark(0, 0, Mark.X); // p1
            board.AddMark(1, 1, Mark.O);
            board.AddMark(0, 1, Mark.X); // p1            
            board.AddMark(0, 2, Mark.O);
            board.AddMark(2, 0, Mark.X); // p1
            board.AddMark(1, 0, Mark.O);
            board.AddMark(1, 2, Mark.X); // p1 <--- tie game should be detected here

            board.Status.ShouldBe(BoardStatus.Drawn);
            board.Winner.ShouldBe(Mark.None);
        }
    }
}
