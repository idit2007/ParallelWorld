using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Teleportion : MonoBehaviour {
	private float teleportRange;
	private bool world;
	private GameObject Player;
   //public GameObject PlayerDummy;

	private GameObject particleTeleportionStart;
	private GameObject particleTeleportionStop;
	private GameObject blueFlash;
	private GameObject explosionLight;
	private GameObject teleportButton;
	private GameObject minimapTpsWorld1;
	private GameObject minimapTpsWorld2;
	public Animator animWorld1;
	public Animator animWorld2;
	private RawImage mapWord1;
	public RawImage mapWord2;
	public Button teleportButtonC;
	private Button buttonWorld1;
	private Button buttonWorld2;
   void Start () {

		ScoreManageMent.numOfTeleport = 0;
		GameObject world1 = GameObject.Find ("World1");
		GameObject world2 = GameObject.Find ("World2");
		teleportRange = world2.transform.position.x - world1.transform.position.x;
		world = true;
		teleportButton = GameObject.Find ("TeleportButton");
	
		Player = GameObject.FindGameObjectWithTag("Player");
		particleTeleportionStart = GameObject.Find ("TeleportStart");
		particleTeleportionStop= GameObject.Find ("TeleportStop");
		blueFlash = GameObject.Find ("BlueFlash");
		explosionLight = GameObject.Find ("ExplosionLight");
		particleTeleportionStart.SetActive (false);
		particleTeleportionStop.SetActive (false);
		blueFlash.SetActive (false);

		teleportButtonC = teleportButton.GetComponent<Button>();

		//test

    }


	public void TeleportionCharacter()
	{
		StartCoroutine (TeleportAnimation());
		ScoreManageMent.numOfTeleport++;

	}

	IEnumerator TeleportAnimation()
	{

      
		teleportButtonC.interactable = false;
		particleTeleportionStart.SetActive (true);
		blueFlash.SetActive (true);
		yield return new WaitForSeconds (1.5f);
		blueFlash.SetActive (false);
		world = !world;
		if (!world) {

			TurnController.Instance.CurrentWorld = 2;
            Player.transform.position = new Vector3(Player.transform.position.x + teleportRange, Player.transform.position.y, Player.transform.position.z);




		
        } 
		else {

			TurnController.Instance.CurrentWorld = 1;
            Player.transform.position = new Vector3 (Player.transform.position.x - teleportRange, Player.transform.position.y, Player.transform.position.z);
            //PlayerDummy.transform.position = new Vector3(PlayerDummy.transform.position.x + teleportRange, PlayerDummy.transform.position.y, PlayerDummy.transform.position.z);


        }
		explosionLight.SetActive (false);
		particleTeleportionStop.SetActive (true);
		yield return new WaitForSeconds (4f);
		particleTeleportionStart.SetActive (false);
		particleTeleportionStop.SetActive (false);
		teleportButtonC.interactable = true;
	}

}
