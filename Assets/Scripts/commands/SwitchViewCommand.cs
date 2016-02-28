using UnityEngine;
using System.Collections;
using strange.extensions.command.impl;
using net.peakgames.codebreaker.views;

namespace net.peakgames.codebreaker.commands {
	public class SwitchViewCommand : Command {
		[Inject]
		public IViewSwitcher viewSwitcher { get; set; }

		[Inject]
		public ViewType viewType {get; set;}

		public override void Execute() {
			viewSwitcher.SwitchWithAnimationTo (viewType);	
		}
	}

}
