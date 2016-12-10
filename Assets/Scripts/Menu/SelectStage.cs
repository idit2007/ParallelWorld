using UnityEngine;
using System.Collections;

public class SelectStage : MonoBehaviour {
	private GameObject mainCamera;
	private Vector3 cameraDefaultPosition;
	private Vector3 thisObjectPosition;
	private Vector3 zoomTarget;
	private Vector3 newDir;
	private Transform target;
	private bool selected;
	private Vector3 lookRotation;
	public Material newBuildingMaterial;
	public Material oldBuildingMaterial;
	public GameObject budding2;
	private GameObject UI;
	private GameObject whitePanel;
	private Ray ray;
	private RaycastHit hit;
	private bool zoomInFinish;
	private bool zoomOut;
	private float rotX;
	void Start()
	{
		Screen.orientation = ScreenOrientation.LandscapeLeft;
		whitePanel = GameObject.Find ("WhitePanel");
		UI = GameObject.Find ("UI");
		mainCamera=GameObject.Find ("Main Camera");
		cameraDefaultPosition = GameObject.Find ("Main Camera").transform.position;
		thisObjectPosition = this.transform.position;
		selected = false;
		lookRotation = mainCamera.transform.rotation.eulerAngles;
		whitePanel.SetActive (false);
		UI.SetActive (false);
		zoomInFinish = false;
		zoomOut = true;
		rotX = mainCamera.transform.rotation.x;
	}
	void Update()
	{
		
		if(Screen.orientation!=ScreenOrientation.LandscapeLeft)
			Screen.orientation = ScreenOrientation.LandscapeLeft;
		Debug.Log (mainCamera.transform.rotation.y);
		//	Debug.DrawLine(ray.origin, hit.point);
		if (selected&&!zoomOut) {
			//mainCamera.transform.rotation = _lookRotation;
		

			mainCamera.transform.position = Vector3.MoveTowards (mainCamera.transform.position, zoomTarget, 10 * Time.deltaTime);
			if (mainCamera.transform.rotation.x > 0)
				mainCamera.transform.Rotate (Vector3.left * Time.deltaTime * 60, Space.Self);
			if (mainCamera.transform.position == zoomTarget && !zoomInFinish) {
				UI.SetActive (true);
				zoomInFinish = true;
			}

		}
		else if(zoomOut)
		{
			selected = false;
			mainCamera.transform.position = Vector3.MoveTowards (mainCamera.transform.position, cameraDefaultPosition, 10 * Time.deltaTime);
			if (mainCamera.transform.rotation.x < rotX)
				mainCamera.transform.Rotate (Vector3.right * Time.deltaTime * 60, Space.Self);
			if (mainCamera.transform.position == cameraDefaultPosition && zoomOut) {
				zoomOut = false;
				whitePanel.SetActive (false);
			}
		}
	}
	protected virtual void OnEnable()
	{
		// Hook into the OnFingerTap event
		Lean.LeanTouch.OnFingerTap += OnFingerTap;
	}

	protected virtual void OnDisable()
	{
		// Unhook into the OnFingerTap event
		Lean.LeanTouch.OnFingerTap -= OnFingerTap;
	}

	public void OnFingerTap(Lean.LeanFinger finger)
	{
		
		 ray = Camera.main.ScreenPointToRay(finger.ScreenPosition);

			if (Physics.Raycast(ray, out hit, 100)) {
			if (hit.transform.gameObject.tag == "StageButton") {
				budding2 = GameObject.Find (hit.transform.gameObject.name+"B2");
				selected = false;
				whitePanel.SetActive (true);
				budding2.SetActive (false);
				Renderer newMaterial=hit.transform.GetComponent<Renderer> ();
				newMaterial.sharedMaterial = newBuildingMaterial;
				Debug.Log ("Hit!");
				target = hit.transform;
				zoomTarget = new Vector3 (target.position.x,2,target.position.z-2.5f);
				selected = true;
			}
		}
	}
	public void BackButton()
	{
		zoomOut = true;
		zoomInFinish = false;
		budding2.SetActive (true);
		Renderer newMaterial=hit.transform.GetComponent<Renderer> ();
		newMaterial.sharedMaterial = oldBuildingMaterial;
	

	}
	public void StartButton()
	{
		Application.LoadLevel(1);
	}

}
