using System;
using System.Collections.Generic;

namespace net.peakgames.codebreaker {
	
	public class GameLogic {
		
		public const int MAX_NUMBERS = 4;
		public const int MAX_NUMBER_OF_POSSIBLE_VALUES = 10;

		private readonly int [] solution;
		public enum MatchType {NONE, WHITE, BLACK}

		public GameLogic(int [] solution) {
			this.solution = solution;
		}

		public static int [] CreateRandomSolution(RandomNumberInterface random) {
			int [] array = new int[GameLogic.MAX_NUMBER_OF_POSSIBLE_VALUES];
			for (int i = 0; i < array.Length; i++) {
				array [i] = i;
			}
			Shuffle (array, random);
			int [] result =  new int[GameLogic.MAX_NUMBERS];
			Array.Copy (array, result, GameLogic.MAX_NUMBERS);
			return result;
		}

		public Result Check(int [] guess) {
			int whites = 0;
			int blacks = 0;
			List<int> processed = new List<int> ();
			for (int i = 0; i < MAX_NUMBERS; i++) {
				if (guess [i] == solution [i]) {
					whites++;
					processed.Add (i);
				}
			}

			for (int i = 0; i < MAX_NUMBERS; i++) {
				for (int j = 0; j < MAX_NUMBERS; j++) {
					int aGuess = guess [i];
					if (!processed.Contains (j) && aGuess == solution [j]) {
						processed.Add (j);
						blacks++;
						break;
					}
				}
			}
			return new Result(whites, blacks);
		}			

		public static void Shuffle (int [] array, RandomNumberInterface random) {			
			int n = array.Length;
			while (n > 1) {
				int k = random.Next(n--);
				int temp = array[n];
				array[n] = array[k];
				array[k] = temp;
			}
		}
	}

	public class Result {
		public readonly int whiteCount;
		public readonly int blackCount;

		public Result(int white, int black) {
			this.whiteCount = white;
			this.blackCount = black;
		}

		public bool IsCorrect() {
			return this.whiteCount == GameLogic.MAX_NUMBERS;
		}

		public override string ToString () {
			return string.Format ("white {0} black {1} correct {2}", whiteCount, blackCount, IsCorrect()); 
		}
	}
}
