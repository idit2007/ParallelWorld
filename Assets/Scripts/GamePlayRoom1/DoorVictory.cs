using UnityEngine;
using System.Collections;

public class DoorVictory : MonoBehaviour {
	public Animator anim;
	public GameObject winPopup;
	public GameObject losePopup;
	void OnTriggerEnter(Collider coll) {

		if (coll.gameObject.tag == "Player") {

			anim.SetTrigger ("openDoor");
			if(winPopup!=null&&!losePopup.activeSelf)
			winPopup.SetActive (true);

		}
	}

}
