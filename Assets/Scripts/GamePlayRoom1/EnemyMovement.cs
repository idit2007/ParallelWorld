using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

	SphereCollider enemeyArea;
	GameObject player;               // Reference to the player's position.
	Rigidbody rb;
	NavMeshAgent nav;               // Reference to the nav mesh agent.
	void Awake()
	{

		// Set up the references.
		enemeyArea=GetComponent<SphereCollider>();
		player = GameObject.FindGameObjectWithTag("Player");
		nav = GetComponent<NavMeshAgent>();
		rb=GetComponent<Rigidbody>();
		enemeyArea.isTrigger = true;
		nav.enabled = false;
	}


	void Update()
	{
		rb.useGravity = true;
		if(nav.enabled)
		nav.SetDestination(player.transform.position);




	}
	void OnTriggerEnter(Collider scol) {
		if (scol.gameObject.tag == "Player") {
			nav.enabled = true;
		}
	}
	void OnTriggerStay(Collider scol) {
		if (scol.gameObject.tag == "Player") {
			nav.enabled = true;
		}
	}
	void OnTriggerExit(Collider scol) {
		if (scol.gameObject.tag == "Player") {
			nav.enabled = false;
		}
	}
}
