using UnityEngine;
using System.Collections;

public class RotatonControl : MonoBehaviour {
	public LayerMask LayerMask = UnityEngine.Physics2D.DefaultRaycastLayers;
	private Lean.LeanFinger draggingFinger;
	// This allows use to set how powerful the swipe will be
	public float ForceMultiplier = 1.0f;
	public GameObject mainCamera;        //Camera.
	public GameObject oldParrent;         //Parrent of objects that floating.
	public GameObject newParrent;         //New parrent which stable.
	public Rigidbody2D gem;

	// Use this for control camera.
	private bool isDropGem;             //End stage.
	private bool beginPos;              //Object 's begin position.
	private Ray camRay;                          
	private Ray beginRay;
	private bool done;
	private int speedEndCamera=7;
	private int speedRotation=50;
	private int speedCameraDown=15;
	private int speedCameraUp=-15;
	private float beginCameraY=0.3f;
	private float topCameraY=8.13f;
	private int lessDistanceToDrag=1;
	private GameObject objects;           //Group object.
	private bool isRotating;
	public Animator anim;
	// Use this for initialization

	private static RotatonControl instance;
	//use singleton.
	public static RotatonControl  Instance
	{
		get 
		{
			return instance;
		}
	}
	void Awake()
	{
		instance = this;
	}

	// Update is called once per frame
	void Update () {
		if(Lean.LeanTouch.Fingers.Count>0)
		camRay= Camera.main.ScreenPointToRay (Lean.LeanTouch.Fingers[0].ScreenPosition);
		objects = GameObject.FindGameObjectWithTag ("Box");
		//Set to default position if camera down or up more default.
		if (mainCamera.transform.localPosition.y < 0)
			mainCamera.transform.localPosition = new Vector3 (0,0,-10);
		if(mainCamera.transform.localPosition.y > topCameraY)
			mainCamera.transform.localPosition = new Vector3 (0,topCameraY,-10);
		//Rotate object.
		if(isRotating){
		objects.transform.Rotate (0, 0, -speedRotation * Time.deltaTime*Lean.LeanTouch.DragDelta.x);
			if(beginRay.origin.y-camRay.origin.y>1)
				mainCamera.transform.localPosition += new Vector3 (0,speedCameraUp*-Time.deltaTime,0);
		}
		//Camera move  up and drop gem.
		if (isDropGem ) {
			mainCamera.transform.localPosition += transform.up * Time.deltaTime * speedEndCamera;
			if (mainCamera.transform.localPosition.y > topCameraY) {
				anim.SetBool ("isCutScence",true);
				isDropGem = false;
				gem.isKinematic = false;
				done = true;
			}
		}
		//Camera move down when gem dropped.
		if (gem.isKinematic == false && done) {
			mainCamera.transform.localPosition -= transform.up * Time.deltaTime * speedEndCamera;
			if (mainCamera.transform.localPosition.y < beginCameraY)
				done = false;
		}
		//Camera move down when player drag it up.
		if (beginPos) {
			if(mainCamera.transform.localPosition.y > beginCameraY)
				mainCamera.transform.localPosition -= transform.up * Time.deltaTime * speedCameraDown;
			if (mainCamera.transform.localPosition.y < beginCameraY)
				beginPos = false;
		}

	}
	public void DropGem()
	{
		isDropGem = true;
	}
	protected virtual void OnEnable()
	{
		// Hook into the OnFingerDown event
		Lean.LeanTouch.OnFingerDown += OnFingerDown;
		// Hook into the OnFingerUp event
		Lean.LeanTouch.OnFingerUp += OnFingerUp;
	}

	protected virtual void OnDisable()
	{
		// Unhook the OnFingerDown event
		Lean.LeanTouch.OnFingerDown -= OnFingerDown;
		// Unhook the OnFingerUp event
		Lean.LeanTouch.OnFingerUp -= OnFingerUp;
	}

	protected virtual void LateUpdate()
	{
		// If there is an active finger, move this GameObject based on it
		if (draggingFinger != null)
		{
			Lean.LeanTouch.MoveObject(transform, draggingFinger.DeltaScreenPosition);
		}
	}

	public void OnFingerDown(Lean.LeanFinger finger)
	{
     
		// Raycast information
		RaycastHit2D hit;
		beginRay= Camera.main.ScreenPointToRay (finger.ScreenPosition);

		hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (finger.ScreenPosition), Vector2.zero);
		//Drag blackground to rotate object.
		if (hit.transform.gameObject.tag=="BG")
		{
		FloatObject.Instance.isFloat =false;
			beginPos = false;
			objects .transform.parent = newParrent.transform;
			isRotating=true;
		}
	}
	public void OnFingerOver(Lean.LeanFinger finger)
	{
		// Raycast information

		camRay= Camera.main.ScreenPointToRay (finger.ScreenPosition);


	}
	public void OnFingerUp(Lean.LeanFinger finger)
	{
		if (isRotating) {
			objects.transform.parent = oldParrent.transform;
			beginPos = true;
		}
		isRotating=false;
		FloatObject.Instance.isFloat = true;
		// Was the current finger lifted from the screen?
		if (finger == draggingFinger)
		{
			// Unset the current finger
			//draggingFinger = null;
		}
	}
}
