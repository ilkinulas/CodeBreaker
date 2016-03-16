using UnityEngine;
using UnityEngine.UI;
using strange.extensions.mediation.impl;

namespace net.peakgames.codebreaker.views {
	public class GameOverView : View , IGameOverView{

		[SerializeField]
		private Image[] solutionImages = new Image[4];

		[SerializeField]
		private Text newRecordText;

		private GameOverViewMediator mediator;

		public void Init(GameOverViewMediator  mediator) {
			this.mediator = mediator;
		}

		public void UpdateView(int [] solution, bool newRecord, int numTries) {
			if (newRecord) {
				newRecordText.gameObject.SetActive (true);
				newRecordText.text = string.Format ("New Record '{0}' tries.", numTries);
			}
		}

		public void OnNewGameButtonPressed() {
			mediator.StartNewGame ();
		}
	}
}
