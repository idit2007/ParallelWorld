using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakWall : MonoBehaviour {
	private Animator anim;
	private bool hitted;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		hitted = false;
	}
	

	void OnCollisionEnter(Collision coll) {


		if (coll.gameObject.tag == "BlueZombie"&&!hitted) {

			hitted = true;
			anim.SetTrigger ("hit");
		}
	}
}
