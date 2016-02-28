using UnityEngine;
using System.Collections;
namespace net.peakgames.codebreaker.views {

	public interface IIntroView  {
		void Init (IntroViewMediator mediator);
	}

	public interface IGameOverView  {
		void Init (GameOverViewMediator mediator);
	}

	public interface IGameView  {
		void Init (GameViewMediator mediator);
		void OnInputButtonPressed (int buttonIndex);
		void OnGuessResult(int [] guess, Result result);
	}


}


