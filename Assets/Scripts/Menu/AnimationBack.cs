using UnityEngine;
using System.Collections;

public class AnimationBack : MonoBehaviour {
	public GameObject thisWorldUI;
	public GameObject thisWorld;
	public GameObject mapWorld;
	// Use this for initialization
	public void OpenMap()
	{
		mapWorld.SetActive (true);
	}
	public void CloseMap()
	{
		mapWorld.SetActive (false);
	}
	public void CloseWorld()   //  open WorldUI and close  World that full screen.
	{
		
		thisWorld.SetActive (false);
		thisWorldUI.SetActive (true);

	}
}
