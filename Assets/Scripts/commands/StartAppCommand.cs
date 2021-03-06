﻿using UnityEngine;
using System.Collections;
using strange.extensions.command.impl;
using net.peakgames.codebreaker.views;
using net.peakgames.codebreaker.audio;

namespace net.peakgames.codebreaker.commands {
	public class StartAppCommand : Command {

		[Inject]
		public IViewSwitcher viewSwitcher { get; set; }

		[Inject]
		public StatsModel statsModel { get; set; }

		[Inject]
		public IAudioManager audioManager {get; set;}

		public override void Execute () {
			viewSwitcher.Initialize ();
			statsModel.Load ();
			audioManager.Initialize ();
		}	
	}
}
