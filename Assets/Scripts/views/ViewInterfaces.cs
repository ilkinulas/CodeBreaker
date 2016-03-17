using UnityEngine;
using System.Collections;
namespace net.peakgames.codebreaker.views {

	public interface IIntroView  {
		void Init (IntroViewMediator mediator);
	}

	public interface IGameOverView  {
		void Init (GameOverViewMediator mediator);
		void UpdateView (int[] solution, bool newRecord, int numTries, int best, int total);
	}

	public interface IGameView  {
		void Init (GameViewMediator mediator);
		void OnGuessResult(int [] guess, Result result);
	}


}


