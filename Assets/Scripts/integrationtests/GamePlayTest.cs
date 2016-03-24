using System.Collections;
using net.peakgames.codebreaker.views;

namespace net.peakgames.codebreaker.test {
	
	[IntegrationTest.DynamicTest("GameScene")]
	[IntegrationTest.Timeout(120)]
	public class GamePlayTest : IntegrationTestBase {

		protected override IEnumerator[]  RunTest() {
			(randomNumberGenerator as FakeRandomNumberGenerator).Numbers = new int[] {0, 0, 0, 0};
			return new IEnumerator [] {
				Wait(3),
				ClickButton("FacebookButton"),
				Wait(3),
				MakeAGuess(new int[] {0, 1, 2, 3}),
				MakeAGuess(new int[] {1, 3, 2, 4}),
				MakeAGuess(new int[] {1, 4, 2, 3}),
				MakeAGuess(new int[] {1, 2, 3, 4}),
				Wait(3),
				AssertViewType(ViewType.GameOver),
				ClickButton("NewGameButton"),
				Wait(3),
				AssertViewType(ViewType.Game),
				TestPass()
			};
		}

		private IEnumerator MakeAGuess(int [] guess){ 
			foreach (var i in guess) {
				yield return ClickButton ("input_button_" + i);
				yield return Wait (0.5f);
			}
			yield return ClickButton ("SubmitButton");
			yield return Wait (3);
		}


	}

}
