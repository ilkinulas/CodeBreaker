namespace net.peakgames.codebreaker {
	public class FakeRandomNumberGenerator : RandomNumberInterface {
		private int[] numbers;
		public int [] Numbers {
			get { return this.numbers; }
			set{ this.numbers = value; }
		}
		private int index = 0;
		public int Next(int max) {
			int result = numbers [index];
			index = (index + 1) % GameLogic.MAX_NUMBERS;
			return result;
		}
	}
}
