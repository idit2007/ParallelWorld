using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameLavelText : MonoBehaviour {
	public Text scoreText;
	// Use this for initialization
	void Start () {
		scoreText.text=TimeScore.currentStage.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
