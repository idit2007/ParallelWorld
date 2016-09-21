using UnityEngine;
using System.Collections;

public class TurnController : MonoBehaviour {
	public bool playerMovemnet;
	private static TurnController instance;
	          
	//use singleton.
	public static TurnController Instance
	{
		get {
			return instance;
		}
	}
	void Awake()
	{

		instance = this;

	}
		
	// Update is called once per frame

}
