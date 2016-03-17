using UnityEngine;
using System.Collections;
namespace net.peakgames.codebreaker {

	public class StatsModel  {
		private const string KEY_NUM_GAMES = "KEY_NUM_GAMES";
		private const string KEY_BEST = "KEY_BEST";

		private int numberOfGamesPlayed;
		private int bestScore;

		public int NumberOfGamesPlayed {
			get { return numberOfGamesPlayed; }
			set {
				numberOfGamesPlayed = value;
				SaveInt (KEY_NUM_GAMES, numberOfGamesPlayed);
			}
		}

		public int BestScore {
			get { return bestScore; }
			set {
				bestScore = value;
				SaveInt (KEY_BEST, bestScore);
			}
		}

		public void Load() {
			this.numberOfGamesPlayed = PlayerPrefs.GetInt (KEY_NUM_GAMES);
			this.bestScore = PlayerPrefs.GetInt (KEY_BEST);
			if (this.bestScore == 0) { 
				this.bestScore = -1;
			}
		}

		public void Reset() {
			NumberOfGamesPlayed = 0;
			BestScore = -1;
		}

		public bool IsBestScore(int score) {
			if (score < 0) {
				return false;
			}
			if (BestScore < 0) {
				return true;
			}
			return BestScore > score;
		}

		private void SaveInt(string key, int value) {
			PlayerPrefs.SetInt (key, value);
			PlayerPrefs.Save ();
		}
	}

}

