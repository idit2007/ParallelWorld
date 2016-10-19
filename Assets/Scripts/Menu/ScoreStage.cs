using UnityEngine;
using System.Collections;
using PlayBento;
public class ScoreStage : MonoBehaviour {
	private static ScoreStage instance;
	private ScoreProfile sp;
	public static ScoreStage Instance
	// Use this for initialization
	{
		get {


			return instance;
		}
	}

	void Awake()
	{
		instance = this;
	
	}
	void Start () {
		PB.Init ();
		sp = Local.GetProfile (typeof(ScoreProfile)) as ScoreProfile;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void GetScore(int gameStage )
	{
		ScoreData sd;
		sd = sp.ScoreList [gameStage-1];

	}
}
