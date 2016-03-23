using UnityEngine;
using System.Collections;
namespace net.peakgames.codebreaker {
	
	public class IntegrationTestContext : CodeBreakerContext {
		public IntegrationTestContext (MonoBehaviour view) : base(view) { }

		protected override void mapBindings() {
			base.mapBindings ();
			injectionBinder.Unbind<RandomNumberInterface> ();
			injectionBinder.Bind<RandomNumberInterface> ().To<FakeRandomNumberGenerator> ().ToSingleton();
			Debug.Log ("IntegrationTestContext running....");
		}
	}

}
