using UnityEngine;
using System.Collections;

public class OpenningGame : MonoBehaviour {
	public GameObject setting;
	public GameObject panel;
	public GameObject mainCamera;
	public GameObject gemPopup;                   //Popup about detail of gem.
	public GameObject blockPopup;                 //Popup about detail of block.
	private int speedCamera ;
	private float botCamera ;
	private Vector3 gemFocus;
	private bool showGemDetail;
	private bool done;
	// Use this for initialization
	void Start () {
		botCamera = 0.3f;
		speedCamera = 5;
		gemFocus= new Vector3 (0,8,-10);
		done = true;
		showGemDetail = true;
		setting.SetActive (false);
		StartCoroutine (GemCoroutine());
	}
	// Update is called once per frame
	void Update () {
		if(showGemDetail)
			mainCamera.transform.localPosition =gemFocus;
		if (!showGemDetail && done) {
			mainCamera.transform.localPosition -= transform.up * Time.deltaTime * speedCamera;
			if (mainCamera.transform.localPosition.y < botCamera &&done ) {
				done = false;
				StartCoroutine (BlockCoroutine ());
			}
		}
	
	}
	public void CloseGemPopUp()
	{
		showGemDetail = false;
		gemPopup.SetActive(false);
	}
	public void CloseBlockPopUp()
	{
		blockPopup.SetActive (false);
		panel.SetActive (false);
		setting.SetActive (true);
	}


	IEnumerator GemCoroutine()
	{
		yield return  new WaitForSeconds(2);    //Wait one frame
		gemPopup.SetActive(true);

	}
	IEnumerator BlockCoroutine()
	{
		yield return  new WaitForSeconds(1);    //Wait one frame
		blockPopup.SetActive(true);
		panel.SetActive (true);
	}
}
