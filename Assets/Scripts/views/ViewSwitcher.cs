using UnityEngine;
using System.Collections;
using net.peakgames.codebreaker.util;
using strange.extensions.context.api;

namespace net.peakgames.codebreaker.views {
	
	public class ViewSwitcher : IViewSwitcher {

		public GameObject introView;
		public GameObject gameView;
		public GameObject gameOverView;

		[Inject]
		public CoroutineRunner coroutineRunner { get; set; }

		[Inject(ContextKeys.CONTEXT_VIEW)]
		public GameObject contextView{ get; set; }

		private GameObject currentView;

		public void Initialize () {
			GameObject canvas = contextView.transform.FindChild ("Canvas").gameObject;
			introView = canvas.transform.FindChild ("IntroView").gameObject;
			gameView = canvas.transform.FindChild ("GameView").gameObject;
			gameOverView = canvas.transform.FindChild ("GameOverView").gameObject;

			gameView.SetActive (false);
			gameOverView.SetActive (false);
			introView.SetActive (true);

			currentView = introView;
		}

		public void SwitchTo(ViewType view) {
			SwitchInternal (view, false, 0);
		}

		public void SwitchWithAnimationTo(ViewType view) {
			SwitchInternal (view, true, 0.5f);
		}

		private void SwitchInternal (ViewType viewType, bool animate, float animationDuration) {		
			GameObject nextView = GetNextView (viewType);
			if (animate) {
				coroutineRunner.StartCoroutine(SetCurrentScreen(nextView, animationDuration));
			} else {
				currentView.SetActive (false);
				currentView = nextView;
				currentView.SetActive (true);
			}
		}

		private IEnumerator SetCurrentScreen(GameObject nextView, float duration) {
			RectTransform rectTransform = nextView.transform as RectTransform;
			rectTransform.anchoredPosition = new Vector2(0, Screen.height);

			currentView.SetActive (false);
			nextView.SetActive (true);

			float y = rectTransform.anchoredPosition.y;
			float screenHeight = Screen.height;
			float elapsedTime = 0;

			while (y > 0) {
				elapsedTime += Time.deltaTime;
				y = Mathf.SmoothStep(screenHeight, 0, elapsedTime / duration);
				rectTransform.anchoredPosition = new Vector2(0, y);
				yield return null;
			}

			rectTransform.anchoredPosition = new Vector2(0, 0);

			currentView = nextView;
		}

		private GameObject GetNextView(ViewType view) {
			switch (view) {
			case ViewType.Intro:
				return introView;
			case ViewType.Game:
				return gameView;
			case ViewType.GameOver:
				return gameOverView;
			default:
				return introView;
			}
		}
	}
}