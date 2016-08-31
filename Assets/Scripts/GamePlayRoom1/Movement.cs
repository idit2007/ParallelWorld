using UnityEngine;
using System.Collections;
public class Movement : MonoBehaviour {
	public float speed = 0.001f;
	public bool isMoving;
	public Transform[] targetList;
	public float cntStep = 0;
	public int indexTarget = 0;
	// Use this for initialization
	void Start () {
		isMoving = true;
	}
	
	// Update is called once per frame
	void Update () {

		if (isMoving)
		{
			cntStep += Time.deltaTime * speed;
			transform.position = Vector3.MoveTowards(transform.position, targetList[indexTarget].position, cntStep);
			var rotation = Quaternion.LookRotation(targetList[indexTarget].position - transform.position);
			transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 2);
			if (Vector3.Distance(transform.position, targetList[indexTarget].position) < 0.001f)
			{
				transform.LookAt(targetList[indexTarget]);
				indexTarget++;
				if (indexTarget >= targetList.Length)
				{
					transform.rotation = targetList[indexTarget - 1].rotation;
					isMoving = false;
					targetList = null;


				}
			}
		}
	}
}
