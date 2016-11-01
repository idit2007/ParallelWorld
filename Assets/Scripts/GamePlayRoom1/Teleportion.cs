using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Teleportion : MonoBehaviour {
	private float teleportRange;
	private bool world;
	private GameObject Player;
   public GameObject PlayerDummy;

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
    public static Button TeleportButtonStatic;
	private Button buttonWorld1;
	private Button buttonWorld2;
    // Use this for initialization


	//test
	private GameObject caM1;
    void Start () {

//		mapWord1 = minimapTpsWorld1.GetComponent<RawImage> ();
//		mapWord2 = minimapTpsWorld2.GetComponent<RawImage> ();
		GameObject world1 = GameObject.Find ("World1");
		GameObject world2 = GameObject.Find ("World2");
		teleportRange = world2.transform.position.x - world1.transform.position.x;
		world = true;
		teleportButton = GameObject.Find ("TeleportionButton");
	
		Player = GameObject.FindGameObjectWithTag("Player");
        PlayerDummy = GameObject.FindGameObjectWithTag("PlayerDummy");
		particleTeleportionStart = GameObject.Find ("TeleportStart");
		particleTeleportionStop= GameObject.Find ("TeleportStop");
		blueFlash = GameObject.Find ("BlueFlash");
		explosionLight = GameObject.Find ("ExplosionLight");
		particleTeleportionStart.SetActive (false);
		particleTeleportionStop.SetActive (false);
		blueFlash.SetActive (false);
        TeleportButtonStatic = GameObject.Find("TeleportionButton").GetComponent<Button>();



		//test
		caM1=GameObject.Find("Camera");

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
		//	mapWord2.enabled = true;
		//	mapWord1.enabled = false;
			TurnController.Instance.CurrentWorld = 2;
            selectpoint.removeway = false;
            Player.transform.position = new Vector3 (Player.transform.position.x + teleportRange, Player.transform.position.y, Player.transform.position.z);
            PlayerDummy.transform.position = new Vector3(PlayerDummy.transform.position.x - teleportRange, PlayerDummy.transform.position.y, PlayerDummy.transform.position.z);
            Unit.World = 1;
            //Unit.DrawLineStatic[0].SetActive(false);
          //  Unit.DrawLineStatic[1].SetActive(true);



			//test

		
        } 
		else {
		//	mapWord2.enabled = false;
		//	mapWord1.enabled = true;
			TurnController.Instance.CurrentWorld = 1;
            selectpoint.removeway = false;
            Player.transform.position = new Vector3 (Player.transform.position.x - teleportRange, Player.transform.position.y, Player.transform.position.z);
            PlayerDummy.transform.position = new Vector3(PlayerDummy.transform.position.x + teleportRange, PlayerDummy.transform.position.y, PlayerDummy.transform.position.z);
            Unit.World = 0;
//            Unit.DrawLineStatic[0].SetActive(true);
     //       Unit.DrawLineStatic[1].SetActive(false);



			//test
			caM1.SetActive(true);

        }
		explosionLight.SetActive (false);
		particleTeleportionStop.SetActive (true);
		yield return new WaitForSeconds (4f);
		particleTeleportionStart.SetActive (false);
		particleTeleportionStop.SetActive (false);
		teleportButtonC.interactable = true;
	}

}
