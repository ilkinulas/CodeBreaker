using System;
using System.Collections.Generic;
using net.peakgames.codebreaker.signals;

namespace net.peakgames.codebreaker {
	public class GameModel {
		private GameLogic gameLogic;
		private List<GuessResult> guessList;

		public void StartGame() {			
			int[] solution = GameLogic.CreateRandomSolution ();
			this.gameLogic = new GameLogic (solution);
			this.guessList = new List<GuessResult> ();

			foreach (int s in solution) {
				UnityEngine.Debug.Log (s);
			}
		}

		public GuessResult MakeAGuess(int [] guess) {
			GuessResult result = 
				new GuessResult (
					guess, 
					this.gameLogic.Check (guess));
			this.guessList.Add( result );
			return result;
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
