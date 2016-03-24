using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using strange.extensions.mediation.impl;
using net.peakgames.codebreaker.views;

namespace net.peakgames.codebreaker.test {
	
	public abstract class IntegrationTestBase : View {
		[Inject]
		public RandomNumberInterface randomNumberGenerator { get; set; }

		[Inject]
		public IViewSwitcher viewSwitcher { get; set; }

		protected abstract IEnumerator [] RunTest ();

		protected override void Start () {					    
			base.Start ();
			StartCoroutine (ExecuteTestSteps ());
		}

		private IEnumerator ExecuteTestSteps() {
			IEnumerator [] steps = RunTest ();
			foreach (var step in steps) {
				yield return step;
			}
		}

		#region Share utility methods
		protected IEnumerator TestPass() {
			IntegrationTest.Pass ();
			yield break;
		}

		protected IEnumerator TestFail() {
			IntegrationTest.Fail ();
			yield break;
		}

		protected IEnumerator Wait(float seconds){
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

		protected void InvokeButtonClick(GameObject go) {	
			go.GetComponent<Button> ().onClick.Invoke ();
		}

		protected GameObject FindByPath(string path) {
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

		protected IEnumerator AssertViewType(ViewType type) {
			if (viewSwitcher.GetCurrentViewType () != type) {
				throw new Exception ("Expected view type " + type + " but current view is " + viewSwitcher.GetCurrentViewType());
			}
			yield break;
		}	
		#endregion
	}
}