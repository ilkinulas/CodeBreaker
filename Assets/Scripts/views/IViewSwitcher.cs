namespace net.peakgames.codebreaker.views {
	public enum ViewType {
		Intro, Game, GameOver
	}

	public interface IViewSwitcher {
		void Initialize();
		void SwitchTo(ViewType view);
		void SwitchWithAnimationTo (ViewType view);
		ViewType GetCurrentViewType();
	}
}	
