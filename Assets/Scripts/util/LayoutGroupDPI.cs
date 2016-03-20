using UnityEngine;
using UnityEngine.UI;
namespace net.peakgames.codebreaker.util {

	[RequireComponent (typeof(HorizontalOrVerticalLayoutGroup))]
	public class LayoutGroupDPI : MonoBehaviour {

		void Start () {
			HorizontalOrVerticalLayoutGroup layoutGroup = GetComponent<HorizontalOrVerticalLayoutGroup> ();
			RectOffset padding = layoutGroup.padding;
			layoutGroup.padding.bottom = (int) GameUtils.ToDP (padding.bottom);
			layoutGroup.padding.left = (int) GameUtils.ToDP (padding.left);
			layoutGroup.padding.right = (int) GameUtils.ToDP (padding.right);
			layoutGroup.padding.top = (int) GameUtils.ToDP (padding.top);
			layoutGroup.spacing= (int) GameUtils.ToDP (layoutGroup.spacing);
		}

	}
}
