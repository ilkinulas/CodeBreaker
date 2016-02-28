using UnityEngine;
using System.Collections.Generic;
using strange.extensions.signal.impl;
using net.peakgames.codebreaker.views;

namespace net.peakgames.codebreaker.signals {
	public enum LoginType {
		Guest, Facebook
	}
	public class StartAppSignal : Signal {}
	public class PlayerGuessSignal : Signal<int []> {}
	public class GuessResultSignal : Signal<int[], Result> { public GuessResultSignal() {AddListener((guess, result)=>{});}}
	public class SwitchViewSignal : Signal<ViewType> {}
	public class LoginRequestSignal : Signal<LoginType> {}
	public class LoginSuccessSignal : Signal<LoginType> { public LoginSuccessSignal() { AddListener((loginType) => {});}}
	public class LoginFailedSignal : Signal<LoginType, string> { public LoginFailedSignal() { AddListener((loginType, errorMessage) => {});}}
}
