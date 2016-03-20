using UnityEngine;
using System.Collections;

namespace net.peakgames.codebreaker.audio {

	public enum GameSound {
		Result, ButtonClick, Congrats
	}

	public interface IAudioManager {
		void Initialize ();
		void Play(GameSound sound);
	}

}
