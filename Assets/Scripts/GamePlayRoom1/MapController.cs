using UnityEngine;
using System.Collections;

public class MapController : MonoBehaviour {
	private Animator anim;
	// Use this for initialization
	void Start () {
		anim = GameObject.Find ("MapFeild").GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void MapButton()
	{
		if (anim.gameObject.tag == "Untagged") {
			anim.SetTrigger ("Big");
			anim.gameObject.tag = "Map";
		} 
		else 
		{
			anim.SetTrigger ("Small");
			anim.gameObject.tag = "Untagged";
		}

	}
}
