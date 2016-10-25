using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using PlayBento;
public class ScoreManageMent : MonoBehaviour {
	private static ScoreManageMent instance;
	public Text scoreValue;               //Score's text.
	public float score=0;                 //Score's vaule           
	public int intScore;
	public int[] starScore;
	private ScoreProfile sp;

	//Singleton
	public static ScoreManageMent Instance
	{
		get {

			return instance;
		}
	}

	void Awake()
	{
		instance = this;

	}
	void Start()
	{
		starScore = new int[3];
		starScore [0] = 1000000;
		starScore [1] = 1010000;
		starScore [2] = 1100000;
		PB.Init ();
		sp = Local.GetProfile (typeof(ScoreProfile)) as ScoreProfile;
	}
	  //Save score when player play finish.
	public void SaveScore(){
		ScoreData sd;
		if (CurrentStage.nowStage <= sp.ScoreList.Count) {
			
			sd = sp.ScoreList [CurrentStage.nowStage- 1];
		} else {
			sd = new ScoreData ();

			sp.ScoreList.Add (sd);
		}
		if (intScore > sd.Score) {
			sd.Score =intScore;
			sd.NumberStage = CurrentStage.nowStage;
			sd.height = intScore;
			sd.Stars = 2;
		}
		Local.SaveProfile ();

	}



}
