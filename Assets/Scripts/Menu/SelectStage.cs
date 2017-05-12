using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
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
	public GameObject selectedCurrentStage;
	private GameObject panelStage;
	private GameObject UI;
	private GameObject whitePanel;
	private Ray ray;
	private RaycastHit hit;
	private bool zoomInFinish;
	private bool zoomOut;
	private float rotX;
	public GameObject blackPanel;
	MeshRenderer mr;
	void Start()
	{
		TimeScore.currentStage = 0;
		panelStage = GameObject.Find ("PanelStage");
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
		//	Debug.DrawLine(ray.origin, hit.point);
		if (selected&&!zoomOut) {
			//mainCamera.transform.rotation = _lookRotation;
		

			mainCamera.transform.position = Vector3.MoveTowards (mainCamera.transform.position, zoomTarget, 10 * Time.deltaTime);
			if (mainCamera.transform.rotation.x > 100)
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
				panelStage.SetActive (true);
			}
		}
	}


	public void BackButton()
	{
		zoomOut = true;
		zoomInFinish = false;
		mr.enabled = true;
		TimeScore.currentStage = 0;


	}
	public void StartButton()
	{
		blackPanel.SetActive (true);
		int x = 0;
		string stage = selectedCurrentStage.name;
		char st = stage [0];
		int.TryParse(st.ToString(),out x);
		TimeScore.currentStage = x;
		StartCoroutine (StartGamedelay());
	}
	public void SelectButton(string stageNum)
	{

		selected = false;
		selectedCurrentStage = GameObject.Find (stageNum);
		whitePanel.SetActive (true);
		LeaderBoard.pressStage = stageNum;
		mr=GameObject.Find ("Stage"+stageNum).GetComponent<MeshRenderer> ();
		mr.enabled = false;
		target = selectedCurrentStage.transform;
		zoomTarget = new Vector3 (target.position.x+1,1f,target.position.z-1);
		selected = true;
		panelStage.SetActive (false);

	}
	public void StartTutorial()
	{
		blackPanel.SetActive (true);
		StartCoroutine (StartTutorialdelay());
	}
	IEnumerator StartTutorialdelay()
	{
		yield return new WaitForSeconds (0.5f);
		SceneManager.LoadScene ("tutorial");
	}
	IEnumerator StartGamedelay()
	{
		yield return new WaitForSeconds (0.5f);
		SceneManager.LoadScene("Stage"+selectedCurrentStage.name);
	}
}
