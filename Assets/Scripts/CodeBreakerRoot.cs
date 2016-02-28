using UnityEngine;
using System.Collections;
using strange.extensions.context.impl;

namespace net.peakgames.codebreaker {
	public class CodeBreakerRoot : ContextView {

		void Awake () {
			context = new CodeBreakerContext (this);
		}
	}
}
