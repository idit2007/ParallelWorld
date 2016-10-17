using UnityEngine;
using System.Collections;

using OnePF;

public class OpenIABDemoUI : MonoBehaviour {

	void OnGUI()
	{
		// Puts some basic buttons onto the screen.
		GUI.skin.button.fontSize = (int)(0.05f * Screen.height);

		Rect rec1 = new Rect (0.1f * Screen.width, 0.05f * Screen.height,
                                  0.8f * Screen.width, 0.1f * Screen.height);
		if (GUI.Button (rec1, "Query Inventory")) {
			OpenIAB.queryInventory ();
		}

		Rect rect2 = new Rect (0.1f * Screen.width, 0.175f * Screen.height,
                               0.8f * Screen.width, 0.1f * Screen.height);
		if (GUI.Button (rect2, "Purchase Sample1")) {
			OpenIAB.purchaseProduct("th.co.progaming.playbento.sample1");
		}
	}
}
