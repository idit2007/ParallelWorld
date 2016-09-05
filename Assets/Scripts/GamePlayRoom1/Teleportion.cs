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
	private GameObject explosionLight;
	private GameObject teleportButton;
	// Use this for initialization
	void Start () {
		world = true;
		teleportButton = GameObject.Find ("TeleportionButton");
	
		Player = GameObject.FindGameObjectWithTag("Player");
		CameraSet = GameObject.Find ("CameraSet");
		CameraSet.SetActive (false);
		particleTeleportionStart = GameObject.Find ("TeleportStart");
		particleTeleportionStop= GameObject.Find ("TeleportStop");
		blueFlash = GameObject.Find ("BlueFlash");
		explosionLight = GameObject.Find ("ExplosionLight");
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
		Button teleportButtonC = teleportButton.GetComponent<Button>();
		teleportButtonC.interactable = false;
		particleTeleportionStart.SetActive (true);
		blueFlash.SetActive (true);
		yield return new WaitForSeconds (2f);

		world = !world;
		if (!world) {
			Player.transform.position = new Vector3 (Player.transform.position.x + teleportRange, Player.transform.position.y, Player.transform.position.z);
			CameraSet.SetActive (true);
		} 
		else {
			Player.transform.position = new Vector3 (Player.transform.position.x - teleportRange, Player.transform.position.y, Player.transform.position.z);
			CameraSet.SetActive (false);
		}
		blueFlash.SetActive (false);
		explosionLight.SetActive (false);
		particleTeleportionStop.SetActive (true);
		yield return new WaitForSeconds (4f);
		particleTeleportionStart.SetActive (false);
		particleTeleportionStop.SetActive (false);
		teleportButtonC.interactable = true;
	}
}
