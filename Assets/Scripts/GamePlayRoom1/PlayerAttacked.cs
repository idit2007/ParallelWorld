using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class PlayerAttacked : MonoBehaviour {
	 
	private GameObject redFlash;
	public Slider hpSlider;
	public Slider virusSlider;
	private bool bitted;
	private bool protect;
	public Text hpText;
	public Text virusText;
	private Rigidbody rgb;
	private Vector3 v3;
	// Use this for initialization
	void Awake () {
		ScoreManageMent.getBite=false;
		hpText = GameObject.Find ("HPText").GetComponent<Text>();
		virusText = GameObject.Find ("VirusText").GetComponent<Text>();
		redFlash = GameObject.Find ("AttackedFlash");
		redFlash.SetActive (false);
		hpSlider = GameObject.Find ("HPSlider").GetComponent<Slider>();
		virusSlider = GameObject.Find ("VirusSlider").GetComponent<Slider>();
		bitted =true;
		protect = false;

		if(BuffStatus.buffStatus==1)
			hpSlider.maxValue = 500;
		else
			hpSlider.maxValue = 100;

		if(BuffStatus.buffStatus==2)
			virusSlider.maxValue = 500;
		else
			virusSlider.maxValue = 100;
		
		hpSlider.value = hpSlider.maxValue;
		virusSlider.value = virusSlider.maxValue;
		rgb = GetComponent<Rigidbody> ();
		v3 = new Vector3 (Random.Range(-10,10),0,Random.Range(-10,10));
	}

	// Update is called once per frame
	void Update () {
		hpText.text=hpSlider.value.ToString()+" / 100";
		virusText.text=((int)virusSlider.value).ToString()+" / 100";
		if (protect&& TurnController.Instance.playerMovemnet) {
			virusSlider .value -= Time.deltaTime*6;
		}
	}
	void OnCollisionEnter(Collision coll) {
		
		if (coll.gameObject.tag == "NormalZombie"&&bitted) {
			

			bitted = false;
			StartCoroutine (NormalZombieRedFlashShow());
		}
		if (coll.gameObject.tag == "RedZombie"&&bitted) {


			bitted = false;
			StartCoroutine (RedZombieRedFlashShow());
		}
		if (coll.gameObject.tag == "BlueZombie"&&bitted) {


			bitted = false;
			StartCoroutine (BlueZombieRedFlashShow());
		}
	}

	IEnumerator NormalZombieRedFlashShow()
	{
		
		yield return new WaitForSeconds (0.5f);
		redFlash.SetActive (true);
		yield return new WaitForSeconds (0.1f);
		redFlash.SetActive (false);
		hpSlider.value -= 10;
		bitted = true;
		protect = true;
		ScoreManageMent.getBite=true;

	}
	IEnumerator RedZombieRedFlashShow()
	{

		yield return new WaitForSeconds (0.5f);
		redFlash.SetActive (true);
		yield return new WaitForSeconds (0.1f);
		redFlash.SetActive (false);
		hpSlider.value -= 30;
		bitted = true;
		protect = true;
		ScoreManageMent.getBite=true;
	}
	IEnumerator BlueZombieRedFlashShow()
	{

		yield return new WaitForSeconds (0.5f);
		redFlash.SetActive (true);
		rgb.AddForce (v3);
		yield return new WaitForSeconds (0.1f);
		redFlash.SetActive (false);
		hpSlider.value -= 100;
		bitted = true;
		protect = true;
		ScoreManageMent.getBite=true;
	}
}
