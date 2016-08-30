using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Teleportion : MonoBehaviour {
	private int teleportRange=47;
	private bool world;
	private GameObject Player;
	private GameObject CameraSet;
	private GameObject particleTeleportionStart;
	private GameObject particleTeleportionStop;
	private GameObject blueFlash;
	// Use this for initialization
	void Start () {
		world = true;
		Player = GameObject.FindGameObjectWithTag("Player");
		CameraSet = GameObject.Find ("CameraSet");
		CameraSet.SetActive (false);
		particleTeleportionStart = GameObject.Find ("TeleportStart");
		particleTeleportionStop= GameObject.Find ("TeleportStop");
		blueFlash = GameObject.Find ("BlueFlash");
		particleTeleportionStart.SetActive (false);
		particleTeleportionStop.SetActive (false);
		blueFlash.SetActive (false);
	}

	// Update is called once per frame
	void Update () {
		
	}
	public void TeleportionCharacter()
	{
		StartCoroutine (TeleportAnimation());

	}
	IEnumerator TeleportAnimation()
	{
		particleTeleportionStart.SetActive (true);
		yield return new WaitForSeconds (2f);
		blueFlash.SetActive (true);
		yield return new WaitForSeconds (0.1f);
		blueFlash.SetActive (false);
		world = !world;
		if (!world) {
			Player.transform.position = new Vector3 (Player.transform.position.x + teleportRange, Player.transform.position.y, Player.transform.position.z);
			CameraSet.SetActive (true);
		} 
		else {
			Player.transform.position = new Vector3 (Player.transform.position.x - teleportRange, Player.transform.position.y, Player.transform.position.z);
			CameraSet.SetActive (false);
		}
		particleTeleportionStop.SetActive (true);
		yield return new WaitForSeconds (4f);
	
		particleTeleportionStart.SetActive (false);
		particleTeleportionStop.SetActive (false);
	}
}
