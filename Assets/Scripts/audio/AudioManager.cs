using UnityEngine;
using System;
using System.Collections.Generic;
using net.peakgames.codebreaker.signals;

namespace net.peakgames.codebreaker.audio {
	
	public class AudioManager : IAudioManager {

		[Inject]
		public PlaySoundSignal playSoundSignal { get; set; }

		private AudioSource audioSource;
		private AudioClip resultSound;

		private Dictionary<GameSound, AudioClip> soundMap = new Dictionary<GameSound, AudioClip>();

		public void Initialize() {
			playSoundSignal.AddListener (Play);
			audioSource = Camera.main.GetComponent<AudioSource>();
			soundMap.Add (GameSound.Result, Resources.Load ("Audio/guess_result") as AudioClip);
			soundMap.Add (GameSound.ButtonClick, Resources.Load ("Audio/button_click") as AudioClip);
			soundMap.Add (GameSound.Congrats, Resources.Load ("Audio/congratulations") as AudioClip);
		}

		public void Play(GameSound sound) {
			AudioClip audioClip = null;
			soundMap.TryGetValue (sound, out audioClip);
			if (audioClip != null) {
				audioSource.PlayOneShot (audioClip);
			}
		}
	}

}
