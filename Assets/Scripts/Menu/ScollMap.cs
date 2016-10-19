using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ScollMap : MonoBehaviour {
	public GameObject universe;
	bool isDraging;
	float speedDraging=1f;
	int topCameraZ = -20;
	int lessDistanceToDrag =-1;

	bool done;
	// This stores the finger that's currently dragging this GameObject
	private Lean.LeanFinger draggingFinger;

	// Update is called once per frame
	void Update () {
		//Protect dragging is doesn't out of area's back ground.
		if (universe.transform.localPosition.z > 0&&done) {
			isDraging = false;
			universe.transform.localPosition += new Vector3 (0,0, -Time.deltaTime);
			if (universe.transform.localPosition.z < -2) done = false;
		}
		if (universe.transform.localPosition.z < topCameraZ&&done) {
			isDraging = false;
			universe.transform.localPosition += new Vector3 (0,0, Time.deltaTime);
			if(universe.transform.localPosition.z > topCameraZ) done = false;
		}
		//Rotate object.
		if(isDraging){
			universe.transform.localPosition+=new Vector3(0, 0,Time.deltaTime*Lean.LeanTouch.DragDelta.y*speedDraging);
		}
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
		Ray ray = Camera.main.ScreenPointToRay(finger.ScreenPosition);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, 100)) {
			if (hit.transform.gameObject.tag == "Ground") {
				Debug.Log ("Test");
				isDraging = true;
				done = true;
			}
		}
	}

	public void OnFingerUp(Lean.LeanFinger finger)
	{
		// Was the current finger lifted from the screen?
		if (finger == draggingFinger)
		{
			// Unset the current finger
			draggingFinger = null;
		}
	}
}
