using UnityEngine;
using System.Collections;

public class CreateObject : MonoBehaviour {
	public TextMesh numobj;
	public GameObject machine;
	public GameObject[] objArray;
	private GameObject  parrent;
	private GameObject newObj;
	public int currentBlock=0;
	private int oldBlock=0;
	private Vector3 positionUI;
	private int remainingObjectCount;
	private bool done;
	//public GameObject parrent;
	// Use this for initialization
	private static CreateObject instance;
	//use singleton.
	public static CreateObject  Instance
	{
		get {

			return instance;
		}
	}

	void Awake()
	{
		instance = this;
	}

	void Start () {
		parrent = GameObject.Find ("ParrentObjects");
		//Create first  object.
		positionUI = new Vector3 (machine.transform.position.x + 2f, machine.transform.position.y + 1.3f, machine.transform.position.z + 1);
		remainingObjectCount=objArray.Length;
		newObj =Instantiate (objArray [currentBlock],positionUI, Quaternion.identity)as GameObject;
		newObj.transform.parent = parrent.transform;
		currentBlock++;
		done = true;
	}

	// Update is called once per frame
	void Update () {
		//Go to cut scene when out of stock.
		remainingObjectCount = (objArray.Length - oldBlock);
		numobj.text = (objArray.Length - currentBlock).ToString();
		if (remainingObjectCount == 0 && done) {
			RotatonControl.Instance.DropGem ();
			done = false;
		}
	}
	//Create  object .
	public void ChageBlock(){
		oldBlock = currentBlock;
		newObj =Instantiate (objArray [currentBlock],positionUI, Quaternion.identity)as GameObject;
		newObj.transform.parent = parrent.transform;
		currentBlock++;
	}







}