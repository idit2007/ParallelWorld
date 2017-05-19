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
	private bool done;
	public static bool getBite;
	public static int  numOfTeleport;
	public GameObject achive1;
	public GameObject achive2;
	public GameObject prepareLoading;
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
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://parallelworld-1a50e.firebaseio.com/");

		// Get the root reference location of the database.
		mDatabaseRef = FirebaseDatabase.DefaultInstance.RootReference;
		QueryName ();
		instance = this;
		PB.Init ();
	}
	void Start()
	{
		done = false;
		email = FireBaseInIt.UserAccount;
		// Set up the Editor before calling into the realtime database.
	

		sp = Local.GetProfile (typeof(ScoreProfile)) as ScoreProfile;
	
	}
	void Update()
	{
		if (TimeScore.currentStage > 0 && done) {
			SaveScore ();
			done = false;
		} else if (TimeScore.currentStage == 0 && !done) {
			TutorialSaveScore ();
			done = true;
		}
	}
	  //Save score when player play finish.
	public void SaveScore(){
		prepareLoading.SetActive (false);
		ScoreData sd;
	     intScore = 100-(int)TimeScore.playTime;
		if (intScore < 10)
			intScore = 10;
		if (!getBite) {
			intScore += 100;
			achive1.SetActive (false);
		}
		if (numOfTeleport < 10) {
			intScore += 100;
			achive2.SetActive (false);
		}

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
		if(TimeScore.currentStage==10)
			SceneManager.LoadScene ("EnddingScene");
		else 
		SceneManager.LoadScene ("Stage"+TimeScore.currentStage.ToString());
	}
	public void QueryName()
	{
		int n=0,i=0;


		FirebaseDatabase.DefaultInstance
			.GetReference("User")
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
						Debug.Log("In");
						if(childSnapshot.Key.ToString()==email){
							username=childSnapshot.Child("Username").Value.ToString();
			
						}
						done=true;
					}

				}


			}

		};



	}
}
