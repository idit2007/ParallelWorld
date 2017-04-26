using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackToModeLocationBase : MonoBehaviour {
	public GameObject Unlock;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void BlackToBaseLocation()
	{
		Application.LoadLevel("BaseLocationMode");

	}

	public void GoTo()
	{
		Application.LoadLevel("LoationBaseMap");

	}

	public void Black()
	{
		Unlock.SetActive (true);

	}

	public void PrePlay()
	{
		Unlock.SetActive (false);

	}
}
