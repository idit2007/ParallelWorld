using UnityEngine;
using System.Collections;
using CnControls;
public class ControlJoy : MonoBehaviour {
	private GameObject player;
	private GameObject slowUI;
	public Animator anim;
	private bool done;
	private bool done2;
	private float MovementSpeed = 5f;

	private Transform _mainCameraTransform;
	private Transform _transform;
	private CharacterController _characterController;

	private void OnEnable()
	{
		_mainCameraTransform = Camera.main.GetComponent<Transform>();
		_characterController = GetComponent<CharacterController>();
		_transform = GetComponent<Transform>();
	}
	void Start()
	{
		player = GameObject.Find ("Player2");
		slowUI = GameObject.Find ("SlowMotionFlash");
		anim = GameObject.Find ("EffectSlow").GetComponent<Animator>();
		anim.gameObject.SetActive (false);
		slowUI.SetActive (false);
		done = false;
		done2 = true;
	}
	public void Update()
	{
		// Just use CnInputManager. instead of Input. and you're good to go

		if (CnInputManager.GetAxis ("Horizontal") != 0 || CnInputManager.GetAxis ("Vertical") != 0) {
			//Debug.Log ("test");
			if (done) {
				StartCoroutine (HideEffect ());
			}
		}
			if (!anim.gameObject.activeSelf && TurnController.Instance.playerMovemnet) {
				var inputVector = new Vector3 (CnInputManager.GetAxis ("Horizontal"), CnInputManager.GetAxis ("Vertical"));
				Vector3 movementVector = Vector3.zero;

				// If we have some input
				if (inputVector.sqrMagnitude > 0.001f) {
					movementVector = _mainCameraTransform.TransformDirection (inputVector);
					movementVector.y = 0f;
					movementVector.Normalize ();
					_transform.forward = movementVector;
				}

				movementVector += Physics.gravity;
				_characterController.Move (movementVector * Time.deltaTime * MovementSpeed);
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