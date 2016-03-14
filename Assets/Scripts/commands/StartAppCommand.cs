using UnityEngine;
using System.Collections;
using strange.extensions.command.impl;
using net.peakgames.codebreaker.views;

namespace net.peakgames.codebreaker.commands {
	public class StartAppCommand : Command {

		[Inject]
		public IViewSwitcher viewSwitcher { get; set; }

		public override void Execute () {
			viewSwitcher.Initialize ();
		}	
	}
}
