using UnityEngine;
using System.Collections;

public class CameraManagement : MonoBehaviour {
	public GameObject thirdPersonCamera;
	// Use this for initialization
	void Start () {
		thirdPersonCamera = GameObject.Find ("ThirdPersonCamera");
	}
	
	// Update is called once per frame
	void Update () {
		if (TurnController.Instance.playerMovemnet)
			thirdPersonCamera.SetActive (true);
		else
			thirdPersonCamera.SetActive (false);
	
	}
}
