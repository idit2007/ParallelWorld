using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PlayBento;
using UnityEngine.UI;
public class StageManagement : MonoBehaviour {
	private GameObject stage;    				 //Current stage
	private GameObject nStage; 				     //Next stage.
	public  int numberStage;  				     //Number of current stage
	//Use this for config
	private ScoreProfile sp;                     
	private ScoreData sd;
	public ScoreData psd; 
	private bool done;   
	private int[] starScore;
	public Sprite[]rankImage;
	public Image showRank; 
	//Check situation that finish.
	// Use this for initialization
	void Awake()
	{
		PB.Init ();
	}

	void Start () 
	{
		


			sp = Local.GetProfile (typeof(ScoreProfile)) as ScoreProfile;
		stage = this.gameObject;
		numberStage = int.Parse(this.gameObject.name);
		nStage = GameObject.Find ((numberStage+1).ToString());
			psd = null;
			done = true;
			if (numberStage <= sp.ScoreList.Count)
			{
				sd = sp.ScoreList [numberStage - 1];    //Index stage
			}
	

	}
	// Update is called once per frame
	void Update ()
	{
		this.transform.Rotate (0,Time.deltaTime*10,0);
				//Do this once time.
				if (done)
				{                                 
					//If it's first stage set it to active stage.
					if (numberStage == 1)              
					{
						this.gameObject.SetActive (true);
					}
					//If stage more than list set them to disble stage.
					else  if(numberStage>sp.ScoreList.Count)
					{
						this.gameObject.SetActive (false);
					}

				} 
				if(sd!=null)
				{

					if (sd.Score >0)
					{
						
						nStage.SetActive (true);
					}
				    
					if (sd.Score >60)
				      showRank.sprite = rankImage [2];
					else if(sd.Score >40)
				      showRank.sprite = rankImage [1];
			        else if(sd.Score >20)
			          showRank.sprite = rankImage [0];
				}
				         //Set number of stars in current stage.

					done = false;

	}


}
