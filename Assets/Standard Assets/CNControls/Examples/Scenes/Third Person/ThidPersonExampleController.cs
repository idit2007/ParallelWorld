using UnityEngine;
using CnControls;
using System.Collections;
// This is merely an example, it's for an example purpose only
// Your game WILL require a custom controller scripts, there's just no generic character control systems, 
// they at least depend on the animations


public class ThidPersonExampleController : MonoBehaviour
{
	private GameObject slowUI;
	public Animator anim;
	private bool done;
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
		slowUI = GameObject.Find ("SlowMotionFlash");
		anim = GameObject.Find ("EffectSlow").GetComponent<Animator>();
		anim.gameObject.SetActive (false);
		slowUI.SetActive (false);
		done = true;
	}
    public void Update()
    {
        // Just use CnInputManager. instead of Input. and you're good to go

        var inputVector = new Vector3(CnInputManager.GetAxis("Horizontal"), CnInputManager.GetAxis("Vertical"));
        Vector3 movementVector = Vector3.zero;

        // If we have some input
        if (inputVector.sqrMagnitude > 0.001f)
        {
            movementVector = _mainCameraTransform.TransformDirection(inputVector);
            movementVector.y = 0f;
            movementVector.Normalize();
			StartCoroutine (HideEffect ());
            _transform.forward = movementVector;
        }

        movementVector += Physics.gravity;
		_characterController.Move(movementVector * Time.deltaTime* MovementSpeed);
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


	}
}
