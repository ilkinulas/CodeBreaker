using UnityEngine;
using System.Collections;
using strange.extensions.context.api;

namespace net.peakgames.codebreaker.util {
	public class CoroutineRunner  {

		[Inject(ContextKeys.CONTEXT_VIEW)]
		public GameObject contextView{ get; set; }

		private CoroutineRunnerBehaviour runner;

		[PostConstruct]
		public void PostConstruct() {
			runner= contextView.AddComponent<CoroutineRunnerBehaviour> ();
		}

		public Coroutine StartCoroutine(IEnumerator routine) {
			return runner.StartCoroutine(routine);
		}

		public void StopCoroutine(IEnumerator routine) {
			runner.StopCoroutine (routine);
		}
	}

	public class CoroutineRunnerBehaviour : MonoBehaviour {
	}
}
