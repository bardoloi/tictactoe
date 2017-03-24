namespace TicTacToe.Tests
{
    using System;
    using Core;
    using Shouldly;

    public class GameTests
    {
        public void should_start_new_game_with_status_in_progress()
        {
            var game = new Game();
            game.Status.ShouldBe(GameStatus.InProgress);
        }

        public void should_put_correct_player_on_board_for_each_move()
        {
            var game = new Game();

            int x = 0, y = 2;
            game.AddMove(x, y);
            game.Board.PlayerInCell(x, y).ShouldBe(Player.One);

            x = 1;
            y = 0;
            game.AddMove(x, y);
            game.Board.PlayerInCell(x, y).ShouldBe(Player.Two);

            x = 1;
            y = 1;
            game.AddMove(x, y);
            game.Board.PlayerInCell(x, y).ShouldBe(Player.One);
        }

        public void should_update_status_and_winner_when_game_is_won_by_player_1()
        {
            var game = new Game();

            game.AddMove(0, 0); // p1
            game.Status.ShouldBe(GameStatus.InProgress);
            game.Winner.ShouldBe(Player.None);

            game.AddMove(1, 0);
            game.Status.ShouldBe(GameStatus.InProgress);
            game.Winner.ShouldBe(Player.None);

            game.AddMove(0, 1); // p1
            game.Status.ShouldBe(GameStatus.InProgress);
            game.Winner.ShouldBe(Player.None);

            game.AddMove(1, 1);
            game.Status.ShouldBe(GameStatus.InProgress);
            game.Winner.ShouldBe(Player.None);

            game.AddMove(0, 2); // p1 <--- win
            game.Status.ShouldBe(GameStatus.Completed);
            game.Winner.ShouldBe(Player.One);            
        }

        public void should_update_status_and_winner_when_game_is_won_by_player_2()
        {
            var game = new Game();

            game.AddMove(0, 0);
            game.Status.ShouldBe(GameStatus.InProgress);
            game.Winner.ShouldBe(Player.None);
            game.AddMove(1, 0); // p2
            game.Status.ShouldBe(GameStatus.InProgress);
            game.Winner.ShouldBe(Player.None);
            game.AddMove(0, 1);
            game.Status.ShouldBe(GameStatus.InProgress);
            game.Winner.ShouldBe(Player.None);
            game.AddMove(1, 1); // p2
            game.Status.ShouldBe(GameStatus.InProgress);
            game.Winner.ShouldBe(Player.None);
            game.AddMove(2, 2);
            game.Status.ShouldBe(GameStatus.InProgress);
            game.Winner.ShouldBe(Player.None);
            game.AddMove(1, 2); // p2 <----- just won
            game.Status.ShouldBe(GameStatus.Completed);
            game.Winner.ShouldBe(Player.Two);
        }

        public void should_detect_antidiagonal_win()
        {
            var game = new Game();
            game.AddMove(1, 1); // p1
            game.AddMove(0, 0);
            game.AddMove(2, 2); // p1
            game.AddMove(0, 1);
            game.AddMove(0, 2); // p1
            game.AddMove(1, 2);
            game.AddMove(2, 0); // p1 <-- win

            game.Status.ShouldBe(GameStatus.Completed);
            game.Winner.ShouldBe(Player.One);
        }

        public void should_update_status_and_winner_when_game_is_tied()
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
                game.AddMove(2, 0);
            });
        }

        private static Game PlayTiedGame()
        {
            var game = new Game();

            game.AddMove(0, 0); // p1
            game.AddMove(0, 1);
            game.AddMove(0, 2); // p1            
            game.AddMove(1, 1);
            game.AddMove(2, 1); // p1
            game.AddMove(1, 0);
            game.AddMove(1, 2); // p1
            game.AddMove(2, 2); // tie game

            return game;
        }
    }
}