using UnityEngine;
using UnityEngine.UI;
using strange.extensions.mediation.impl;

namespace net.peakgames.codebreaker.views
{
	public class GameView : View, IGameView
	{
		
		[SerializeField]
		private Color[] colors = new Color[GameLogic.MAX_NUMBER_OF_POSSIBLE_VALUES];

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


		private GameViewMediator mediator;
		private int headerImageIndex = 0;

		protected override void Start () {		
			for (int i = 0; i < inputButtons.Length; i++) {
				UpdateButtonColor (inputButtons [i], colors [i]);
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
			UpdateHeader (buttonIndex);
			this.mediator.OnPlayerMadeGuess (buttonIndex);
		}

		public void OnGuessResult (int[] guess, Result result) {
			ResetHeader ();
			GameObject resultItem = Instantiate (this.guessResultPrefab);
			resultItem.transform.SetParent (scrollContent.transform);

			GuessResultItemView itemView = resultItem.GetComponent<GuessResultItemView> ();
			itemView.UpdateGuess (guess, result, colors);

			EnableAllInputButtons ();
		}

		private void EnableAllInputButtons() {
			foreach (Button button in inputButtons) {
				button.interactable = true;
			}
		}

		private void ResetHeader () {
			foreach (Image image in headerImages) {
				image.color = Color.white;
			}
		}

		private void UpdateHeader (int buttonIndex) {
			Image image = headerImages [headerImageIndex];
			image.color = colors [buttonIndex];
			headerImageIndex = (headerImageIndex + 1) % GameLogic.MAX_NUMBERS;
		}
	}
}