using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using net.peakgames.codebreaker.util;

namespace net.peakgames.codebreaker.views {
	
	public class GuessResultItemView : MonoBehaviour {

		[SerializeField]
		private Image [] guesses = new Image[GameLogic.MAX_NUMBERS];

		[SerializeField]
		private Image[] results = new Image[GameLogic.MAX_NUMBERS];

		void Start() {
			LayoutElement layoutElement = GetComponent<LayoutElement> ();
			layoutElement.preferredHeight = GameUtils.ToDP (layoutElement.preferredHeight);
			layoutElement.minHeight = GameUtils.ToDP (layoutElement.minHeight);
		}

		public void UpdateGuess(int [] guess, Result result, Color [] colors) {
			for (int i = 0; i < guess.Length; i++) {
				Image image = guesses [i];
				image.color = colors [guess[i]];
			}

			int whiteCount = result.whiteCount;
			int blackCount = result.blackCount;
			foreach (Image image in results) {
				if (whiteCount > 0) {
					image.gameObject.SetActive (true);
					image.color = Color.white;
					whiteCount--;
				} else if (blackCount > 0) {
					image.gameObject.SetActive (true);
					image.color = Color.black;
					blackCount--;
				}
			}
		}
	}
}
