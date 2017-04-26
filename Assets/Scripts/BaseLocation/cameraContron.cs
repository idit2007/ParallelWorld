using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cameraContron : MonoBehaviour {
	public Transform CameraMap;
	public Transform CameraIN;
	public bool Map=true;
	private bool first=true;

	public GameObject PopUp;
	public GameObject YouWelcome;
	public GameObject BlackToModeLocationBase;
	public GameObject Unlock;

	private Ray ray;
	private RaycastHit hit;
	public GameObject selectedCurrentStage;
	private bool selected;




	// Use this for initialization

	void Start () {
		selected = false;
	}
	
	// Update is called once per frame
	void Update () {





		if (selected&&!Map) {
			transform.position = Vector3.MoveTowards(this.transform.position, CameraIN.position, 0.2f);
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, 0), 1f);

			if (transform.position == CameraIN.position && transform.rotation == Quaternion.Euler (0, 0, 0)) 
			{
				PopUp.SetActive (true);
			}

		} 
		else {
			
			if (first) {
				transform.position = CameraMap.position;
				transform.rotation =  Quaternion.Euler(24.727f, 0, 0);

			} 
			else {
				transform.position = Vector3.MoveTowards (this.transform.position, CameraMap.position, 0.2f);
				transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(24.727f, 0, 0), 1f);
				BlackToModeLocationBase.SetActive(true);
			}

		}
	}

	public void CameraZoomIN() {
		Map=false;
		first=false;
	}

	public void CameraZoomOut() {
		Map=true;
		selected = false;
		PopUp.SetActive(false);
		YouWelcome.SetActive(true);
		Unlock.SetActive(false);
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

		ray =  Camera.main.ScreenPointToRay(finger.ScreenPosition);

		if (Physics.Raycast(ray, out hit, 100)) {
			if (hit.transform.gameObject.tag == "ButtonOnMap") {
				selectedCurrentStage = GameObject.Find (hit.transform.gameObject.name);
				selected = false;

				Debug.Log("Touched " + hit.transform.gameObject.name);

				if (hit.transform.gameObject.name == "Red") 
				{
					Map = false;
					first = false;
					BlackToModeLocationBase.SetActive(false);
				}


				selected = true;
			}
		}
	}


	public void Popup()
	{
		YouWelcome.SetActive(false);
		Unlock.SetActive(true);
	}

	public void UnlockMap()
	{
		YouWelcome.SetActive(false);
		Unlock.SetActive(true);
	}
}
