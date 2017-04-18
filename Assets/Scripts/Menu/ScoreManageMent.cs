using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using PlayBento;
public class ScoreManageMent : MonoBehaviour {
	private static ScoreManageMent instance;
	public float score=0;                 //Score's vaule           
	public int intScore;
	private ScoreProfile sp;
	private string email = "test";
	public string Username;
	DatabaseReference mDatabaseRef;
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
		email = FireBaseInIt.UserAccount;
		// Set up the Editor before calling into the realtime database.
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://parallelworld-1a50e.firebaseio.com/");

		// Get the root reference location of the database.
		mDatabaseRef = FirebaseDatabase.DefaultInstance.RootReference;
		sp = Local.GetProfile (typeof(ScoreProfile)) as ScoreProfile;
		SaveScore ();

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
			mDatabaseRef.Child("Stage"+TimeScore.currentStage.ToString()).Child(email).Child("Score").SetValueAsync(intScore);
			mDatabaseRef.Child("Stage"+TimeScore.currentStage.ToString()).Child(email).Child("Time").SetValueAsync((int)TimeScore.playTime);
		}
		if (intScore > sd.Score) {
			sd.time=TimeScore.playTime;
			sd.Score =intScore;
			sd.NumberStage = TimeScore.currentStage;
			mDatabaseRef.Child("Stage"+TimeScore.currentStage.ToString()).Child(email).Child("Score").SetValueAsync(intScore);
			mDatabaseRef.Child("Stage"+TimeScore.currentStage.ToString()).Child(email).Child("Time").SetValueAsync((int)TimeScore.playTime);
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
