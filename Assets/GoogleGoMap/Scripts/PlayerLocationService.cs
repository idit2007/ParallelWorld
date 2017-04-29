﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class PlayerLocationService : MonoBehaviour {

	public GeoPoint loc = new GeoPoint();
	//public GeoPoint locOld = new GeoPoint();
	[HideInInspector]
	public float trueHeading;
	public bool locServiceIsRunning = false;
	public int maxWait = 30; // seconds
	private float locationUpdateInterval = 0.2f; // seconds
	private double lastLocUpdate = 0.0; //seconds

	public Text locText;
	public Text locOldText;
	public Text at;

	public Animator Player;


	public void StartLocationService() {
		Debug.Log ("Player Loc started.");
		StartCoroutine (_StartLocationService ());
	}

	public IEnumerator _StartLocationService()
	{
		
		// First, check if user has location service enabled
		if (!Input.location.isEnabledByUser) {
			Debug.Log ("Locations is not enabled.");

			//NOTE: If location is not enabled, we initialize the postion of the player to somewhere in Los Angeles, just for demonstration purposes
			loc.setLatLon_deg (13.65174f, 100.4927f); 

			GameManager.Instance.playerStatus = GameManager.PlayerStatus.FreeFromDevice;
			// To get the game run on Editor without location services
			locServiceIsRunning = true;
			yield break;
		}

		// Start service before querying location
		Input.location.Start();
		//locOld = 
		// Wait until service initializes
		while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
		{
			yield return new WaitForSeconds(1);
			maxWait--;
		}

		// Service didn't initialize in maxWait seconds
		if (maxWait < 1)
		{
			print("Locations services timed out");
			yield break;
		}

		// Connection has failed
		if (Input.location.status == LocationServiceStatus.Failed)
		{
			print("Location services failed");
			yield break;
		} else if (Input.location.status == LocationServiceStatus.Running){
			GameManager.Instance.playerStatus = GameManager.PlayerStatus.TiedToDevice;
			loc.setLatLon_deg (Input.location.lastData.latitude, Input.location.lastData.longitude);
			//locOld.setLatLon_deg (Input.location.lastData.latitude, Input.location.lastData.longitude);
			Debug.Log ("Location: " + Input.location.lastData.latitude.ToString ("R") + " " + Input.location.lastData.longitude.ToString ("R") + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
			locServiceIsRunning = true;
			lastLocUpdate = Input.location.lastData.timestamp;

		} else {
			print ("Unknown Error!");
		}
		Debug.Log (loc.ToString());
	}

	public IEnumerator RunLocationService()
	{
		double lastLocUpdate = 0.0;
		//int  cout=0;
		//int  NoRun=0;
		while (true) {
			if (lastLocUpdate != Input.location.lastData.timestamp) {
				Player.SetBool ("Run", true);
				//at.text="Update";
				//cout=cout+1;
				loc.setLatLon_deg (Input.location.lastData.latitude, Input.location.lastData.longitude);
				/*
				locText.text="Loc: " +loc.lat_d.ToString()+ " "+loc.lon_d .ToString();
				locOldText.text="LocOld: " +locOld.lat_d.ToString()+ " "+locOld.lon_d .ToString();

				if (loc.lat_d == locOld.lat_d && loc.lon_d == locOld.lon_d) {
					Player.SetBool ("Run",false);
					NoRun=NoRun+1;
				} 
				else 
				{
					Player.SetBool ("Run",true);
				}
				locOld.setLatLon_deg (Input.location.lastData.latitude, Input.location.lastData.longitude);*/
				trueHeading = Input.compass.trueHeading;
				Debug.Log ("Location: " + Input.location.lastData.latitude.ToString ("R") + " " + Input.location.lastData.longitude.ToString ("R") + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
				//locServiceIsRunning = true;
				lastLocUpdate = Input.location.lastData.timestamp;

			} 
			else 
			{
				Player.SetBool ("Run",false);
			}

			//at.text="Wait "+cout.ToString()+"NoRun "+NoRun.ToString();
			yield return new WaitForSeconds(locationUpdateInterval);
		}
	}
}