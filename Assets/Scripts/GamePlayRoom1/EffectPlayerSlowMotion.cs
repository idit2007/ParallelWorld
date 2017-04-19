using UnityEngine;
using System.Collections;

public class EffectPlayerSlowMotion : MonoBehaviour {
	private static EffectPlayerSlowMotion instance;
	private GameObject slowEffect;
	public bool done1;
	public bool done2;
	public bool playerMove=false;
	public static EffectPlayerSlowMotion Instance
	{
		get {
			return instance;
		}
	}
	void Awake()
	{

		instance = this;


	}
	// Use this for initialization
	void Start () {

		slowEffect = GameObject.Find ("EffectSlow");


		done1 = true;
		done2 = true;
	}
	
	// Update is called once per frame

	

}
