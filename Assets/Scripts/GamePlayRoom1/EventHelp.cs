using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventHelp : MonoBehaviour {

	public Text popupText;
	public SphereCollider door1;
	public SphereCollider door2;
	public GameObject target1;
	public GameObject target2;
	private GameObject popup;
	private GameObject blackPanel;
	private Collider collKey;
	void Start()
	{
		popup = GameObject.Find ("PopUp");
		blackPanel = GameObject.Find ("BlackPanel");
		collKey = GetComponent<Collider> ();
	}
	void OnTriggerEnter(Collider coll) {

		if (coll.gameObject.tag == "Player") {
			blackPanel.SetActive (true);
			EventHlepNovel ();
			popup.SetActive (true);
			if(door1!=null)
			door1.enabled = true;
			if(door2!=null)
			door2.enabled = true;
			if(target1!=null)
			target1.SetActive (true);
			if(target2!=null)
			target2.SetActive (true);
			collKey.enabled = false;
			this.gameObject.SetActive (false);
		}
	}
	private void EventHlepNovel()
	{
		popupText.fontSize = 45;
		 if (TimeScore.currentStage == 2)
			popupText.text = "ขอบใจที่มานะแต่ฉันคงไม่รอดแล้ว โปรดไปช่วยลูกสาวฉันที่แลปเคมีที ส่วนเรื่องของนายนั่น... แอ๊ก(กระอักเลือดตาย)";
		else if (TimeScore.currentStage == 3)
			popupText.text = "แม่ฉันส่งนายมาหรอ แต่ฉันก็คงไม่รอดเหมือนกันและทุกคนก็คงติดเชื้อหมดแล้ว นายไปช่วยไปหาวิศวกรที่อยู้ห้องอาหารนะเขารู้ต้องทำอย่างไร";
		else if (TimeScore.currentStage == 5)
			popupText.text = "นายเองสินะ เธอบอกฉันว่านายจะมา เอานี้ไปนี้คือวิธีการเปิดระบบควบคุมระเบิด แต่นายต้องไปห้องควบคุมเพื่ออนุมัติ เร็วเข้า";

	}
}
