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
public class ScoreManageMent : MonoBehaviour {
	private static ScoreManageMent instance;
	public float score=0;                 //Score's vaule           
	public int intScore;
	private ScoreProfile sp;
	private string email = "test";
	private string username="test";
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
		QueryName ();
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
		if (TimeScore.currentStage > 0)
			SaveScore ();
		else if (TimeScore.currentStage == 0)
			TutorialSaveScore ();
	}

	  //Save score when player play finish.
	public void SaveScore(){
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
			mDatabaseRef.Child("Stage"+TimeScore.currentStage.ToString()).Child(email).Child("Username").SetValueAsync(username);
		}

		if (intScore > sd.Score) {
			sd.time=TimeScore.playTime;
			sd.Score =intScore;
			sd.NumberStage = TimeScore.currentStage;
			mDatabaseRef.Child("Stage"+TimeScore.currentStage.ToString()).Child(email).Child("Score").SetValueAsync(intScore);
			mDatabaseRef.Child("Stage"+TimeScore.currentStage.ToString()).Child(email).Child("Time").SetValueAsync((int)TimeScore.playTime);
			mDatabaseRef.Child("Stage"+TimeScore.currentStage.ToString()).Child(email).Child("Username").SetValueAsync(username);

		}
		Local.SaveProfile ();

	}
	public void TutorialSaveScore(){

		ScoreData sd;
		if (sp.ScoreList.Count == 0) {
			sd = new ScoreData ();
			sp.ScoreList.Add (sd);
			sd.time = 0;
			sd.Score = 0;
			sd.NumberStage = 1;
	
			Local.SaveProfile ();
		}
	}

	public void BacktoMap()
	{
		SceneManager.LoadScene ("Menu");

	}
	public void PlayAgain()
	{
		SceneManager.LoadScene ("Stage"+TimeScore.currentStage.ToString());
	}
	public void NextStage()
	{
		TimeScore.currentStage++;
		SceneManager.LoadScene ("Stage"+TimeScore.currentStage.ToString());
	}
	public void QueryName()
	{
		int n=0,i=0;

		mDatabaseRef.Child("User")
			.ValueChanged += (object sender2, ValueChangedEventArgs e2) => {
			if (e2.DatabaseError != null) {
				Debug.LogError(e2.DatabaseError.Message);
				return;
			}


			if (e2.Snapshot != null && e2.Snapshot.ChildrenCount > 0) {
				foreach (var childSnapshot in e2.Snapshot.Children) {
					if (childSnapshot.Child("Username") == null
						|| childSnapshot.Child("Username").Value == null) {
						Debug.LogError("Bad data in sample.  Did you forget to call SetEditorDatabaseUrl with your project id?");
						break;
					} else {
						if(childSnapshot.Key.ToString()==email){
							username=childSnapshot.Child("Username").Value.ToString();
						}
					}

				}


			}

		};



	}
}
