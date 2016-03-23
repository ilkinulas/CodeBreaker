using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using System.Collections.Generic;
namespace net.peakgames.codebreaker {

	public class GameLogicTest {

		[TestFixtureSetUp]
		public void Init() {
			//Init runs once before running test cases.
		}

		[TestFixtureTearDown]
		public void CleanUp() {
			//CleanUp runs once after all test cases are finished.
		}

		[SetUp]
		public void SetUp() {
			//SetUp runs before all test cases
		}

		[TearDown]
		public void TearDown() {
			//SetUp runs after all test cases
		}

		[Test]
		public void SolutionItemsMustBeDistinct() {
			HashSet<int> hashSet = new HashSet<int> ();
			for (int i = 0; i < 10; i++) {
				int[] solution = GameLogic.CreateRandomSolution(new RandomNumberGenerator());
				foreach (int sol in solution) {
					hashSet.Add (sol);
				}
				Assert.AreEqual (4, hashSet.Count);
				hashSet.Clear ();
			}
		}

		[Test]
		public void SuccessfulGuess() {
			GameLogic logic = new GameLogic (new int[] {1, 2, 3, 4});
			int [] guess = new int [] {1, 2, 3, 4};
			Result result = logic.Check (guess);
			Assert.AreEqual (4, result.whiteCount);
			Assert.AreEqual (0, result.blackCount);
			Assert.IsTrue (result.IsCorrect ());
		}

		[Test]
		public void AllGuessesAreMissing() {
			GameLogic logic = new GameLogic (new int[] {1, 2, 3, 4});
			int [] guess = new int [] {5, 6, 7, 8};
			Result result = logic.Check (guess);
			Assert.AreEqual (0, result.whiteCount);
			Assert.AreEqual (0, result.blackCount);
			Assert.IsFalse (result.IsCorrect ());
		}

		[Test]
		public void AllGuessesAreBlack() {
			GameLogic logic = new GameLogic (new int[] {1, 2, 3, 4});
			int [] guess = new int [] {2, 1, 4, 3,};
			Result result = logic.Check (guess);
			Assert.AreEqual (0, result.whiteCount);
			Assert.AreEqual (4, result.blackCount);
			Assert.IsFalse (result.IsCorrect ());
		}

		[Test]
		public void TwoBlacksAndTwoWhites() {
			GameLogic logic = new GameLogic (new int[] {1, 2, 3, 4});
			int [] guess = new int [] {1, 2 , 4 , 3};
			Result result = logic.Check (guess);
			Assert.AreEqual (2, result.whiteCount);
			Assert.AreEqual (2, result.blackCount);
			Assert.IsFalse (result.IsCorrect ());
		}

		[Test]
		public void SameDigitSolutionAndWrongGuess() {
			GameLogic logic = new GameLogic (new int[] {0, 0, 0, 0});
			int [] guess = new int [] {1, 2 , 4 , 3};
			Result result = logic.Check (guess);
			Assert.AreEqual (0, result.whiteCount);
			Assert.AreEqual (0, result.blackCount);
			Assert.IsFalse (result.IsCorrect ());
		}

		[Test]
		public void SameDigitSolutionAndCorrectGuess() {
			GameLogic logic = new GameLogic (new int[] {0, 0, 0, 0});
			int [] guess = new int [] {0, 0 ,0 ,0};
			Result result = logic.Check (guess);
			Assert.AreEqual (4, result.whiteCount);
			Assert.AreEqual (0, result.blackCount);
			Assert.IsTrue (result.IsCorrect ());
		}

		[Test]
		public void SameDigitGuessWithWhites() {
			GameLogic logic = new GameLogic (new int[] {7, 4, 8, 1});
			int [] guess = new int [] {7, 7, 7 ,7};
			Result result = logic.Check (guess);
			Assert.AreEqual (1, result.whiteCount);
			Assert.AreEqual (0, result.blackCount);
			Assert.IsFalse (result.IsCorrect ());
		}
	}
}