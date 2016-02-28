using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using strange.extensions.mediation.impl;
using net.peakgames.codebreaker.util;

namespace net.peakgames.codebreaker.views {
	public class GameView : View, IGameView {

		public Color[] colors = new Color[GameLogic.MAX_NUMBER_OF_POSSIBLE_VALUES];
		public Image[] headerImages = new Image[GameLogic.MAX_NUMBERS];
		public Button[] inputButtons = new Button[GameLogic.MAX_NUMBER_OF_POSSIBLE_VALUES];

		private GameViewMediator mediator;
		private int headerImageIndex = 0;

		protected override void Start() {		
			for (int i = 0; i < inputButtons.Length; i++) {
				UpdateButtonColor (inputButtons [i], colors [i]);
			}
		}

		private void UpdateButtonColor(Button button, Color color) {
			ColorBlock colorBlock = button.colors;
			colorBlock.normalColor = color;
			colorBlock.highlightedColor = color;
			colorBlock.pressedColor = color * 0.8f;
			button.colors = colorBlock;
		}

		public void Init(GameViewMediator mediator) {			
			this.mediator = mediator;
		}

		public void OnInputButtonPressed(int buttonIndex) {		
			UpdateHeader (buttonIndex);
			this.mediator.OnPlayerMadeGuess (buttonIndex);
		}

		public void OnGuessResult(int [] guess, Result result) {
			StartCoroutine (Delay(1, ResetHeader));
		}

		private IEnumerator Delay(int seconds, Action action) {
			yield return new WaitForSeconds (seconds);
			action ();
		}

		private void ResetHeader() {
			foreach (Image image in headerImages) {
				image.color = Color.white;
			}
		}

		private void UpdateHeader(int buttonIndex) {
			Image image = headerImages [headerImageIndex];
			image.color = colors [buttonIndex];
			headerImageIndex = (headerImageIndex + 1) % GameLogic.MAX_NUMBERS;
		}
	}
}

