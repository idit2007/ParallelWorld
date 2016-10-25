using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class MenuDrag : MonoBehaviour {
	public Text stageText;
	StageManagement thisStage;
	private GameObject panel;
	public GameObject selectPopUp;  
	public GameObject[] starUIPopUp;  					 //Array's star image
	// Use this for initialization
	void Start () {
		panel = GameObject.FindGameObjectWithTag ("Panel");
		panel.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {


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

		RaycastHit2D hit;
		hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (finger.ScreenPosition), Vector2.zero);
		// Does the prefab exist?
		if (hit.transform.gameObject.tag=="StageButton") 
		{
			
			panel.SetActive (true);
			thisStage=hit.transform.gameObject.GetComponent<StageManagement> ();
			CurrentStage.nowStage=thisStage.numberStage;
			selectPopUp.SetActive (true);
			stageText.text = thisStage.name;
			if (thisStage.star[0].activeSelf)
			{
				starUIPopUp [0].SetActive (true);
			}
			if (thisStage.star[1].activeSelf)
			{
				starUIPopUp [1].SetActive (true);
			}
			if (thisStage.star[2].activeSelf)
			{
				starUIPopUp [2].SetActive (true);
			}
		}
		if (hit.transform.gameObject.tag == "Panel") 
		{
			selectPopUp.SetActive (false);
			panel.SetActive (false);
			starUIPopUp [0].SetActive (false);
			starUIPopUp [1].SetActive (false);
			starUIPopUp [2].SetActive (false);
		}

	}
	public void LoadlevelStage()
	{
		
			if(thisStage.numberStage%2==1)
				Application.LoadLevel(2);
			else 
				Application.LoadLevel(3);
		
	}
}
