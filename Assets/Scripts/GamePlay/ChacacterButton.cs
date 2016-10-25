using UnityEngine;
using System.Collections;

public class ChacacterButton : MonoBehaviour {
	private GameObject character;			//Chacter's button
	private GameObject setting;              //Setting's button
	private GameObject invisiblePanel;        
	public Animator anim;
	private bool done;
	void Start()
	{     
		 //initual with game object name in scene.
		character = GameObject.FindGameObjectWithTag ("Character");
		setting = GameObject.Find ("Setting");
		invisiblePanel = GameObject.Find ("InvisiblePanel");

	}
	protected virtual void OnEnable()
	{
		// Hook into the OnFingerTap event
		Lean.LeanTouch.OnFingerTap += OnFingerTap;
	}

	protected virtual void OnDisable()
	{
		// Unhook into the OnFingerTap event
		Lean.LeanTouch.OnFingerTap -= OnFingerTap;
	}

	public void OnFingerTap(Lean.LeanFinger finger)
	{
		RaycastHit2D hit;
		hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (finger.ScreenPosition), Vector2.zero);
		//Tap character button  to ready stage and change tag .
		if (hit.transform.gameObject.tag=="CharacterAnimation") 
		{
			hit.transform.gameObject.tag = "Character";
			anim.SetBool("cutScence", true);
		}
		//Tap character button to cut scene stage.
		else if (hit.transform.gameObject.tag=="Character") 
		{
			//Close UI.
			invisiblePanel.SetActive (true);
			setting.SetActive (false);
			RotatonControl.Instance.DropGem ();
		}
		//Cancel arvy shooting stage.
		else if (hit.transform.gameObject.tag=="BG") 
		{
			anim.SetBool("cutScence", false);
			character = GameObject.FindGameObjectWithTag ("Character");
			if (character != null) {
				character.tag = "CharacterAnimation";
			}
		}
	}
}
