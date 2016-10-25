using UnityEngine;
using System.Collections;

public class Lose : MonoBehaviour {
	public GameObject objects;
	public GameObject panel;
	public GameObject windForce;
	public GameObject loseImage;
	// Use this for initialization
	private static Lose  instance;
	//use singleton.
	public bool isLose;
	public static Lose   Instance
	{
		get {
			return instance;
		}
	}

	void Awake()
	{
		instance = this;
	}
	void Start () {
		isLose = false;

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	//Show lose image and close panel if gem collision or touch ground.
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.CompareTag("Gem")&&!Gem.Instance.victoryImage.activeSelf)      
		{
			windForce.SetActive (false);
			objects.SetActive (false);
			loseImage.SetActive (true);
			isLose = true;
			panel.SetActive (true);
		}
	}
	void OnCollisionStay2D(Collision2D coll) 
	{
		if (coll.transform.tag == "Gem"&&!Gem.Instance.victoryImage.activeSelf) 
		{
			windForce.SetActive (false);
			objects.SetActive (false);
			loseImage.SetActive (true);
			isLose = true;
			panel.SetActive (true);
		}
	}
}
