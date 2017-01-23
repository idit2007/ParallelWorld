using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

	SphereCollider enemeyArea;
	GameObject player;               // Reference to the player's position.
	Rigidbody rb;
	public UnityEngine.AI.NavMeshAgent nav;               // Reference to the nav mesh agent.
	private float minTarX=-3;
	private float minTarZ=-3;
	private float maxTarX=3;
	private float maxTarZ=3;
	public float tarX;
	public float tarZ;
	public bool target;
	private bool done;
	public Vector3 beginPos;
	public float timeSwitch=100;
	void Awake()
	{
		beginPos = this.transform.position;
		// Set up the references.
		enemeyArea=GetComponent<SphereCollider>();
		player = GameObject.FindGameObjectWithTag("Player");
		nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
		rb=GetComponent<Rigidbody>();
		enemeyArea.isTrigger = true;
		target= false;
		done = true;
		EnemyRandomMovement ();
	}


	void Update()
	{
		rb.useGravity = true;
		if (target) {
			if(nav.enabled)
			nav.SetDestination (player.transform.position);
			if (!TurnController.Instance.playerMovemnet) {
				nav.enabled = false;
			} else
				nav.enabled = true;
		}
		else if (!target) {
			nav.enabled = true;
			if (timeSwitch <= 0) {
				timeSwitch = 100;
				EnemyRandomMovement ();

			} 
			else 
			{
				timeSwitch -= 20 * Time.deltaTime;
				if(nav.enabled)
				nav.destination= new Vector3 (tarX, this.transform.position.y, tarZ);

			}

 
		}
		if (TurnController.Instance.playerMovemnet == false) {
			
			enemeyArea.enabled = false;
		}
		else if(TurnController.Instance.playerMovemnet )
		{
			enemeyArea.enabled = true;
		}

	}
	void OnTriggerEnter(Collider scol) {
		if (scol.gameObject.tag == "Player") {
			target = true;
		}
	}
	void OnTriggerStay(Collider scol) {
		if (scol.gameObject.tag == "Player") {
			target= true;
		}
	}
	void OnTriggerExit(Collider scol) {
		if (scol.gameObject.tag == "Player") {
			if (TurnController.Instance.playerMovemnet == false) 
			target= false;
			done = true;
		}
	}
	/*
	void OnCollisionEnter(Collision coll) {
		if (coll.gameObject.tag == "Environment") {
			Debug.Log ("Collistion");
			done = true;
		}
	}
	void OnCollisionStay(Collision coll) {
		if (coll.gameObject.tag == "Environment") {
			Debug.Log ("Collistion");
			EnemyRandomMovement ();
			done = true;
		}
	}
	*/
	private void EnemyRandomMovement()
	{
		tarX = Random.Range (beginPos.x+minTarX,beginPos.x+maxTarX);
		tarZ = Random.Range (beginPos.z+minTarZ,beginPos.z+maxTarZ);
	}
}
