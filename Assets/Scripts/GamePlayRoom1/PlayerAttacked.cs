using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class PlayerAttacked : MonoBehaviour {
	private GameObject redFlash;
	private Slider hpSlider;
	private Slider virusSlider;
	private bool bitted;
	// Use this for initialization
	void Start () {
		redFlash = GameObject.Find ("AttackedFlash");
		redFlash.SetActive (false);
		hpSlider = GameObject.Find ("HPSlider").GetComponent<Slider>();
		virusSlider = GameObject.Find ("VirusSlider").GetComponent<Slider>();
		bitted = false;
		hpSlider.maxValue = 100;
		virusSlider.maxValue = 100;
		hpSlider.value = hpSlider.maxValue;
		virusSlider.value = virusSlider.maxValue;
	}
	
	// Update is called once per frame
	void Update () {
	      
		if (bitted && TurnController.Instance.playerMovemnet) {
			virusSlider .value -= Time.deltaTime*10;
		}
	}
	void OnCollisionEnter(Collision coll) {
		if (coll.gameObject.tag == "NormalZombie") {
			bitted = true;
			hpSlider.value -= 10;
			StartCoroutine (RedFlashShow());
		}
	}

	IEnumerator RedFlashShow()
	{
		redFlash.SetActive (true);
		yield return new WaitForSeconds (0.1f);
		redFlash.SetActive (false);
	}
}
