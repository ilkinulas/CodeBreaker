using UnityEngine;
using System.Collections;
using strange.extensions.command.impl;
using net.peakgames.codebreaker.signals;
using net.peakgames.codebreaker.views;

namespace net.peakgames.codebreaker.commands {	
	public class StartNewGameCommand : Command {
	
		[Inject]
		public IViewSwitcher viewSwitcher { get; set; }

		[Inject]
		public GameModel gameModel { get; set; }

		[Inject]
		public StatsModel statsModel { get; set; }

		public override void Execute () {
			gameModel.StartGame ();
			statsModel.NumberOfGamesPlayed++;
			viewSwitcher.SwitchWithAnimationTo (ViewType.Game);	
		}
	}
}
