using UnityEngine;
using System;
using System.Collections;
using strange.extensions.mediation.impl;
using UnityEngine.UI;
using net.peakgames.codebreaker.views;

namespace net.peakgames.codebreaker.test {
	
	[IntegrationTest.DynamicTest("GameScene")]
	[IntegrationTest.ExcludePlatformAttribute(RuntimePlatform.Android, RuntimePlatform.IPhonePlayer)]
	[IntegrationTest.Timeout(120)]
	public class GamePlayTest : View {

		[Inject]
		public RandomNumberInterface randomNumberGenerator { get; set; }

		[Inject]
		public IViewSwitcher viewSwitcher { get; set; }

		protected override void Start () {					    
			base.Start ();
			StartCoroutine (RunTest ());
		}

		private IEnumerator RunTest() {
			(randomNumberGenerator as FakeRandomNumberGenerator).Numbers = new int[] {0, 0, 0, 0};
			IEnumerator[] actions = {
				Wait(3),
				ClickButton("FacebookButton"),
				Wait(3),
				MakeAGuess(new int[] {0, 1, 2, 3}),
				Wait(3),
				MakeAGuess(new int[] {1, 3, 2, 4}),
				Wait(3),
				MakeAGuess(new int[] {1, 4, 2, 3}),
				Wait(3),
				MakeAGuess(new int[] {1, 2, 3, 4}),
				Wait(5),
				AssertViewType(ViewType.GameOver),
				ClickButton("NewGameButton"),
				Wait(5),
				AssertViewType(ViewType.Game),
				TestPass()
			};

			foreach (IEnumerator action in actions ){ 
				yield return action;
			}
		}

		private IEnumerator TestPass() {
			IntegrationTest.Pass ();
			yield break;
		}

		private IEnumerator MakeAGuess(int [] guess){ 
			foreach (var i in guess) {
				yield return ClickButton ("input_button_" + i);
				yield return Wait (1);
			}
			yield return ClickButton ("SubmitButton");
		}

		protected IEnumerator Wait(int seconds){
			yield return new WaitForSeconds (seconds);
		}

		protected IEnumerator ClickButton(string path, float timeout = 10) {
			float elapsedTime = 0;
			GameObject go = FindByPath (path);
			if (go != null) {
				InvokeButtonClick (go);
				yield break;
			}

			while (go == null) {
				elapsedTime += Time.deltaTime;
				if (elapsedTime > timeout) {
					throw new Exception ("GameObject " + path + " not found.");
				}
				go = FindByPath (path);
				if (go != null) {
					InvokeButtonClick (go);
					break;
				} else {
					yield return null;
				}
			}
			yield break;
		}

		private void InvokeButtonClick(GameObject go) {	
			go.GetComponent<Button> ().onClick.Invoke ();
		}
	
		private GameObject FindByPath(string path) {
			string[] parts = path.Split ('/');
			GameObject root = GameObject.Find(parts[0]);
			if (root == null) {
				return null;
			}
			if (parts.Length == 1) {
				return root;
			}

			for (int i = 1; i < parts.Length; i++) {
				Transform transform = root.transform.Find (parts[i]);
				if (transform == null) {
					return null;
				} else {
					root = transform.gameObject;
				}
			}
			return root;
		}

		private IEnumerator AssertViewType(ViewType type) {
			if (viewSwitcher.GetCurrentViewType () != type) {
				throw new Exception ("Expected view type " + type + " but current view is " + viewSwitcher.GetCurrentViewType());
			}
			yield break;
		}
	}

}
