using UnityEngine;
using System.Collections;
using CnControls;
public class ControlJoy : MonoBehaviour {
	private GameObject slowUI;
	public Animator anim;
	private Animator novaAnim;
	private bool done;
	private bool done2;
	private float MovementSpeed = 5f;
	private AudioSource soundRun;
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
		soundRun = GetComponent<AudioSource> ();
		slowUI = GameObject.Find ("SlowMotionFlash");
		novaAnim = GetComponent<Animator>();
		anim.gameObject.SetActive (false);
		if(slowUI!=null)
		slowUI.SetActive (false);
		done = false;
		done2 = true;
		soundRun.enabled = false;
	}
	public void Update()
	{
		// Just use CnInputManager. instead of Input. and you're good to go

		if (CnInputManager.GetAxis ("Horizontal") != 0 || CnInputManager.GetAxis ("Vertical") != 0) {
			if (done) {
				EffectPlayerSlowMotion.Instance.playerMove = true;

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
				novaAnim.SetBool ("Run", true);
				soundRun.enabled = true;
			}
			else {
				novaAnim.SetBool ("Run", false);
				soundRun.enabled = false;
			}
				movementVector += Physics.gravity;
				_characterController.Move (movementVector * Time.deltaTime * MovementSpeed);
			soundRun.enabled = true;
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
		EffectPlayerSlowMotion.Instance.playerMove = false;
		novaAnim.SetBool ("Run", false);
		soundRun.enabled = false;
		StartCoroutine (ShowEffect ());

	}
}