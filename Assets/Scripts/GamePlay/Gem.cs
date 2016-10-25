using UnityEngine;
using System.Collections;

public class Gem : MonoBehaviour {
	private Rigidbody2D rb2d;
	public GameObject panel;
	public GameObject victoryImage;
	Ray camRay;
	bool done;
	// Use this for initialization
	private static Gem  instance;
	//use singleton.
	public static Gem   Instance
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
		rb2d = GetComponent<Rigidbody2D> ();
		done = true;
	}

	// Update is called once per frame
	void Update () {
		if (rb2d.isKinematic == false  &&done ) {
			StartCoroutine (MyCoroutine ());
	
		} 
	}
	 
	IEnumerator MyCoroutine()
	{
		yield return  new WaitForSeconds(2);   //Wait 2 secound.
		//Show victory image and close panel when object don't move anymore and dont lose's stiuation. 
		if (rb2d.velocity.y < 0.15f&&rb2d.velocity.x < 0.15f &&!Lose.Instance.loseImage.activeSelf) {
			yield return  new WaitForSeconds(1);
			victoryImage.SetActive (true);
			done = false;
			panel.SetActive (true);
		}
	}

}
