using UnityEngine;
using System.Collections;

public class Option : MonoBehaviour {

	public Animation anim;            //Animation end.
	public bool doneRestart;          
	public bool doneNext;
	// Update is called once per frame
	void Update () 
	{
		if (anim != null) {
			//Save score and go next level when end animation play finish.
			if (anim.isPlaying == false && doneNext) {
				doneNext = false;
				ScoreManageMent.Instance.SaveScore ();
				NextLevel ();
			}
			//Restart level when end animation play finish.
			if (anim.isPlaying == false && doneRestart) {
				doneRestart = false;
				RestartLevel ();
			}
		}
	}
	//Show end animation  and prepare for next stage. 
	public void NextStage()
	{
		anim["OpenGame"].speed = -1.0f;
		anim ["OpenGame"].time = anim ["OpenGame"].length;
		anim.gameObject.SetActive (true);
		doneNext = true;
		anim.Play ();
	}
	//Show end animation  and prepare for restart stage. 
	public void Restart()
	{
		anim["OpenGame"].speed = -1.0f;
		anim ["OpenGame"].time = anim ["OpenGame"].length;
		anim.gameObject.SetActive (true);
		doneRestart = true;
		anim.Play ();
	}
	//Reset score and  go to next stage.
	private void NextLevel()
	{
		ScoreManageMent.Instance.score = 0;
		LoadLevel.Instance.NextStage ();
	}
	//Reset score and go to restart stage.
	private void RestartLevel()
	{
		ScoreManageMent.Instance.score = 0;
		LoadLevel.Instance.ReStartStage ();
	}
	//Go scence menu.
	public void Home()
	{
		Application.LoadLevel(1);

	}
	//Exit game.
	public void Quit()
	{
		Application.Quit();
	}

}
 