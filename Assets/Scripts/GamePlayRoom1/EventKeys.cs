using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EventKeys : MonoBehaviour {
	public Text popupText;
	public SphereCollider door1;
	public SphereCollider door2;
	public GameObject target;
	private GameObject popup;
	private GameObject blackPanel;

	void Start()
	{
		popup = GameObject.Find ("PopUp");
		blackPanel = GameObject.Find ("BlackPanel");
	}
	void OnTriggerEnter(Collider coll) {

		if (coll.gameObject.tag == "Player") {
			blackPanel.SetActive (true);
			if(target!=null)
			target.SetActive (true);
			popupText.text = "Now you get a key.";
			popup.SetActive (true);
			if(door1!=null)
			door1.enabled = true;
			if(door2!=null)
			door2.enabled = true;
			this.gameObject.SetActive (false);
		}
	}
}
