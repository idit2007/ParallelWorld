using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventHelp : MonoBehaviour {

	public Text popupText;
	public SphereCollider door1;
	public SphereCollider door2;
	public GameObject target1;
	public GameObject target2;
	private GameObject popup;
	private GameObject blackPanel;
	private Collider collKey;
	void Start()
	{
		popup = GameObject.Find ("PopUp");
		blackPanel = GameObject.Find ("BlackPanel");
		collKey = GetComponent<Collider> ();
	}
	void OnTriggerEnter(Collider coll) {

		if (coll.gameObject.tag == "Player") {
			blackPanel.SetActive (true);
			popupText.text = "people this say....";
			popup.SetActive (true);
			if(door1!=null)
			door1.enabled = true;
			if(door2!=null)
			door2.enabled = true;
			if(target1!=null)
			target1.SetActive (true);
			if(target2!=null)
			target2.SetActive (true);
			collKey.enabled = false;
			this.gameObject.SetActive (false);
		}
	}
}
