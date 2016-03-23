using System;

namespace net.peakgames.codebreaker {
	public class RandomNumberGenerator : RandomNumberInterface {
		public int Next(int max) {
			return new Random ().Next (max);
		}
	}
}
