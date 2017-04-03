using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTopView : MonoBehaviour {
	private GameObject mainCamera;
	public bool zoomIN;
	private Vector3 playerTransform;
	private Vector3 cameraDefaultPosition;
	public GameObject player;
	private Vector3 offset;
	private float cameraTopHight;
	// Use this for initialization
	void Start () {
		player=GameObject.FindGameObjectWithTag("Player");
		cameraDefaultPosition = GameObject.FindGameObjectWithTag("MainCamera").transform.position;
		mainCamera = GameObject.FindGameObjectWithTag ("MainCamera");
		playerTransform = new Vector3(this.transform.position.x,this.transform.position.y+0.5f,this.transform.position.z);
		zoomIN = false;
		offset = mainCamera.transform.position - player.transform.position;
		cameraTopHight = 20;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		mainCamera.transform.position =new Vector3(player.transform.position.x+offset.x,player.transform.position.y+cameraTopHight,player.transform.position.z+offset.z) ;
		if (zoomIN) {
			if (cameraTopHight < 30)
				cameraTopHight += Time.deltaTime * 20;

//			Debug.Log (playerTransform+" "+mainCamera.transform.position);
		//	mainCamera.transform.position = Vector3.MoveTowards (mainCamera.transform.position, playerTransform , 20 * Time.deltaTime);
			if (mainCamera.transform.rotation.x < 0.70f) {
				mainCamera.transform.Rotate (Vector3.right * Time.deltaTime * 80, Space.Self);
			
			}
		

		}
		else if(!zoomIN)
		{
			if (cameraTopHight >20)
				cameraTopHight -= Time.deltaTime * 20;
		//	mainCamera.transform.position = Vector3.MoveTowards (mainCamera.transform.position, cameraDefaultPosition, 10 * Time.deltaTime);
			if (mainCamera.transform.rotation.x > 0.46f)
				mainCamera.transform.Rotate (Vector3.left * Time.deltaTime * 80, Space.Self);
			
		}
	}

			void OnTriggerEnter(Collider coll) {

				if (coll.gameObject.tag == "Wall") {
			        zoomIN = true;
				}
			}
			void OnTriggerStay(Collider coll) {
		         if (coll.gameObject.tag == "Wall") {
			        zoomIN = true;
				} 
			}
			void OnTriggerExit(Collider coll) {
		        if (coll.gameObject.tag == "Wall") {
			        zoomIN = false;
				}
			}


}
