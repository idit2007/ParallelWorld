using UnityEngine;
using System.Collections;

public class ShaderManagement : MonoBehaviour {
	public Material[] oldMaterialsWorld1;
	public Material[] oldMaterialsWorld2;
	public Transform AllMaterialsWorld1;
	public Transform AllMaterialsWorld2;
	public Renderer floorWorld1;
	public Renderer floorWorld2;
	public Material BlackfloorMaterial;
	public Material floorMaterialWorld1;
	public Material floorMaterialWorld2;
	public Material world1;
	public Material world2;
	private GameObject effect;
	private GameObject allMaterialHoloWorld1;
	private GameObject allMaterialHoloWorld2;
	private int i;
	private int j;
	public float alpha;
	private bool holo;
	// Use this for initialization
	void Awake()
	{
		allMaterialHoloWorld1 = GameObject.Find ("AllMaterialsHoloWorld1");
		allMaterialHoloWorld2 = GameObject.Find ("AllMaterialsHoloWorld2");
		allMaterialHoloWorld1.SetActive (false);
		allMaterialHoloWorld2.SetActive (false);
	}
	void Start () {
		alpha = 0;
		j = 0;
		holo = false;
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
	void Update()
	{
		
		if (holo) {
			if (alpha < 1)
				alpha += Time.deltaTime ;

		}
		else
		{
			if(alpha>0)
			alpha -= Time.deltaTime ;
		}
			world1.SetFloat( "_ColorIntensity", alpha );
		world2.SetFloat( "_ColorIntensity", alpha );
		if (EffectPlayerSlowMotion.Instance.playerMove&&EffectPlayerSlowMotion.Instance.done1) {
			StartCoroutine (WaitEffectChangeshaderBack());
			EffectPlayerSlowMotion.Instance.done1 = false;
		}
	}
	// Update is called once per frame

	private void HoloGramScene()
	{
		
		allMaterialHoloWorld1.SetActive (true);
		allMaterialHoloWorld2.SetActive (true);




		foreach (Transform child in  AllMaterialsWorld1) {
			Renderer newMaterialWorld1 = child.GetComponent<Renderer> ();
			newMaterialWorld1.sharedMaterial = world1;
		   
		}
		foreach (Transform child in  AllMaterialsWorld2) {
			Renderer newMaterialWorld2 = child.GetComponent<Renderer> ();
			newMaterialWorld2.sharedMaterial = world2;
		}
  
		floorWorld1.sharedMaterial = BlackfloorMaterial;
		floorWorld2.sharedMaterial = BlackfloorMaterial;

	}
	private void DefaultScene()
	{
		j = 0;
		allMaterialHoloWorld1.SetActive (false);
		allMaterialHoloWorld2.SetActive (false);

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

		floorWorld1.sharedMaterial = floorMaterialWorld1;
		floorWorld2.sharedMaterial = floorMaterialWorld2;
	}

	public void TeleportionChangeShader()
	{
		EffectPlayerSlowMotion.Instance.done1 = true;
		EffectPlayerSlowMotion.Instance.playerMove = false;

		StartCoroutine (WaitEffectChangeshader());

	}

	IEnumerator WaitEffectChangeshader()
	{


		yield return new WaitForSeconds (0.2f);
		HoloGramScene ();
		holo = true;

	}
	IEnumerator WaitEffectChangeshaderBack()
	{
		yield return new WaitForSeconds (0.5f);
		DefaultScene ();
		holo = false;

	}
}

