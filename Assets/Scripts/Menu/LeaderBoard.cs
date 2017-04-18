using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using PlayBento;
using UnityEngine.UI;
public class LeaderBoard : MonoBehaviour {
	ArrayList leaderBoard;
	public Text[]rankScoreText;
	public Text[]rankTimeText;
	public Text[]rankUserText;
	public string[]bufferRankScoreText;
	private string[]bufferRankTimeText;
	private string[]bufferRankUserText;
	private const int MaxScores = 5;
	public static string pressStage;
	// Use this for initialization
	void Start () 
	{

		bufferRankScoreText=new string[5];
		bufferRankTimeText=new string[5];
		bufferRankUserText=new string[5];
			FirebaseApp app = FirebaseApp.DefaultInstance;
			app.SetEditorDatabaseUrl("https://parallelworld-1a50e.firebaseio.com/");
			if (app.Options.DatabaseUrl != null) app.SetEditorDatabaseUrl(app.Options.DatabaseUrl);

			leaderBoard = new ArrayList();
			leaderBoard.Add("Firebase Top " + MaxScores.ToString() + " Scores");
			
		}
	// Update is called once per frame
	public void QueryLeaderBoard()
	{
		int n=0,i=0;
		Debug.Log ("pressStage= "+pressStage);
		FirebaseDatabase.DefaultInstance
			.GetReference(pressStage).OrderByChild("Score")
			.ValueChanged += (object sender2, ValueChangedEventArgs e2) => {
			if (e2.DatabaseError != null) {
				Debug.LogError(e2.DatabaseError.Message);
				return;
			}
			string title = leaderBoard[0].ToString();
			leaderBoard.Clear();
			leaderBoard.Add(title);

			if (e2.Snapshot != null && e2.Snapshot.ChildrenCount > 0) {
				foreach (var childSnapshot in e2.Snapshot.Children) {
					if (childSnapshot.Child("Score") == null
						|| childSnapshot.Child("Score").Value == null) {
						Debug.LogError("Bad data in sample.  Did you forget to call SetEditorDatabaseUrl with your project id?");
						break;
					} else {
						
				
						bufferRankScoreText[n]="Score: "+childSnapshot.Child("Score").Value.ToString();
						bufferRankTimeText[n]="Time: "+childSnapshot.Child("Time").Value.ToString();
						bufferRankUserText[n]=childSnapshot.Key.ToString();
						n++;
						Debug.Log(bufferRankScoreText[n]);
					}

				}
				int j=0;
				for(i=n-1;i>=0;i--){
					Debug.Log("now here");
					rankScoreText[j].text=bufferRankScoreText[i];
					rankTimeText[j].text=bufferRankTimeText[i];
					rankUserText[j].text=bufferRankUserText[i];
					j++;
				}
			}

		};

	

	}
	public void ClearBoard()
	{
		for(int i=0;i<5;i++){
			Debug.Log("now here");
			rankScoreText[i].text="Score: -";
			rankTimeText[i].text="TIme: -";
			rankUserText[i].text="";
		}
	}
}
