﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectMode : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Story()
	{
		Application.LoadLevel("Menu");

	}
	public void BaseLocation()
	{
		Application.LoadLevel("BaseLocationMode");

	}
}