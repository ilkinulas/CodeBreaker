using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;
using net.peakgames.codebreaker.signals;

namespace net.peakgames.codebreaker.views {
	public class GameOverViewMediator : Mediator {
		
		[Inject]
		public IGameOverView view { get; set; }

		[Inject]
		public StartNewGameSignal newGameSignal { get; set; }

		public override void OnRegister() {
			this.view.Init (this);
		}

		[ListensTo(typeof(GameOverSignal))]
		public void OnGameOver(int [] solution, bool isNewRecord, int numTries) {
			this.view.UpdateView (solution, isNewRecord, numTries);
		}

		public void StartNewGame() {
			newGameSignal.Dispatch ();
		}
	}
}
