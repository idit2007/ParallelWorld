using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Option : MonoBehaviour {

	public void BacktoMap()
	{
		StartCoroutine (Map());

	}
	public void PlayAgain()
	{
		StartCoroutine (Again());
	}
	public void ExitGame()
	{
		Application.Quit ();
	}
	IEnumerator Map()
	{
		yield return new WaitForSeconds (0.5f);
		SceneManager.LoadScene ("Menu");
	}
	IEnumerator Again()
	{
		yield return new WaitForSeconds (0.5f);

		SceneManager.LoadScene ("Stage"+TimeScore.currentStage.ToString());
	}

}
