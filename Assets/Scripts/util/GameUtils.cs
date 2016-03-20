using UnityEngine;
namespace net.peakgames.codebreaker.util {
	public class GameUtils {
		
		public static float ToDP(float pixels) {
			return Mathf.Max(
				pixels , 
				pixels * Screen.dpi / 160f);
		}
	}
}

