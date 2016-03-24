using UnityEngine;
using strange.extensions.context.impl;

namespace net.peakgames.codebreaker {
	public class CodeBreakerRoot : ContextView {

		[SerializeField]
		private bool IntegrationTestsEnabled = false;

		[SerializeField]
		private int TimeScaleForIntegrationTests = 1;

		void Awake () {
			if (IsIntegrationTest()) {
				Time.timeScale = TimeScaleForIntegrationTests;
				context = new IntegrationTestContext (this);
				CreateTestRunnerIfNecessary ();
			} else {
				context = new CodeBreakerContext (this);
			}
		}

		void Update() {
			if (Input.GetKeyDown (KeyCode.Escape)) {
				HandleBackButtonPress ();
			}
		}

		private void HandleBackButtonPress() {			
			Application.Quit ();
		}

		private bool IsIntegrationTest() {
			if(Application.isEditor) {
				try {
					string commandLineOptions = System.Environment.CommandLine;
					return IntegrationTestsEnabled || 
						(commandLineOptions != null && commandLineOptions.Contains ("-batchmode"));
				} catch(System.Exception) {}
			}
			return false;
		}

		private void CreateTestRunnerIfNecessary () {
			if(FindObjectOfType(typeof(UnityTest.TestRunner)) == null)  {
				GameObject go = new GameObject ();
				go.name = "TestRunner";
				go.AddComponent<UnityTest.TestRunner> ();
			}
		}
	}
}
