using UnityEngine;
using System.Collections;

public class InputGamePlayManagement : MonoBehaviour {
	// This stores the layers we want the raycast to hit (make sure this GameObject's layer is included!)
	public LayerMask LayerMask = UnityEngine.Physics2D.DefaultRaycastLayers;
	public GameObject DoneParrent;           
	public GameObject FloatingParrent;        
	Rigidbody2D rb2d; 
	Collider2D col2d; 
	public bool collisionBox;               // Collision stage  occur when block have collision.                   
	public bool collDone;                   //Make sure, object will have collision for one time .
	public bool isMoving;                   //Dragging stage.
	public bool isOutOfSavePoint;            
	private Vector3 savePoint;               //Begin object position.
	public GameObject explosionMuch;         //Particle much explostion.
	public GameObject explosionLess;         //Particle less explostion.
	// This stores the finger that's currently dragging this GameObject
	private Lean.LeanFinger draggingFinger;
	// Use this for initialization
	void Start () {
		DoneParrent = GameObject.Find ("DoneParrent");
		FloatingParrent = GameObject.Find ("ParrentObjects");
		collDone = false;
		collisionBox = false;
		isMoving = false;
		savePoint = new Vector3(4.42f,2.85f,2.2f);
		this.transform.localPosition = savePoint;
		isOutOfSavePoint = false;
		rb2d = GetComponent<Rigidbody2D> ();
		col2d= GetComponent<Collider2D> ();
		col2d.isTrigger = true;

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
		RaycastHit2D hit;
		hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (finger.ScreenPosition), Vector2.zero);
           //Select block and change parrent.
		if (hit.collider.gameObject == gameObject&&hit.transform.gameObject.tag=="Box")
			{
			FloatObject.Instance.isFloat = false;
			col2d = hit.transform.gameObject.GetComponent<Collider2D> ();
			rb2d =  hit.transform.gameObject.GetComponent<Rigidbody2D> ();
			col2d.isTrigger =false;
			this.transform.parent = DoneParrent.transform;
				draggingFinger = finger;	   
			}
	}

	public void OnFingerUp(Lean.LeanFinger finger)
	{
		//When let object down floatting systime will be active.
		FloatObject.Instance.isFloat = true;
		// Was the current finger lifted from the screen?
		if (finger == draggingFinger) {
			// Unset the current finger
			draggingFinger = null;
			// Create new block when mouse up.
			if (!collisionBox && isOutOfSavePoint) 
			{
				rb2d.isKinematic = false;
				CreateUIObject.Instance.ChageUIBlock ();
				CreateObject.Instance.ChageBlock ();
				this.tag = "row";
				collDone = true;
			}
			//Go to save point position and set to begin stage.
			if (!isOutOfSavePoint && !collisionBox && !collDone) 
			{
				collDone = false;
				col2d.isTrigger = true;
				isMoving = false;
				isOutOfSavePoint = false;
				this.transform.parent = FloatingParrent.transform;
				rb2d.isKinematic = true;
				this.transform.localPosition= savePoint;
			}
		}
	}

	void OnCollisionEnter2D(Collision2D coll) 
	{
		// Create new block when have collision with block.
		if (coll.transform.tag =="row"&&!collDone&&isOutOfSavePoint) {    
			rb2d.isKinematic = false;
			this.tag = "row";
			this.transform.parent = DoneParrent.transform;
			CreateUIObject.Instance.ChageUIBlock ();
			CreateObject.Instance.ChageBlock ();
			collDone = true;
			collisionBox = true;
			draggingFinger = null;

		}
		// Particle occur when have collision.
		if(coll.transform.tag == "row"||coll.transform.tag == "OnGround"||coll.transform.tag == "Gem"||coll.transform.tag == "Stage")
		{
			// Spawn an explosion at each point of contact
			foreach(ContactPoint2D missileHit in coll.contacts)
			{
				Vector2 hitPoint = missileHit.point;
				Vector3 v3 = new Vector3 (hitPoint.x, hitPoint.y, 0);
				if (coll.relativeVelocity.magnitude > 10) {
					Instantiate (explosionMuch, v3, Quaternion.identity);
				} else if   (coll.relativeVelocity.magnitude > 1){
					Instantiate (explosionLess, v3, Quaternion.identity);
				}
			}
		}
	}
	void OnCollisionStay2D(Collision2D coll) 
	{
		// Create new block when have collision with block.
		if (coll.transform.tag =="row"&&!collDone&&isOutOfSavePoint) {     
			collisionBox = true;
			rb2d.isKinematic = false;
			this.tag = "row";
			this.transform.parent = DoneParrent.transform;
			CreateUIObject.Instance.ChageUIBlock ();
			CreateObject.Instance.ChageBlock ();
			collDone = true;
			collisionBox = true;
			draggingFinger = null;
		}
	}
	//Cheak area about save point area.
	void OnTriggerExit2D(Collider2D col)
	{
		if (col.CompareTag("SavePointArea")) 								
		{
			isOutOfSavePoint = true;
		}
	}
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.CompareTag("SavePointArea")) 									
		{
			isOutOfSavePoint = false;
		}
	}


}
