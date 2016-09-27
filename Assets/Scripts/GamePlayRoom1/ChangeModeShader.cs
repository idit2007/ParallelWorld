using UnityEngine;
using System.Collections;

public class ChangeModeShader : MonoBehaviour {
	public Material hologramMaterial;
	void Start()
	{
	}
	void OnTriggerEnter(Collider col) {
		if(col.CompareTag("ShaderChangeArea")){
			Renderer newMaterial=GetComponent<Renderer> ();
			newMaterial.sharedMaterial = hologramMaterial;
		}
	}
	
}
