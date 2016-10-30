using UnityEngine;
using System.Collections;

public class PlayerKeyBoardMovement : MonoBehaviour {
	private GameObject player;
	private GameObject slowUI;
	public Animator anim;
	private bool done;
	private bool done2;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player2");
		slowUI = GameObject.Find ("SlowMotionFlash");
		anim = GameObject.Find ("EffectSlow").GetComponent<Animator>();
		anim.gameObject.SetActive (false);
		slowUI.SetActive (false);
		done = true;
		done2 = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey ("w") || Input.GetKey ("d") || Input.GetKey ("a") || Input.GetKey ("s")) {
			if (done) {
				StartCoroutine (HideEffect ());
			}
			if (!anim.gameObject.activeSelf&& TurnController.Instance.playerMovemnet) {
				if (Input.GetKey ("s") ) {
					player.transform.Translate (Time.deltaTime * 3, 0, 0);
				}

				if (Input.GetKey ("w") ) {
					player.transform.Translate (-Time.deltaTime * 3, 0, 0);
				}
				if (Input.GetKey ("d") ) {
					player.transform.Translate (0, 0, Time.deltaTime * 3);
				}
				if (Input.GetKey ("a") ) {
					player.transform.Translate (0, 0, -Time.deltaTime * 3);
				}

			}
		} 
	
		if (!anim.gameObject.activeSelf) {
			TurnController.Instance.playerMovemnet = true;
		}
	
	}
	IEnumerator ShowEffect(){
		anim.gameObject.SetActive (true);
		yield return new WaitForSeconds (0.4f);
		if (!done) {
			slowUI.SetActive (true);
			done = true;
		}
	}
	IEnumerator HideEffect(){
		done = false;
		anim.SetTrigger ("EffectBack");

		yield return new WaitForSeconds (0.2f);
		slowUI.SetActive (false);

	

	}
	public void TeleportPause()
	{
		StartCoroutine (ShowEffect ());

		TurnController.Instance.playerMovemnet = false;
	}
}
