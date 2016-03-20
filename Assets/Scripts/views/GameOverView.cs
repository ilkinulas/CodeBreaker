using UnityEngine;
using UnityEngine.UI;
using strange.extensions.mediation.impl;
using net.peakgames.codebreaker.util;

namespace net.peakgames.codebreaker.views {
	public class GameOverView : View , IGameOverView{

		[SerializeField]
		private Image[] solutionImages = new Image[4];

		[SerializeField]
		private Text newRecordText;

		[SerializeField]
		private Text totalGamesPlayedText;

		[SerializeField]
		private Text bestScoreText;

		private GameOverViewMediator mediator;

		public void Init(GameOverViewMediator  mediator) {
			this.mediator = mediator;
		}

		public void UpdateView(int [] solution, bool newRecord, int numTries, int bestScore, int totalGames) {
			if (newRecord) {
				newRecordText.gameObject.SetActive (true);
				newRecordText.text = string.Format ("New Record '{0}' tries.", numTries);
			}
			bestScoreText.text = string.Format ("Best Score : {0}", bestScore);
			totalGamesPlayedText.text = string.Format ("Total Games Played : {0}", totalGames);

			int counter = 0;
			foreach (var index in solution) {
				solutionImages [counter++].sprite = GameUtils.GetAnimalSprite (index);
			}
		}

		public void OnNewGameButtonPressed() {
			mediator.StartNewGame ();
		}
	}
}
