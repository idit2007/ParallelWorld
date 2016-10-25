using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Camera2 : MonoBehaviour {

	// Use

	// Use this for initialization
	void Start () {
		Screen.orientation = ScreenOrientation.LandscapeLeft;
	
//		tpcPos = thirdPersonCamera.transform.position;
//		tpcRos = thirdPersonCamera.transform.eulerAngles;
		//		gameObjectAnimWorld1.SetActive (false);
		//		gameObjectAnimWorld2.SetActive (false);


	}

	// Update is called once per frame
	void Update () {

	}

	/*
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
		animWorld2.SetBool ("DownScaleMap",true);
		mapWord2.enabled = false;
		//	gameObjectAnim.SetActive (f alse);
	}
	*/


}
