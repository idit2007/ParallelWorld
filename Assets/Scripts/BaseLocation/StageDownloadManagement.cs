using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayBento;

public class StageDownloadManagement : MonoBehaviour {
	private GameObject stage;    				 //Current stage
	public GameObject StageLock; 
	public GameObject Unlock; 
	public Text StageName; 
	public  int numberStage;  				     //Number of current stage
	//Use this for config
	private DownlodeProfile sp;                     
	private DownlodeData sd;
	public DownlodeData psd; 
   
	private bool found;  
	//Check situation that finish.
	// Use this for initialization


	void Start () 
	{

		sp = Local.GetProfile (typeof(DownlodeProfile)) as DownlodeProfile;
		stage = this.gameObject;
		numberStage = int.Parse(this.gameObject.name);



		if (sp.DownlodeList.Count > 0)
		{
			for (int i = 0; i < sp.DownlodeList.Count; i++) 
			{
				if (numberStage == sp.DownlodeList [i].NumberStage)
				{
					sd = sp.DownlodeList [i];
				}
					
			}
				
		}
			


	}
	// Update is called once per frame
	void Update ()
	{


		if (sd != null) {
			StageLock.SetActive (false);
		} 
		else 
		{
			StageLock.SetActive (true);
		}
		//Set number of stars in current stage.

		if (!Unlock.activeSelf)
		{
			TimeScore.currentStage=numberStage;
			Debug.Log("numberStage"+numberStage);
		}


	}


	public void UnlockMap(){
		
		sd = new DownlodeData ();
		sd.NumberStage = numberStage;
		sd.NameStage = StageName.text;

		sp.DownlodeList.Add (sd);


		Local.SaveProfile ();
		StageLock.SetActive (false);

		for (int i = 0; i < sp.DownlodeList.Count; i++) 
		{
			if (numberStage == sp.DownlodeList [i].NumberStage)
			{
				sd = sp.DownlodeList [i];
			}

		}
	}
}
