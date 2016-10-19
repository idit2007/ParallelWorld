using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Victory : MonoBehaviour {
	
	private static Victory instance;
	public GameObject objects;
	public GameObject windForce;
	public int droppedObject;
	public bool isVictory=false;             
	public GameObject[] star;               
	//use singleton.
	public static Victory Instance
	{
		get {
			return instance;
		}
	}
	void Awake()
	{
		windForce.SetActive (false);
		instance = this;
		droppedObject = 0;
		objects.SetActive (false);

	}
	//Set active/unactive star  follow score.
	void Update()
	{    
		if(ScoreManageMent.Instance.intScore>=ScoreManageMent.Instance.starScore [0])
			{
				star [0].SetActive (true);
			}
		if(ScoreManageMent.Instance.intScore>ScoreManageMent.Instance.starScore[1])
			{
				star [1].SetActive (true);
			}
		if(ScoreManageMent.Instance.intScore>=ScoreManageMent.Instance.starScore[2])
			{
				star [2].SetActive (true);
			}
	}                                                                  
	public void NumDroppedObj()          //Number of object that dropeed.
	{
		droppedObject++;
	}
	public void ShowVictoryPopup()      // Show victory popup.
	{
		isVictory = true;
	}
}

	