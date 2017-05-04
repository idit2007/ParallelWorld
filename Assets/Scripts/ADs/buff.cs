using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buff : MonoBehaviour {
	
	public GameObject ButtonBuff;
	// Use this for initialization
	void Start () {
		BuffStatus.buffStatus=0;

	}
	
	// Update is called once per frame
	void Update () {
		if(BuffStatus.buffStatus==0)
			ButtonBuff.SetActive(false);
		else
			ButtonBuff.SetActive(true);

		Debug.Log(BuffStatus.buffStatus);

	}


	public void playTest()
	{
		Application.LoadLevel("Stage1");
	}
}
