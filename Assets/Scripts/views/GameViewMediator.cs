using UnityEngine;
using System.Collections.Generic;
using strange.extensions.mediation.impl;
using net.peakgames.codebreaker.signals;
using net.peakgames.codebreaker.views;

namespace net.peakgames.codebreaker.views {
	
	public class GameViewMediator : Mediator {

		[Inject]
		public PlayerGuessSignal playerGuessSignal { get; set; }

		[Inject]
		public IGameView view {get; set;}

		private List<int> guesses = new List<int> ();

		public override void OnRegister() {
			this.view.Init (this);

		}

		public void OnPlayerMadeGuess(int inputIndex) {
			guesses.Add (inputIndex);
			if (guesses.Count == GameLogic.MAX_NUMBERS) {
				playerGuessSignal.Dispatch (guesses.ToArray());		
				guesses.Clear ();
			}
		}

		[ListensTo(typeof(GuessResultSignal))]
		public void OnGuessResult(int [] guess, Result result) {
			view.OnGuessResult (guess, result);
		}
	}

}
