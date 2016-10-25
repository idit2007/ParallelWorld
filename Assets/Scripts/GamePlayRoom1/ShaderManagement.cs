using UnityEngine;
using System.Collections;

public class ShaderManagement : MonoBehaviour {
	public Material[] oldMaterialsWorld1;
	public Material[] oldMaterialsWorld2;
	public Material hologramMaterialWorld1;
	public Material hologramMaterialWorld2;
	public Transform AllMaterialsWorld1;
	public Transform AllMaterialsWorld2;
	public Renderer floorWorld1;
	public Renderer floorWorld2;
	public Material newfloorMaterial;
	public Material oldfloorMaterial;
	private GameObject effect;
	private GameObject allMaterialHoloWorld1;
	private GameObject allMaterialHoloWorld2;
	private GameObject preFps;
	private int i;
	private int j;
	private bool done1;
	private bool done2;
	// Use this for initialization
	void Start () {
		done1 = false;
		done2 = false;
		allMaterialHoloWorld1 = GameObject.Find ("AllMaterialsHoloWorld1");
		allMaterialHoloWorld2 = GameObject.Find ("AllMaterialsHoloWorld2");
		allMaterialHoloWorld1.SetActive (false);
		allMaterialHoloWorld2.SetActive (false);
		preFps = GameObject.Find ("PreFps");
		preFps.SetActive (false);
		//effect = GameObject.Find ("Capsule");
	//	effect.SetActive (false);
		i = 0;
		oldMaterialsWorld1=new Material[AllMaterialsWorld1.childCount];
		oldMaterialsWorld2=new Material[AllMaterialsWorld2.childCount];
		foreach (Transform child in  AllMaterialsWorld1 ){
			Renderer newMaterialWorld1=child.GetComponent<Renderer> ();
		
			oldMaterialsWorld1[i] = newMaterialWorld1.sharedMaterial;
			i++;
		}
		i = 0;
		foreach (Transform child in  AllMaterialsWorld2 ){
			Renderer newMaterialWorld2=child.GetComponent<Renderer> ();
			oldMaterialsWorld2[i] = newMaterialWorld2.sharedMaterial;
			i++;
		}

	}
	
	// Update is called once per frame
	void Update () {
		if (TurnController.Instance.playerMovemnet && done1) {
			DefaultScene ();
			done1 = false;
		}
		else if (!TurnController.Instance.playerMovemnet && done2)
		{
			StartCoroutine (WaitEffectChangeshader ());
			done2 = false;
			done1 = true;
		}
	
	}
	private void HoloGramScene()
	{
		allMaterialHoloWorld1.SetActive (true);
		allMaterialHoloWorld2.SetActive (true);
		foreach (Transform child in  AllMaterialsWorld1) {
			Renderer newMaterialWorld1 = child.GetComponent<Renderer> ();
			newMaterialWorld1.sharedMaterial = hologramMaterialWorld1;
		   
		}
		foreach (Transform child in  AllMaterialsWorld2) {
			Renderer newMaterialWorld2 = child.GetComponent<Renderer> ();
			newMaterialWorld2.sharedMaterial = hologramMaterialWorld2;
		}

		floorWorld1.sharedMaterial = newfloorMaterial;
		floorWorld2.sharedMaterial = newfloorMaterial;

	}
	private void DefaultScene()
	{
		allMaterialHoloWorld1.SetActive (false);
		allMaterialHoloWorld2.SetActive (false);
		j = 0;
		foreach (Transform child in  AllMaterialsWorld1 ){
			Renderer newMaterialWorld1=child.GetComponent<Renderer> ();
			newMaterialWorld1.sharedMaterial = oldMaterialsWorld1[j];
			j++;
		}
		j = 0;
		foreach (Transform child in  AllMaterialsWorld2 ){
			Renderer newMaterialWorld2=child.GetComponent<Renderer> ();
			newMaterialWorld2.sharedMaterial = oldMaterialsWorld2[j];
			j++;
		}
		floorWorld1.sharedMaterial = oldfloorMaterial;
		floorWorld2.sharedMaterial = oldfloorMaterial;
	}
	IEnumerator EffectChangeShaderHoloGram()
	{

		//effect.SetActive (true);
		yield return new WaitForSeconds (2f);
	
		//effect.SetActive (false);
	}
	public void TeleportionChangeShader()
	{
		done2 = true;
	}

	IEnumerator WaitEffectChangeshader()
	{

		//zoom = true;
		//uIFPS.SetActive (true);
		yield return new WaitForSeconds (3f);
		preFps.SetActive (true);
		//	changeShaderArea.SetActive (true);
		yield return new WaitForSeconds (0.5f);
		//	uIFPS.SetActive (true);
		preFps.SetActive (false);
		HoloGramScene ();
		//mapWord1.enabled = true;
		//mapWord2.enabled = true;
		//thirdPersonCamera.SetActive (false);
	}

}

