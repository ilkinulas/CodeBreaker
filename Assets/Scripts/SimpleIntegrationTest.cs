using UnityEngine;
using System.Collections;

[IntegrationTest.DynamicTestAttribute("TestScene")]
[IntegrationTest.TimeoutAttribute(5)]
public class SimpleIntegrationTest : MonoBehaviour {
	
	protected virtual void Start() {
		Debug.Log ("test started...");
		StartCoroutine (c1 ());
	}	


	private IEnumerator c1(float duration=1.0f) {
		float elapsedTime = 0;
		while (elapsedTime < duration) {
			elapsedTime += Time.deltaTime;
			Debug.Log ("c1 Elapsed time " + elapsedTime);
			yield return new WaitForEndOfFrame ();
		}
		yield return null;
	}
}
