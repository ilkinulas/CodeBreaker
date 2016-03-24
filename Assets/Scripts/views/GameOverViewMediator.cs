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

		[Inject]
		public StatsModel statsModel { get; set; }

		[Inject]
		public GameModel gameModel { get; set; }

		public override void OnRegister() {
			this.view.Init (this);
		}
			
		public void StartNewGame() {
			newGameSignal.Dispatch ();
		}

		[ListensTo(typeof(GameOverSignal))]
		public void OnGameOver(int [] solution, bool isNewRecord) {
			this.view.UpdateView (
				solution, 
				isNewRecord, 
				gameModel.NumberOfGuesses, 
				statsModel.BestScore, 
				statsModel.NumberOfGamesPlayed);
		}
	}
}
