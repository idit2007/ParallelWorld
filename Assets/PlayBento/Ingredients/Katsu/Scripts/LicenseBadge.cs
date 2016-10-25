using UnityEngine;
using System.Collections;

public class LicenseBadge : MonoBehaviour {

	void OnGUI()
	{
		GUI.depth = -1;
		GUIStyle style = new GUIStyle ();
		style.normal.textColor = Color.white;

		GUI.Box (new Rect(0, Screen.height - 60, 180, 60), "");
		GUI.Label (new Rect(10, Screen.height - 50, 200, 20), "Trial Version", style);
		GUI.Label (new Rect(10, Screen.height - 30, 200, 20), "Play Bento by ProGaming", style);
	}
}
