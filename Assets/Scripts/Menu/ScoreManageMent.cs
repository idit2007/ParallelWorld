using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using PlayBento;
public class ScoreManageMent : MonoBehaviour {
	private static ScoreManageMent instance;
	public float score=0;                 //Score's vaule           
	public int intScore;
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
		PB.Init ();
	}
	void Start()
	{

		sp = Local.GetProfile (typeof(ScoreProfile)) as ScoreProfile;
	}
	  //Save score when player play finish.
	public void SaveScore(){
		Debug.Log ("In");
		Debug.Log (TimeScore.currentStage);
		ScoreData sd;
		intScore = 100-(int)TimeScore.playTime;
		if (TimeScore.currentStage <= sp.ScoreList.Count) {
			
			sd = sp.ScoreList [TimeScore.currentStage-1];
		} 
		else 
		{
			sd = new ScoreData ();
			sp.ScoreList.Add (sd);
		}
		if (intScore > sd.Score) {
			sd.time=TimeScore.playTime;
			sd.Score =intScore;
			sd.NumberStage = TimeScore.currentStage;
		
		}
		Local.SaveProfile ();

	}
	public void BacktoMap()
	{
		Application.LoadLevel ("Menu");

	}
	public void PlayAgain()
	{
		Application.LoadLevel ("P"+TimeScore.currentStage.ToString()+"V2");
	}
	public void NextStage()
	{
		TimeScore.currentStage++;
		Application.LoadLevel ("P"+TimeScore.currentStage.ToString()+"V2");
	}
}
