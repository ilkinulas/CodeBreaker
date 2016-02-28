using UnityEngine;
using System.Collections;
using strange.extensions.command.impl;
using net.peakgames.codebreaker.signals;

namespace net.peakgames.codebreaker.commands {

	public class LoginRequestCommand : Command {
		
		[Inject]
		public LoginType loginType {get; set;}

		[Inject]
		public LoginSuccessSignal loginSuccessSignal {get; set;}

		[Inject]
		public LoginFailedSignal loginFailedSignal {get; set;}

		public override void Execute() {
			loginSuccessSignal.Dispatch (loginType);	
		}
	}

}
