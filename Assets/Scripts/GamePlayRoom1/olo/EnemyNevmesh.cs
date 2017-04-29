using UnityEngine;
using System.Collections;

public class EnemyNevmesh : MonoBehaviour {
	public UnityEngine.AI.NavMeshAgent navMeshAgent;
	private GameObject player;
	private GameObject effectTeleportSlow;
	public Animator anim; 
	private AudioSource soundZombie;
	bool inArea;
	// Use this for initialization
	void Awake()
	{
		effectTeleportSlow = GameObject.Find ("EffectSlow");
	}
	void Start () {
		soundZombie = GetComponent<AudioSource> ();
		inArea = false;
		navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>( );
		 player = GameObject.FindGameObjectWithTag ("Player");
		anim = GetComponent<Animator> ();
		soundZombie.enabled = false;
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
			anim.SetBool ("run",true);
			soundZombie.enabled = true;
		}
	}
	void OnTriggerStay(Collider coll) {
		if (coll.gameObject.tag == "Player") {
			inArea = true;
			anim.SetBool ("run",true);
			soundZombie.enabled = true;

		} 
	}
	void OnTriggerExit(Collider coll) {
		if (coll.gameObject.tag == "Player") {
			inArea = false;
			anim.SetBool ("run",false);
			soundZombie.enabled = false;

		}
	}
	void OnCollisionEnter(Collision coll) {

		if (coll.gameObject.tag == "Player") {

			anim.SetTrigger("attack");
		}
	}

}
