using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxHit : MonoBehaviour {

	private bool hitted;
	private Rigidbody rgb;
	private Vector3 v3;
	// Use this for initialization
	void Start () {
		
		hitted = false;
		rgb = GetComponent<Rigidbody> ();
		v3 = new Vector3 (Random.Range(-10,10),Random.Range(-10,10),Random.Range(-10,10));
	}


	void OnCollisionEnter(Collision coll) {


		if (coll.gameObject.tag == "BlueZombie"&&!hitted) {

			hitted = true;
			rgb.AddForce (v3);
		}
	}
}
