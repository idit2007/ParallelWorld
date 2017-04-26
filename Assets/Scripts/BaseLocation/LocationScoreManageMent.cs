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
using UnityEngine.SceneManagement;

public class LocationScoreManageMent : MonoBehaviour {
	private static LocationScoreManageMent instance;
	public float score=0;                 //Score's vaule           
	public int intScore;
	private DownlodeProfile sp;
	private DownlodeData sd;

	private string email = "test";
	public string Username;
	DatabaseReference mDatabaseRef;

	public Text NameStage;
	//Singleton
	public static LocationScoreManageMent Instance
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
		sp = Local.GetProfile (typeof(DownlodeProfile)) as DownlodeProfile;

		if (sp.DownlodeList.Count > 0) {
			for (int i = 0; i < sp.DownlodeList.Count; i++) {
				if (TimeScore.currentStage == sp.DownlodeList [i].NumberStage) {
					sd = sp.DownlodeList [i];
					Debug.Log("currentStage "+TimeScore.playTime);
				}

			}
			if (sd!=null) {
				NameStage.text=sd.NameStage;
			} 

		}



		intScore = 100-(int)TimeScore.playTime;
	}
	//Save score when player play finish.
	public void SaveScore(){
		


		if (sd!=null) {

		} 
		else 
		{
			sd = new DownlodeData ();
			sp.DownlodeList.Add (sd);

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
		SceneManager.LoadScene ("BaseLocationPlay");
		//Application.LoadLevel ("BaseLocationPlay");

	}
	public void PlayAgain()
	{
		SceneManager.LoadScene ("LoationBaseMap");
		//Application.LoadLevel ("LoationBaseMap");
	}

}
