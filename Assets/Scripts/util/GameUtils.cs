using UnityEngine;
namespace net.peakgames.codebreaker.util {
	public class GameUtils {

		public static string[] ANIMALS = {
			"giraffe",
			"elephant", 
			"hippo",
			"monkey",
			"panda",
			"parrot",
			"penguin",
			"pig",
			"rabbit",
			"snake"
		};
			
			
		public static float ToDP(float pixels) {
			return Mathf.Max(
				pixels , 
				pixels * Screen.dpi / 160f);
		}

		public static Sprite GetAnimalSprite(int index) {
			string spriteName = "Sprites/" + GameUtils.ANIMALS [index];
			return Resources.Load<Sprite> (spriteName);
		}
	}
}

