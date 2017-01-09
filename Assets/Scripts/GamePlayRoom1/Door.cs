using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {
	public GameObject winPopup;
	GameObject thedoor;
	// Use this for initialization
	void Start () {
		thedoor= GameObject.Find("door");
	}

	void OnTriggerEnter(Collider coll) {

		if (coll.gameObject.tag == "Player") {
			
			thedoor.GetComponent<Animation>().Play("open");
		winPopup.SetActive (true);

		}
	}
}
