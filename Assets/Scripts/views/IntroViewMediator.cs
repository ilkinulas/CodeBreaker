using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;
using net.peakgames.codebreaker.signals;

namespace net.peakgames.codebreaker.views {
	public class IntroViewMediator : Mediator{

		[Inject]
		public IIntroView view { get; set; }

		[Inject]
		public LoginRequestSignal loginRequestSignal {get; set;}

		[Inject]
		public IViewSwitcher viewSwitcher { get; set; }

		[Inject]
		public GameModel gameModel { get; set; }

		public override void OnRegister() {
			view.Init (this);
		}

		public void StartGuestLogin() {
			loginRequestSignal.Dispatch (LoginType.Guest);
		}

		public void StartFacebookLogin() {
			loginRequestSignal.Dispatch (LoginType.Facebook);
		}

		[ListensTo(typeof(LoginSuccessSignal))]
		public void OnLoginSuccess(LoginType loginType) {
			gameModel.StartGame ();
			viewSwitcher.SwitchWithAnimationTo (ViewType.Game);
		}

		[ListensTo(typeof(LoginFailedSignal))]
		public void OnLoginFailed(LoginType loginType, string errorMessage) {
		}
	}
}
