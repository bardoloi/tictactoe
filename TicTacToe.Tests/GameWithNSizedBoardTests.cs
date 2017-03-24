namespace TicTacToe.Tests
{
    using System;
    using Core;
    using Shouldly;

    public class GameWithNSizedBoardTests
    {
        public void should_throw_if_board_side_under_3()
        {
            Should.Throw<ArgumentException>(() =>
            {
                var game = new Game(2);
            });
        }

        public void should_start_new_game_with_status_in_progress()
        {
            var game = new Game(6);
            game.Status.ShouldBe(GameStatus.InProgress);
        }

        public void should_put_correct_player_on_board_for_each_move()
        {
            var game = new Game(5);

            int x = 0, y = 4;
            game.AddMove(x, y);
            game.Board.PlayerInCell(x, y).ShouldBe(Player.One);

            x = 4;
            y = 0;
            game.AddMove(x, y);
            game.Board.PlayerInCell(x, y).ShouldBe(Player.Two);

            x = 4;
            y = 4;
            game.AddMove(x, y);
            game.Board.PlayerInCell(x, y).ShouldBe(Player.One);
        }

        public void should_update_status_and_winner_when_game_is_won_by_player_1()
        {
            var game = new Game(4);

            game.AddMove(0, 0); // p1
            game.Status.ShouldBe(GameStatus.InProgress);
            game.Winner.ShouldBe(Player.None);

            game.AddMove(1, 0);
            game.Status.ShouldBe(GameStatus.InProgress);
            game.Winner.ShouldBe(Player.None);

            game.AddMove(1, 1); // p1
            game.Status.ShouldBe(GameStatus.InProgress);
            game.Winner.ShouldBe(Player.None);

            game.AddMove(2, 0);
            game.Status.ShouldBe(GameStatus.InProgress);
            game.Winner.ShouldBe(Player.None);

            game.AddMove(2, 2); // p1
            game.Status.ShouldBe(GameStatus.InProgress);
            game.Winner.ShouldBe(Player.None);

            game.AddMove(3, 0);
            game.Status.ShouldBe(GameStatus.InProgress);
            game.Winner.ShouldBe(Player.None);

            game.AddMove(3, 3); // p1 just won
            game.Status.ShouldBe(GameStatus.Completed);
            game.Winner.ShouldBe(Player.One);
        }

        public void should_update_status_and_winner_when_game_is_won_by_player_2()
        {
            var game = new Game(4);

            game.AddMove(0, 0);
            game.Status.ShouldBe(GameStatus.InProgress);
            game.Winner.ShouldBe(Player.None);

            game.AddMove(3, 0); // p2
            game.Status.ShouldBe(GameStatus.InProgress);
            game.Winner.ShouldBe(Player.None);

            game.AddMove(1, 1);
            game.Status.ShouldBe(GameStatus.InProgress);
            game.Winner.ShouldBe(Player.None);

            game.AddMove(3, 1); // p2
            game.Status.ShouldBe(GameStatus.InProgress);
            game.Winner.ShouldBe(Player.None);

            game.AddMove(2, 2);
            game.Status.ShouldBe(GameStatus.InProgress);
            game.Winner.ShouldBe(Player.None);

            game.AddMove(3, 2); // p2
            game.Status.ShouldBe(GameStatus.InProgress);
            game.Winner.ShouldBe(Player.None);

            game.AddMove(2, 3);
            game.Status.ShouldBe(GameStatus.InProgress);
            game.Winner.ShouldBe(Player.None);

            game.AddMove(3, 3); // p2 just won
            game.Status.ShouldBe(GameStatus.Completed);
            game.Winner.ShouldBe(Player.Two);
        }

        public void should_detect_when_game_is_tied()
        {
            var game = PlayTiedGame();
            game.Status.ShouldBe(GameStatus.Completed);
            game.Winner.ShouldBe(Player.None);
        }

        public void should_throw_when_adding_move_if_game_is_already_over()
        {
            var game = PlayTiedGame();
            Should.Throw<ApplicationException>(() =>
            {
                game.AddMove(2, 1);
            });
        }

        private static Game PlayTiedGame()
        {
            var game = new Game(5);

            game.AddMove(0, 0); // p1
            game.AddMove(0, 1);

            game.AddMove(1, 1); // p1            
            game.AddMove(2, 2);

            game.AddMove(3, 3); // p1
            game.AddMove(4, 4);

            game.AddMove(0, 2); // p1
            game.AddMove(1, 2);

            game.AddMove(3, 2); // p1
            game.AddMove(3, 1);

            game.AddMove(4, 0); // p1
            game.AddMove(3, 0);

            game.AddMove(4, 3); // p1
            game.AddMove(2, 3);

            game.AddMove(2, 4); // p1

            // game is tied at this point; no result is possible even with additional moves
            return game;
        }
    }
}
