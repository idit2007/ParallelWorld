using UnityEngine;
using System.Collections;
using PlayBento;
public class StageManagement : MonoBehaviour {
	public GameObject[] star;  					 //Array's star image
	private GameObject stage;    				 //Current stage
	private GameObject nStage; 				     //Next stage.
	public  int numberStage;  				     //Number of current stage
	//Use this for config
	private ScoreProfile sp;                     
	private ScoreData sd;
	public ScoreData psd; 
	private bool done;   
	private int[] starScore;
	//Check situation that finish.
	// Use this for initialization

	void Start () 
	{
			PB.Init ();
			sp = Local.GetProfile (typeof(ScoreProfile)) as ScoreProfile;
		star = new GameObject[3];
		stage = this.gameObject;
		numberStage = int.Parse(this.gameObject.name);
		nStage = GameObject.Find ((numberStage+1).ToString());
			psd = null;
			done = true;
			if (numberStage <= sp.ScoreList.Count)
			{
				sd = sp.ScoreList [numberStage - 1];    //Index stage
			}
		star[0]=this.transform.Find ("Star1").gameObject;
		star[1]=this.transform.Find ("Star2").gameObject;
		star[2]=this.transform.Find ("Star3").gameObject;

	}
	// Update is called once per frame
	void Update ()
	{
		
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
		         //Set number of stars in current stage.
				if(sd!=null)
				{

			       if (sd.Score > ScoreManageMent.Instance.starScore [0])
			        {
						star [0].SetActive (true);
						nStage.SetActive (true);
					}
			       if (sd.Score >  ScoreManageMent.Instance.starScore [1])
						star [1].SetActive (true);
			       if (sd.Score > ScoreManageMent.Instance.starScore [2])
						star [2].SetActive (true);


				}
				
					done = false;

	}


	}


