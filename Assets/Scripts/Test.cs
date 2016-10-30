using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {
	private Material thisMat;

	// Use this for initialization
	void Start () {
		thisMat = GetComponent<Renderer>().material;
	}
	
	// Update is called once per frame
	void Update () {
		thisMat.SetFloat( "_ColorIntensity", 1 );
	}
}
