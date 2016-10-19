using UnityEngine;
using System.Collections;
using PlayBento;

public class Login : MonoBehaviour {
	public GameObject sl;
	// Use this for initialization
	void Start () {
		PB.Init ();
	}
	//Go to menu scence.

	private void LoginCallBack(bool success)
	{
		Application.LoadLevel(1);
	}
	public void Restart()
	{
		Application.LoadLevel(Application.loadedLevel);
	}
	public void LoginWithFacebook()
	{
		
		PlayBento.Social.Login (LoginCallBack);   //Login with facebook.
	}
		
}

