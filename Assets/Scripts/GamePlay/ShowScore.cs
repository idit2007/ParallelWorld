using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ShowScore : MonoBehaviour {
	public GameObject gem;
	public Text textScore;
	public float gameScore;
	int baseScore =1000;
	int scoreTimes=1000;
	int speedShowScore =1000000;
	private static ShowScore  instance;
	//use singleton.
	public static ShowScore  Instance
	{
		get {


			return instance;
		}
	}

	void Awake()
	{
		instance = this;
	}
	// Update is called once per frame
	void Update () {
		//Show score with text.

		if (gameScore < (gem.transform.position.y+baseScore)*scoreTimes) 
			gameScore += Time.deltaTime* speedShowScore;
		ScoreManageMent.Instance.intScore = (int)gameScore;
		ScoreManageMent.Instance.scoreValue.text =  (string.Format("{0:n0}",(int)gameScore));

	}
}
