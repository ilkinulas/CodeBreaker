using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;

namespace net.peakgames.codebreaker.views {
	public class IntroView : View, IIntroView {

		private IntroViewMediator mediator;

		public void Init(IntroViewMediator  mediator) {
			this.mediator = mediator;
		}

		public void OnGuestButtonClicked() {
			mediator.StartGuestLogin ();
		}

		public void OnFacebookButtonClicked() {
			mediator.StartFacebookLogin ();
		}
	}

}
