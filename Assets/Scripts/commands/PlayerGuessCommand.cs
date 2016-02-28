using UnityEngine;
using System.Collections;
using strange.extensions.command.impl;
using net.peakgames.codebreaker.signals;

namespace net.peakgames.codebreaker.commands {	
	public class PlayerGuessCommand : Command {
		[Inject]
		public int [] guess { get; set; }

		[Inject]
		public GameModel gameModel { get; set; }

		[Inject]
		public GuessResultSignal guessResultSignal { get; set; }

		public override void Execute () {
			GuessResult guessResult = gameModel.MakeAGuess (guess);			
			guessResultSignal.Dispatch (guessResult.guess, guessResult.result);
		}	
	}
}
