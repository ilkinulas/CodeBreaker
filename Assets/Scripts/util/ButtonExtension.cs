using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using strange.extensions.mediation.impl;
using net.peakgames.codebreaker.signals;
using net.peakgames.codebreaker.audio;

namespace net.peakgames.codebreaker.util {
	public class ButtonExtension  : View, IPointerDownHandler{

		[Inject]
		public PlaySoundSignal playSoundSignal { get; set; }

		private Button button;

		protected override void OnEnable() {
			button = gameObject.GetComponent<Button>();
		}

		public void OnPointerDown (PointerEventData eventData) {
			if (button.interactable) {
				playSoundSignal.Dispatch (GameSound.ButtonClick);
			}
		}
	}

}
