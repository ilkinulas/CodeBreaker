using UnityEngine;

namespace net.peakgames.codebreaker {
	public class RandomNumberGenerator : RandomNumberInterface {
		public int Next(int max) {
			return Random.Range (0, max);
		}
	}
}
