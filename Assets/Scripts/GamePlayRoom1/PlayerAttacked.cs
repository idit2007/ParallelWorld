using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class PlayerAttacked : MonoBehaviour {
	 
	private GameObject redFlash;
	private Slider hpSlider;
	private Slider virusSlider;
	private bool bitted;
	private Text hpText;
	private Text virusText;
	// Use this for initialization
	void Start () {
		hpText = GameObject.Find ("HPText").GetComponent<Text>();
		virusText = GameObject.Find ("VirusText").GetComponent<Text>();
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
		hpText.text=hpSlider.value.ToString()+" / 100";
		virusText.text=((int)virusSlider.value).ToString()+" / 100";
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
