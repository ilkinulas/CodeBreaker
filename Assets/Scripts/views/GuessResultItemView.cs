using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using net.peakgames.codebreaker.util;
using net.peakgames.codebreaker.signals;
using net.peakgames.codebreaker.audio;
using strange.extensions.mediation.impl;

namespace net.peakgames.codebreaker.views {
	
	public class GuessResultItemView : View {

		[SerializeField]
		private Image [] guesses = new Image[GameLogic.MAX_NUMBERS];

		[SerializeField]
		private Image[] results = new Image[GameLogic.MAX_NUMBERS];

		[SerializeField]
		private AnimationCurve resultAnimationCurve;

		[Inject]
		public PlaySoundSignal playSoundSignal { set; get;}

		protected override void Awake() {
			LayoutElement layoutElement = GetComponent<LayoutElement> ();
			layoutElement.preferredHeight = GameUtils.ToDP (layoutElement.preferredHeight);
			layoutElement.minHeight = GameUtils.ToDP (layoutElement.minHeight);
		}

		public void UpdateGuess(int [] guess, Result result, Button [] inputButtons) {
			for (int i = 0; i < guess.Length; i++) {
				Image image = guesses [i];
				image.sprite = inputButtons [guess[i]].image.sprite;
			}

			int whiteCount = result.whiteCount;
			int blackCount = result.blackCount;
			List<Image> activeImages = new List<Image> ();
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
				if (image.IsActive ()) { 
					activeImages.Add (image);
				}
			}
			StartCoroutine(ResultAnimation(activeImages));
		}

		private IEnumerator ResultAnimation(List<Image> images, float duration = 0.5f) {					
			if (images.Count == 0) {
				yield break;
			}
			foreach (var image in images) {
				image.rectTransform.localScale = Vector3.zero;
			}

			yield return null;

			foreach (var image in images) {				
				float elapsedTime = 0;
				playSoundSignal.Dispatch (GameSound.Result);
				while (elapsedTime < duration) {
					elapsedTime += Time.deltaTime;
					image.rectTransform.localScale = 
						Vector3.one * resultAnimationCurve.Evaluate (elapsedTime / duration);
					yield return null;
				}	
			}

		}
	}
}
