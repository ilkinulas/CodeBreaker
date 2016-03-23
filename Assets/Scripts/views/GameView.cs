using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using strange.extensions.mediation.impl;
using net.peakgames.codebreaker.util;

namespace net.peakgames.codebreaker.views
{
	public class GameView : View, IGameView
	{
		[SerializeField]
		private Image[] headerImages = new Image[GameLogic.MAX_NUMBERS];

		[SerializeField]
		private Button[] inputButtons = new Button[GameLogic.MAX_NUMBER_OF_POSSIBLE_VALUES];

		[SerializeField]
		private GameObject guessResultPrefab;

		[SerializeField]
		private GameObject scrollContent;

		[SerializeField]
		private ScrollRect scrollRect;

		[SerializeField]
		private Sprite placeHolder;

		[SerializeField]
		private Button submitButton;

		[SerializeField]
		private AnimationCurve headerExitAnimationCurve;

		[SerializeField]
		private AnimationCurve headerExitItemRotationAnimationCurve;

		private GameViewMediator mediator;
		private int headerImageIndex = 0;
		private List<int> guessList = new List<int> ();

		protected override void OnEnable() {
			ResetScrollContent ();
			for  (int i=0; i<inputButtons.Length; i++) {
				inputButtons [i].image.sprite = GameUtils.GetAnimalSprite (i);
			}
			submitButton.gameObject.SetActive (false);
		}

		private void ResetScrollContent() {
			int numChildren = scrollContent.transform.childCount;
			for (int i = 0; i < numChildren; i++) {
				Destroy (scrollContent.transform.GetChild (i).gameObject);
			}
		}

		private void UpdateButtonColor (Button button, Color color) {
			ColorBlock colorBlock = button.colors;
			colorBlock.normalColor = color;
			colorBlock.highlightedColor = color;
			colorBlock.pressedColor = color * 0.8f;
			button.colors = colorBlock;
		}

		public void Init (GameViewMediator mediator) {			
			this.mediator = mediator;
		}

		public void OnInputButtonPressed (int buttonIndex) {		
			inputButtons [buttonIndex].interactable = false;
			Image image = headerImages [headerImageIndex];
			image.sprite = inputButtons [buttonIndex].image.sprite;
			headerImageIndex = (headerImageIndex + 1);
			guessList.Add (buttonIndex);
			if (headerImageIndex == GameLogic.MAX_NUMBERS) {
				submitButton.gameObject.SetActive (true);
				DisableAllInputButtons ();
				headerImageIndex = 0;
			}
		}

		public void OnSubmitButtonPressed() {
			this.mediator.OnPlayerMadeGuess (guessList.ToArray ());
			guessList.Clear ();
		}

		public void OnGuessResult (int[] guess, Result result) {			
			GameObject resultItem = Instantiate (this.guessResultPrefab);
			resultItem.transform.SetParent (scrollContent.transform);

			GuessResultItemView itemView = resultItem.GetComponent<GuessResultItemView> ();
			itemView.UpdateGuess (guess, result, inputButtons);

			ResetHeader ();
			EnableAllInputButtons ();

			scrollRect.GetComponent<ScrollViewController> ().UpdateScrollPosition ();
			submitButton.gameObject.SetActive (false);
		}

		private void EnableAllInputButtons() {
			foreach (Button button in inputButtons) {
				button.interactable = true;
			}
		}

		private void DisableAllInputButtons() {
			foreach (Button button in inputButtons) {
				button.interactable = false;
			}
		}

		private void ResetHeader () {
			StartCoroutine (StartExitAnimation ());
		}

		private IEnumerator StartExitAnimation(float duration=1f) {
			yield return SetHeaderLayoutEnabled (false);
			Vector2[] initialPositions = new Vector2[headerImages.Length];
			for (int i = 0; i < headerImages.Length; i++) {
				initialPositions[i]=headerImages[i].rectTransform.anchoredPosition;
			}
			float elapsedTime = 0f;
			while (elapsedTime < duration) {	
				float ratio = elapsedTime / duration;
				float xDiff = Screen.width * headerExitAnimationCurve.Evaluate (ratio);	
				float rotation = Mathf.PI*2 * headerExitItemRotationAnimationCurve.Evaluate (ratio);
				for (int i = 0; i < headerImages.Length; i++) {
					headerImages[i].rectTransform.anchoredPosition = 
						new Vector2 (initialPositions[i].x + xDiff, initialPositions[i].y);
					headerImages [i].rectTransform.localEulerAngles = new Vector3(0, 0, rotation);
				}
				elapsedTime += Time.deltaTime;
				yield return null;
			}
			for (int i = 0; i < headerImages.Length; i++) {
				headerImages[i].rectTransform.anchoredPosition = initialPositions[i];
				headerImages [i].rectTransform.localEulerAngles = new Vector3(0, 0, 0);
				headerImages[i].sprite = placeHolder;
			}
			yield return SetHeaderLayoutEnabled (true);
		}

		private IEnumerator SetHeaderLayoutEnabled(bool enabled) {
			HorizontalLayoutGroup layout = GameObject.Find ("Header").GetComponent<HorizontalLayoutGroup> ();
			layout.enabled = enabled;
			yield break;
		}
	}
}