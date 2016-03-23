using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using NSubstitute;
using net.peakgames.codebreaker.signals;
using net.peakgames.codebreaker.views;

namespace net.peakgames.codebreaker.commands {
	public class PlayerGuessCommandTest {

		private PlayerGuessCommand command;
		private int[] solution = { 3, 5, 7, 1 };
		private bool guessResultSignalDispatched;
		private bool gameOverSignalDispatched;

		[SetUp]
		public void SetUp() {
			command = new PlayerGuessCommand();
			command.gameModel = new GameModel ();
			command.guessResultSignal = new GuessResultSignal ();
			command.guessResultSignal.AddOnce (GuessResultSignalCallback);
			command.viewSwitcher = Substitute.For<IViewSwitcher> ();
			command.gameOverSignal = new GameOverSignal ();
			command.gameOverSignal.AddOnce (GameOverSignalCallback);
			command.statsModel = new StatsModel ();
			command.playSoundSignal = new PlaySoundSignal ();
		
			command.gameModel.StartGame (solution);

			guessResultSignalDispatched = false;
			gameOverSignalDispatched = false;
		}
			
		[Test]
		public void IncorrectGuest() {
			int numberOfGamesPlayed = command.statsModel.NumberOfGamesPlayed;
			command.guess = new int[] { 1, 2, 3, 4 };
			command.Execute ();

			Assert.AreEqual (numberOfGamesPlayed, command.statsModel.NumberOfGamesPlayed);
			Assert.IsTrue (guessResultSignalDispatched);
			Assert.IsFalse (gameOverSignalDispatched);
			command.viewSwitcher.DidNotReceiveWithAnyArgs ()
				.SwitchWithAnimationTo (Arg.Any<ViewType>());				
		}

		[Test]
		public void CorrectGuess() {
			int numberOfGamesPlayed = command.statsModel.NumberOfGamesPlayed;
			command.guess = solution;
			command.Execute ();

			Assert.AreEqual (numberOfGamesPlayed + 1, command.statsModel.NumberOfGamesPlayed);
			Assert.IsTrue (guessResultSignalDispatched);
			Assert.IsTrue (gameOverSignalDispatched);

			command.viewSwitcher.Received (1)
				.SwitchWithAnimationTo (ViewType.GameOver);
		}

		private void GuessResultSignalCallback(int [] guess, Result result) {
			guessResultSignalDispatched = true;
		}

		private void GameOverSignalCallback(int [] guess, bool bestScore) {
			gameOverSignalDispatched = true;
		}
	}

}
