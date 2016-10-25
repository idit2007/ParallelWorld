using UnityEngine;
using System.Collections;

public class PlayerKeyBoardMovement : MonoBehaviour {
	private GameObject player;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player2");
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey ("w")||Input.GetKey ("d")||Input.GetKey ("a")||Input.GetKey ("s")){
			TurnController.Instance.playerMovemnet = true;
		if (Input.GetKey ("s"))
			player.transform.Translate (Time.deltaTime*3,0,0);

		if (Input.GetKey("w"))
			player.transform.Translate (-Time.deltaTime*3,0,0);
		if (Input.GetKey("d"))
			player.transform.Translate (0,0,Time.deltaTime*3);
		if (Input.GetKey("a"))
			player.transform.Translate (0,0,-Time.deltaTime*3);
		}
		else 
			TurnController.Instance.playerMovemnet = false;

	
	}
}
