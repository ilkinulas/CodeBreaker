using UnityEngine;
using System.Collections;
using strange.extensions.command.impl;
using net.peakgames.codebreaker.signals;
using net.peakgames.codebreaker.views;
using net.peakgames.codebreaker.audio;

namespace net.peakgames.codebreaker.commands {	

	public class PlayerGuessCommand : Command {
		
		[Inject]
		public int [] guess { get; set; }

		[Inject]
		public GameModel gameModel { get; set; }

		[Inject]
		public GuessResultSignal guessResultSignal { get; set; }

		[Inject]
		public IViewSwitcher viewSwitcher { get; set; }

		[Inject]
		public GameOverSignal gameOverSignal { get; set; }

		[Inject]
		public StatsModel statsModel { get; set; }

		[Inject]
		public PlaySoundSignal playSoundSignal { get; set; }

		public override void Execute () {
			GuessResult guessResult = gameModel.MakeAGuess (guess);			
			guessResultSignal.Dispatch (guessResult.guess, guessResult.result);

			if (guessResult.result.IsCorrect ()) {
				viewSwitcher.SwitchWithAnimationTo (ViewType.GameOver);
				bool bestScore = statsModel.IsBestScore(gameModel.NumberOfGuesses);
				if (bestScore) {
					statsModel.BestScore = gameModel.NumberOfGuesses;
				}
				statsModel.NumberOfGamesPlayed++;
				gameOverSignal.Dispatch (guessResult.guess, bestScore);
				playSoundSignal.Dispatch (GameSound.Congrats);
			}
		}	
	}
}
