using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScrollViewController : MonoBehaviour {

	[SerializeField]
	private ScrollRect scrollRect;

	[SerializeField]
	private RectTransform scrollContent;

	void Start () {
		scrollRect.verticalNormalizedPosition = 0f;
	}
	
	public void UpdateScrollPosition() {
		scrollRect.verticalNormalizedPosition = 0f;
	}
}
