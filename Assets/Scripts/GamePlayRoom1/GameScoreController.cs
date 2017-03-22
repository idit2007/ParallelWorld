using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameScoreController : MonoBehaviour {
	private  float score;
	public Text scoreText;
	// Use this for initialization
	void Start () {
		score = 0;
	}

	// Update is called once per frame
	void Update () {
		if (score < TimeScore.playTime)
			score += Time.deltaTime*10;
		else
			score = TimeScore.playTime;
		scoreText.text = "Score: " + (string.Format ("{0:n0}", (int)score));
	}
}
