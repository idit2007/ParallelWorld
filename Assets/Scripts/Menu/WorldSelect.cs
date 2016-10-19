using UnityEngine;
using System.Collections;
using UnityEngine.UI; 
public class WorldSelect : MonoBehaviour {
	public GameObject thisWorld;
	public GameObject BG;
	bool exit;
	public bool done;
	public Animator anim;
	// Use this for initialization
	void Start ()
	{
		//BG.SetActive (false);
		//anim.SetBool ("Exit",false);
		anim.SetBool ("ClickBG",true);

	}
	// Update is called once per frame
	public void Exit()
	{
		
		anim.SetBool ("ClickBG",false);
		anim.SetBool ("Exit",true);

	}


}
