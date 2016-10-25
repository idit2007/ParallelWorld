using UnityEngine;
using System.Collections;

public class FloatObject : MonoBehaviour {
	private static FloatObject instance;
	public bool isFloat;                                                                        //use singleton.
	public static FloatObject Instance
	{
		get {
			return instance;
		}
	}
	void Awake()
	{
		instance = this;
		isFloat = true;
	}
	bool   objectFloatUp;  
	float  topSavePoint=1.3f;
	float  botSavePoint=0.3f;
	int speedFloating=2;
	// Update is called once per frame
	void Update ()
	{
		 //Make object move between  bot and top of save point area.
		if (isFloat)
		{
			if (this.transform.localPosition.y > topSavePoint) {

				objectFloatUp = false;
			}

			if (this.transform.localPosition.y < botSavePoint) {

				objectFloatUp = true;
			}
			if (objectFloatUp) {
				this.transform.Translate (0, Time.deltaTime * speedFloating, 0);
			}
			if (!objectFloatUp) {
				this.transform.Translate (0, -Time.deltaTime, 0);
			}
		 }
	}


	
}
