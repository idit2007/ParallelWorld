using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InstrutionTutorial : MonoBehaviour {
	public Text insTuText;
	// Use this for initialization
	void Awake () {
		insTuText.fontSize = 55;
	
		if (TimeScore.currentStage == 1)
			insTuText.text = "Escape the enemy to the door.\n !Notice the yellow symbol in the mini map.";
		else if (TimeScore.currentStage == 2)
			insTuText.text = "Inquiries from someome who are in the hospital.\n !Notice the yellow symbol in the mini map.";
		else if (TimeScore.currentStage == 3)
			insTuText.text = "Help someome who are in the Chemistry Laboratory.\n !Notice the yellow symbol in the mini map.";
		else if (TimeScore.currentStage == 4)
			insTuText.text = "Keep the keycard on stage.\n !Watch out for new zombies.";
		else if (TimeScore.currentStage == 5)
			insTuText.text = "Keep the keycard on table.";
		else if (TimeScore.currentStage == 6)
			insTuText.text = "Keep some keycard on table.\n !Watch out for new zombies.";
	}
	void Update()
	{
		if (TimeScore.currentStage == 0) {
			insTuText.fontSize = 70;
			if (LeaderBoard.pressStage == "Stage1") {
				insTuText.text = "บทที่ 1 ฟื้นตื่นบนดินแดงนรก \n Nova ถูกจับมาทดลองโดยองกรณ์ปริศนา แต่แล้วในขณะที่เขาอยู่ในหลอดทดลองนั่นมีบางคนได้แอบปล่อยเขาให้เป็นอิสระแต่ก็มีเงื่อนไข แม้ว่าเขาจะสงสัยและจับต้นชนปลายไม่ถูกถึงเหตุที่เขาถูกจับแต่ถึงอย่างไรเขาต้องเอาชีวิตรอดกับตัวอะไรไม่รู้ที่กำลังจะฆ่าเขาให้ได้ซะก่อน";
				TimeScore.currentStage = 1;
			} else if (LeaderBoard.pressStage == "Stage2") {
				insTuText.text = "บทที่ 2 คำขอร้องจากคนตาย.\n  Nova ได้เจอแต่กับคนที่ปล่อยตัวเขาออกมาแต่โชคร้ายที่เขาดันติดเชื้อไปแล้ว แต่เขาได้ขอร้องกับ Novaให้ช่วยลูกสาวของเขาที่อยู่ห้องเคมี.";
				TimeScore.currentStage = 2;
			} else if (LeaderBoard.pressStage == "Stage3") {
				insTuText.text = "บทที่ 3 ไม่มีใครรอดชีวิต\n  Nova มาช่วยลูกสาวของคนที่ปล่อยตัวเขาแต่เธอก็ติดเชื้อไปแล้ว และเธอยังบอกอีกด้วยว่าคนที่นี้ทั่งหมดก็ล้วนติดเชื้อไปแล้วยกเว้นตัว Nova ที่เพิ่งออกจากหลอดทดลอง แต่เธอไม่ต้องการให้ตัวซอมบี้เหล่านี้ออกไปสู่โลกภายนอกจึงวานให้ Novaไปหาใคนบางคนที่อยู่ห้องอาหาร ";
				TimeScore.currentStage = 3;
			} else if (LeaderBoard.pressStage == "Stage4") {
				insTuText.text = "บทที่ 4 Red Zombie!.\n ถึง Nova เขาจะมีพลังพิเศษ แต่เขาจะเอาตัวรอดอย่างไรกับซอมบี้พิเศษมาจากมนุษย์ทดลองที่วิ่งเร็วขององค์กร แล้วเขาจะไปห้องอาหารทันหรือเปล่า.";
				TimeScore.currentStage = 4;
			} else if (LeaderBoard.pressStage == "Stage5") {
				insTuText.text = "บทที่ 5 วางแผนระเบิดองค์กร.\n ณ ห้องอาหาร Nova ได้เจอกับวิศวกรที่รู้วิธีระเบิดองค์กรนี้ทิ้งแต่ต้องไปยังห้องควบคุมก่อน.";
				TimeScore.currentStage =5;
			} else if (LeaderBoard.pressStage == "Stage6") {
				insTuText.text = "บทที่ 6 Blue Zombie!.\n ก่อนทางไปห้องควบคุมนั่น Nova ได้เจอกับซอบบี้ตัวใหม่ ซึ่งถ้าไม่ระวังละก็ได้ตายแน่นอน";
				TimeScore.currentStage = 6;
			}
			
		}
	}

	

		
	// Update is called once per frame

}
