using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class CameraManagement : MonoBehaviour {
	private GameObject topViewCameraWorld1;
	private GameObject topViewCameraWorld2;
	private GameObject thirdPersonCamera;
	private GameObject player;
	private GameObject uIFPS;
	private GameObject preFps;
	private GameObject changeShaderArea;
	private Vector3  defaultPosition;
	private GameObject gameObjectAnimWorld1;
	private GameObject gameObjectAnimWorld2;
	private GameObject allMaterialHoloWorld1;
	private GameObject allMaterialHoloWorld2;
	public Animator animWorld1;
	public Animator animWorld2;
	private RawImage mapWord1;
	private bool openGame;
	public RawImage mapWord2;
	public bool done1;
	public bool done2;
	public bool zoom;
	// Use this for initialization
	void Start () {
		Screen.orientation = ScreenOrientation.LandscapeLeft;
		zoom = false;
		allMaterialHoloWorld1 = GameObject.Find ("AllMaterialsHoloWorld1");
		allMaterialHoloWorld2 = GameObject.Find ("AllMaterialsHoloWorld2");
		topViewCameraWorld1 = GameObject.Find ("TopViewCameraWorld1");
		topViewCameraWorld2 = GameObject.Find ("TopViewCameraWorld2");
	      gameObjectAnimWorld1 = GameObject.Find ("mapfps1Button");
		gameObjectAnimWorld2 = GameObject.Find ("maptps2Button");
		 animWorld1 = gameObjectAnimWorld1.GetComponent<Animator> ();
       animWorld2 = gameObjectAnimWorld2.GetComponent<Animator> ();
		changeShaderArea = GameObject.Find ("ChangeShaderArea");
		uIFPS = GameObject.Find ("UIFPS");
		preFps = GameObject.Find ("PreFps");

		thirdPersonCamera = GameObject.Find ("ThirdPersonCamera");
		player = GameObject.FindGameObjectWithTag ("Player");
		 mapWord1 = gameObjectAnimWorld1.GetComponent<RawImage> ();
		mapWord2 = gameObjectAnimWorld2.GetComponent<RawImage> ();
		//defaultPosition= thirdPersonCamera.transform.position;
		done1 = true;
		done2 = true;
		//uIFPS.SetActive (false);
		preFps.SetActive (false);
		topViewCameraWorld1.SetActive (false);
		topViewCameraWorld2.SetActive (false);
		mapWord2.enabled = false;
		openGame = false;
		allMaterialHoloWorld1.SetActive (false);
		allMaterialHoloWorld2.SetActive (false);
//		gameObjectAnimWorld1.SetActive (false);
//		gameObjectAnimWorld2.SetActive (false);
	//	changeShaderArea.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (openGame) {
			if (TurnController.Instance.playerMovemnet && done1) {
				allMaterialHoloWorld1.SetActive (false);
				allMaterialHoloWorld2.SetActive (false);
				thirdPersonCamera.SetActive (true);
				if (TurnController.Instance.CurrentWorld == 1) {
					mapWord2.enabled = false;
					mapWord1.enabled = true;
					animWorld1.SetBool ("ShowMap", false);
					animWorld1.SetBool ("DownScaleMap", true);

				} else if (TurnController.Instance.CurrentWorld == 2) {
					mapWord2.enabled = true;
					mapWord1.enabled = false;
					animWorld2.SetBool ("ShowMap", false);
					animWorld2.SetBool ("DownScaleMap", true);
				}


				topViewCameraWorld1.SetActive (false);
				topViewCameraWorld2.SetActive (false);
				//uIFPS.SetActive (false);
				done1 = false;
				done2 = true;
			} else if (!TurnController.Instance.playerMovemnet && done2) {
				if (TurnController.Instance.CurrentWorld == 1) {
					mapWord2.enabled = false;
					mapWord1.enabled = true;	
				} else if (TurnController.Instance.CurrentWorld == 2) {
					mapWord2.enabled = true;
					mapWord1.enabled = false;
				}
				StartCoroutine (WaitEffectChangeshader ());

				done2 = false;
				done1 = true;
			}
			/*
		if (zoom) {
			if(thirdPersonCamera.transform.localRotation.x>0)
			thirdPersonCamera.transform.Rotate (-Time.deltaTime*40,0,0);
			if (thirdPersonCamera.transform.localPosition.y > 0.5f)
				thirdPersonCamera.transform.localPosition+= new Vector3 (0, -Time.deltaTime*3, 0); 
			if (thirdPersonCamera.transform.localPosition.z < 0)
				thirdPersonCamera.transform.localPosition += new Vector3 (0,0, Time.deltaTime*3); 

			if (thirdPersonCamera.transform.localPosition.z >= 0 && thirdPersonCamera.transform.localPosition.y <= 0.5f) {
				zoom = false;

			}
		}
		*/
		}
	}
	IEnumerator WaitEffectChangeshader()
	{
		
		//zoom = true;
		//uIFPS.SetActive (true);
		yield return new WaitForSeconds (1f);
		preFps.SetActive (true);
	//	changeShaderArea.SetActive (true);
		yield return new WaitForSeconds (0.5f);
		allMaterialHoloWorld1.SetActive (true);
		allMaterialHoloWorld2.SetActive (true);
	//	uIFPS.SetActive (true);
		preFps.SetActive (false);
		//mapWord1.enabled = true;
		//mapWord2.enabled = true;
		//thirdPersonCamera.SetActive (false);
	}
	public void ShowMap1()
	{
		animWorld1.SetBool ("DownScaleMap",false);
		animWorld1.SetBool("ShowMap",true);
		StartCoroutine (ShowCameraTopView1());
	}
	IEnumerator ShowCameraTopView1()
	{
		
		yield return new WaitForSeconds (1f);
		animWorld1.SetBool("ShowMap",false);
		thirdPersonCamera.SetActive (false);
		topViewCameraWorld1.SetActive (true);
		animWorld1.SetBool ("DownScaleMap",true);
		mapWord1.enabled = false;
	//	gameObjectAnim.SetActive (f alse);
	}
	public void ShowMap2()
	{
		animWorld2.SetBool ("DownScaleMap",false);
		animWorld2.SetBool("ShowMap",true);
		StartCoroutine (ShowCameraTopView2());
	}
	IEnumerator ShowCameraTopView2()
	{

		yield return new WaitForSeconds (1f);
		animWorld2.SetBool("ShowMap",false);
		thirdPersonCamera.SetActive (false);
			topViewCameraWorld2.SetActive (true);
		animWorld2.SetBool ("DownScaleMap",true);
		mapWord2.enabled = false;
		//	gameObjectAnim.SetActive (f alse);
	}
	public void OpenningGame()
	{
		openGame = true;
	}
}
