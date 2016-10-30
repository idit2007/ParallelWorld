using UnityEngine;
using System.Collections;

public class EffectPlayerSlowMotion : MonoBehaviour {

	private GameObject slowEffect;
	public bool done1;
	public bool done2;

	// Use this for initialization
	void Start () {

		slowEffect = GameObject.Find ("EffectSlow");


		done1 = true;
		done2 = true;
	}
	
	// Update is called once per frame

	

}
