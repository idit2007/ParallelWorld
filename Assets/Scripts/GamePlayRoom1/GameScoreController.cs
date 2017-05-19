using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
public class GameScoreController : MonoBehaviour {
	
	private  float score;
	public Text scoreText;

	// Use this for initialization
	void Start () {

		score = 0;

	}

	// Update is called once per frame
	void Update () {
		if (score < ScoreManageMent.Instance.intScore)
			score += Time.deltaTime*100;
		else
			score = ScoreManageMent.Instance.intScore;
		scoreText.text = "Score: " + (string.Format ("{0:n0}", (int)score));
	}

}
