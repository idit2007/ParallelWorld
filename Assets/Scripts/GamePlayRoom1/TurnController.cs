using UnityEngine;
using System.Collections;

public class TurnController : MonoBehaviour {
	public bool playerMovemnet;
	public int CurrentWorld;
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
		CurrentWorld = 1;
		instance = this;

	}
		
	// Update is called once per frame

}
