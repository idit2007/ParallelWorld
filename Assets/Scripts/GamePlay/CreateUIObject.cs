using UnityEngine;
using System.Collections;

public class CreateUIObject : MonoBehaviour {
	private GameObject createBlock;                             //Save array list's blocks.
	private string UIString;
	//use singleton.
	private static CreateUIObject  instance;
	public static CreateUIObject  Instance
	{
		get 
		{
		return instance;
		}
	}

	void Awake()
	{
		instance = this;
	}
	// Create first block.
	void Start () 
	{
		UIString = "PrefabsBlock/" + CreateObject.Instance.objArray[1].name + "UI";     //Check name UI from array list's blocks.
		createBlock  = ((GameObject)Instantiate (Resources.Load (UIString)));
	}
	//Change UI's block by destroy old block and create new block .
	public void ChageUIBlock()
	{
		if (createBlock != null)
			Destroy (createBlock);	
		if (CreateObject.Instance.currentBlock + 1 < CreateObject.Instance.objArray.Length) {
			UIString = "PrefabsBlock/" + CreateObject.Instance.objArray [CreateObject.Instance.currentBlock + 1].name + "UI";     //Check name UI from array list's blocks.
			createBlock = ((GameObject)Instantiate (Resources.Load (UIString)));
		}
	}

}
