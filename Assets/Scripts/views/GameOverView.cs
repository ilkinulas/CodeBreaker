using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;

namespace net.peakgames.codebreaker.views {
	public class GameOverView : View , IGameOverView{

		private GameOverViewMediator mediator;

		public void Init(GameOverViewMediator  mediator) {
			this.mediator = mediator;
		}

	}
}
