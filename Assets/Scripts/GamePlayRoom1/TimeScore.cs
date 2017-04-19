using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TimeScore : MonoBehaviour {

	// Use this for initialization
	private static TimeScore instance;
	public static float playTime;
	public static int currentStage=0;
	public bool gameStart;
	public Text timeScoreText;
	//use singleton.
	public static TimeScore Instance
	{
		get {
			return instance;
		}
	}
	void Awake()
	{
		playTime = 0;
		instance = this;
		gameStart = false;

	}
	//Set active/unactive star  follow score.
	void Update()
	{    
		//Debug.Log (currentStage);
		timeScoreText.text= "Time: "+(string.Format("{0:0.00}",playTime));
		if (gameStart)
			playTime += Time.deltaTime;
	}                                                                  
	public void GameStart()
	{
		gameStart = true;
	}
}
