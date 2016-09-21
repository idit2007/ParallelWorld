using UnityEngine;
using System.Collections;

public class ShaderManagement : MonoBehaviour {
	public Material[] oldMaterials;
	public Material hologramMaterial;
	public Transform AllMaterialsWorld1;
	public Renderer floorWorld1;
	public Material newfloorMaterial;
	public Material oldfloorMaterial;
	private int i;
	private int j;
	// Use this for initialization
	void Start () {
		i = 0;
		oldMaterials=new Material[AllMaterialsWorld1.childCount];
		foreach (Transform child in  AllMaterialsWorld1 ){
			Renderer newMaterial=child.GetComponent<Renderer> ();
			oldMaterials[i] = newMaterial.sharedMaterial;
			i++;
		}

	}
	
	// Update is called once per frame
	void Update () {
		if (TurnController.Instance.playerMovemnet)
			DefaultScene ();
		else
			HoloGramScene ();
		
	}
	private void HoloGramScene()
	{
		foreach (Transform child in  AllMaterialsWorld1) {
			Renderer newMaterial = child.GetComponent<Renderer> ();
			newMaterial.sharedMaterial = hologramMaterial;
		}
		floorWorld1.sharedMaterial = newfloorMaterial;

	}
	private void DefaultScene()
	{
		j = 0;
		foreach (Transform child in  AllMaterialsWorld1 ){
			Renderer newMaterial=child.GetComponent<Renderer> ();
			newMaterial.sharedMaterial = oldMaterials[j];
			j++;
		}
		floorWorld1.sharedMaterial = oldfloorMaterial;
	}
}
