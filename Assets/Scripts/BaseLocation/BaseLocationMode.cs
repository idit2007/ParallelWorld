using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseLocationMode : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void BaseLocationPlay()
	{
		Application.LoadLevel("BaseLocationPlay");

	}
	public void BaseLocationPlace()
	{
		Application.LoadLevel("BaseLocation");
	}
	public void BlackToSelectMode()
	{
		Application.LoadLevel("SelectMode");
	}
}
