using UnityEngine;
using System.Collections;
using PlayBento;

public class Shared : MonoBehaviour {
	private void SharedCallBack(bool success)
	{
		Debug.Log ("Success ="+success);
	}
	public void SharedButton(){
		PB.Init ();
		aftherInit ();
	}
	public void aftherInit(){
		PlayBento.Social.PostRecommend (SharedCallBack);
		//PlayBento.Social.PostRecommend (SharedCallBack);
		//PlayBento.Social.PostScore (3,SharedCallBack);
		//PlayBento.Social.PostAchivement ("th.co.progaming.proengine.firsttime",SharedCallBack);
	}

}
