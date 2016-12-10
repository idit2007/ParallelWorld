using UnityEngine;
using System.Collections;

public class EnemyNevmesh : MonoBehaviour {
	private NavMeshAgent navMeshAgent;
	private GameObject player;
	private GameObject effectTeleportSlow;
	bool inArea;
	// Use this for initialization
	void Start () {
		
		inArea = false;
		navMeshAgent = GetComponent<NavMeshAgent>( );
		 player = GameObject.FindGameObjectWithTag ("Player");
		effectTeleportSlow = GameObject.Find ("EffectSlow");
	}
	void Update()
	{
		
		if (inArea&&!effectTeleportSlow.activeSelf) {
			navMeshAgent.enabled = true;
			navMeshAgent.SetDestination (player.transform.position);
		}
		else 
			navMeshAgent.enabled = false;
			
	}
	void OnTriggerEnter(Collider coll) {
		
		if (coll.gameObject.tag == "Player") {
			inArea = true;
		
		}
	}
	void OnTriggerStay(Collider coll) {
		if (coll.gameObject.tag == "Player") {
			inArea = true;
		} 
	}
	void OnTriggerExit(Collider coll) {
		if (coll.gameObject.tag == "Player") {
			inArea = false;
		}
	}

}
