using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {


	GameObject player;               // Reference to the player's position.
	Rigidbody rb;
	NavMeshAgent nav;               // Reference to the nav mesh agent.
	void Awake()
	{

		// Set up the references.
		player = GameObject.FindGameObjectWithTag("Player");
		nav = GetComponent<NavMeshAgent>();
		rb=GetComponent<Rigidbody>();
	}


	void Update()
	{
		rb.useGravity = true;
		nav.SetDestination(player.transform.position);




	}
}
