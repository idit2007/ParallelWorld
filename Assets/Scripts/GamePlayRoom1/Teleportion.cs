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
		explosionLight.SetActive (false);
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
        selectpoint.removeway = true;
        Button teleportButtonC = teleportButton.GetComponent<Button>();
		teleportButtonC.interactable = false;
		particleTeleportionStart.SetActive (true);
		blueFlash.SetActive (true);
		yield return new WaitForSeconds (2f);
		blueFlash.SetActive (false);
		yield return new WaitForSeconds (0.1f);

		world = !world;
		if (!world) {
            selectpoint.removeway = false;
            Player.transform.position = new Vector3 (Player.transform.position.x + teleportRange, Player.transform.position.y, Player.transform.position.z);
			CameraSet.SetActive (true);
            Unit.World = 1;
            if (Unit.haveway == true)
                selectpoint.removeway = true;
        } 
		else {
            selectpoint.removeway = false;
            Player.transform.position = new Vector3 (Player.transform.position.x - teleportRange, Player.transform.position.y, Player.transform.position.z);
			CameraSet.SetActive (false);
            Unit.World = 0;
            if(Unit.haveway==true)
                selectpoint.removeway = true;

        }
		explosionLight.SetActive (false);
		particleTeleportionStop.SetActive (true);
		yield return new WaitForSeconds (4f);
		particleTeleportionStart.SetActive (false);
		particleTeleportionStop.SetActive (false);
		teleportButtonC.interactable = true;
	}
}
