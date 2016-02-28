using System;
using System.Collections.Generic;
using net.peakgames.codebreaker.signals;

namespace net.peakgames.codebreaker {
	public class GameModel {
		private GameLogic gameLogic;
		private List<GuessResult> guessList;

		public void StartGame() {
			int[] solution = CreateRandomSolution ();
			this.gameLogic = new GameLogic (solution);
			this.guessList = new List<GuessResult> ();
		}

		public GuessResult MakeAGuess(int [] guess) {
			GuessResult result = 
				new GuessResult (
					guess, 
					this.gameLogic.Check (guess));
			this.guessList.Add( result );
			return result;
		}

		private int [] CreateRandomSolution() {
			Random rnd = new Random ();
			int [] solution = new int[GameLogic.MAX_NUMBERS];
			for (int i = 0; i < GameLogic.MAX_NUMBERS; i++) {
				solution[i] = rnd.Next(1, GameLogic.MAX_NUMBER_OF_POSSIBLE_VALUES + 1);
			}
			return solution;
		}
	}

	public class GuessResult {
		public int[] guess;
		public Result result;

		public GuessResult(int [] guess, Result result) {
			this.guess = guess;
			this.result = result;
		}
	}
}
