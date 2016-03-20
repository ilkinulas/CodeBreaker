using UnityEngine;
using System.Collections;
using strange.extensions.context.impl;

namespace net.peakgames.codebreaker {
	public class CodeBreakerRoot : ContextView {

		void Awake () {
			context = new CodeBreakerContext (this);
		}

		void Update() {
			if (Input.GetKeyDown (KeyCode.Escape)) {
				HandleBackButtonPress ();
			}
		}

		private void HandleBackButtonPress() {			
			Application.Quit ();
		}
	}
}
