using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class OpenGameAnimation : MonoBehaviour {
	
	private Animation anim;
	 
	private bool done;
 	// Use this for initialization
	void Start(){
		anim = GetComponent<Animation> ();
		done = true;

	}
	
	// Update is called once per frame
	void Update () {
		//this.transform.localScale += new Vector3 (0.1f,0.1f,0.1f);
		if (anim.isPlaying == false && done)
		{
			gameObject.SetActive (false);
			done = false;
		}
			
	}
}
