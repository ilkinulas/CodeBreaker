using UnityEngine;
using System.Collections;
using strange.extensions.context.api;
using strange.extensions.context.impl;
using strange.extensions.command.api;
using strange.extensions.command.impl;
using net.peakgames.codebreaker.signals;
using net.peakgames.codebreaker.commands;
using net.peakgames.codebreaker.views;
using net.peakgames.codebreaker.util;

namespace net.peakgames.codebreaker {

	public class CodeBreakerContext : MVCSContext {

		public CodeBreakerContext(MonoBehaviour root) : base (root) { }

		public override IContext Start() {
			base.Start();
			StartAppSignal startSignal= (StartAppSignal)injectionBinder.GetInstance<StartAppSignal>();
			startSignal.Dispatch();
			return this;
		}

		protected override void addCoreComponents () {
			base.addCoreComponents ();
			injectionBinder.Unbind<ICommandBinder>();
			injectionBinder.Bind<ICommandBinder>().To<SignalCommandBinder>().ToSingleton();
		}

		protected override void mapBindings() {
			base.mapBindings ();
			mediationBinder.Bind<IntroView> ().ToAbstraction<IIntroView> ().To<IntroViewMediator>();
			mediationBinder.Bind<GameView> ().ToAbstraction<IGameView> ().To<GameViewMediator>();
			mediationBinder.Bind<GameOverView> ().ToAbstraction<IGameOverView> ().To<GameOverViewMediator>();

			commandBinder.Bind<StartAppSignal> ().To<StartAppCommand> ().Once();
			commandBinder.Bind<PlayerGuessSignal> ().To<PlayerGuessCommand> ();
			commandBinder.Bind<SwitchViewSignal> ().To<SwitchViewCommand> ();
			commandBinder.Bind<LoginRequestSignal> ().To<LoginRequestCommand> ();

			injectionBinder.Bind<GuessResultSignal> ().ToSingleton ();
			injectionBinder.Bind<LoginSuccessSignal> ().ToSingleton ();
			injectionBinder.Bind<LoginFailedSignal> ().ToSingleton ();
			injectionBinder.Bind<GameModel> ().ToSingleton ();
			injectionBinder.Bind<CoroutineRunner> ().ToSingleton ();
			injectionBinder.Bind<IViewSwitcher> ().To<ViewSwitcher> ().ToSingleton();
		}
	}

}