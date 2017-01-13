using UnityEngine;
using System.Collections;

public class DoorVictory : MonoBehaviour {
	public Animator anim;
	public GameObject winPopup;

	void OnTriggerEnter(Collider coll) {

		if (coll.gameObject.tag == "Player") {

			anim.SetTrigger ("openDoor");
			if(winPopup!=null)
			winPopup.SetActive (true);

		}
	}

}
