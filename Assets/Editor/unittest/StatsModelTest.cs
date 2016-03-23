using UnityEngine;
using System.Collections;
using NUnit.Framework;

namespace net.peakgames.codebreaker {
	public class StatsModelTest : MonoBehaviour {

		private StatsModel stats;

		[SetUp]
		public void SetUp() {
			stats = new StatsModel ();
			stats.Reset ();
		}

		[Test]
		public void IsBestScoreTest() {			
			Assert.AreEqual (-1, stats.BestScore);
			Assert.IsTrue (stats.IsBestScore (7));

			stats.BestScore = 5;
			Assert.IsFalse (stats.IsBestScore (7));
			Assert.IsTrue (stats.IsBestScore (3));
		}

		[Test]
		public void StatsShouldPersistAccrossInstances() {
			StatsModel stats1 = new StatsModel ();
			stats1.NumberOfGamesPlayed = 15;
			stats1.BestScore = 4;

			StatsModel stats2 = new StatsModel ();
			stats2.Load ();
			Assert.AreEqual (15, stats2.NumberOfGamesPlayed);
			Assert.AreEqual (4, stats2.BestScore);
		}

		[Test]
		public void ResetShouldWipeOutPersistedData() {
			StatsModel stats1 = new StatsModel ();
			stats.NumberOfGamesPlayed = 15;
			stats.BestScore = 4;
			stats1.Reset ();

			StatsModel stats2 = new StatsModel ();
			stats2.Load ();
			Assert.AreEqual (0, stats2.NumberOfGamesPlayed);
			Assert.AreEqual (-1, stats2.BestScore);
		}
	}
}
