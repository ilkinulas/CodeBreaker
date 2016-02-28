using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;

namespace net.peakgames.codebreaker.views {
	public class GameOverViewMediator : Mediator {
		[Inject]
		public IGameOverView view { get; set; }
	}
}
