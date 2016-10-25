using UnityEngine;
using System.Collections;

public class LoadLevel : MonoBehaviour {
	//Singleton
	private static LoadLevel instance;
	public static LoadLevel Instance
	{
		get {


			return instance;
		}
	}

	void Awake()
	{
		instance = this;
	
	}
	//Go next stage. 
	public void NextStage()
	{
		
		CurrentStage.nowStage += 1;
		if(CurrentStage.nowStage%2==1)
			Application.LoadLevel(2);
		else 
			Application.LoadLevel(3);
	
	}
	//Restart stage.
	public void ReStartStage()
	{
		if(CurrentStage.nowStage%2==1)
			Application.LoadLevel(2);
		else 
			Application.LoadLevel(3);
		CurrentStage.nowStage = CurrentStage.nowStage;

	}

	// Update is called once per frame
	/*
	void Update () {
	
	}
	public void LoadStage1()
	{

		CurrentStage.nowStage=1;
		Application.LoadLevel(2);
	}
	public void LoadStage2()
	{
		CurrentStage.nowStage=2;
		Application.LoadLevel(2);

	}
	public void LoadStage3()
	{
		CurrentStage.nowStage=3;
		Application.LoadLevel(2);

	}
	public void LoadStage4()
	{
		CurrentStage.nowStage=4;
		Application.LoadLevel(2);

	}
	public void LoadStage5()
	{
		CurrentStage.nowStage=5;
		Application.LoadLevel(2);

	}
	public void LoadStage6()
	{
		CurrentStage.nowStage=6;
		Application.LoadLevel(2);

	}
	public void LoadStage7()
	{
		CurrentStage.nowStage=7;
		Application.LoadLevel(2);

	}
	public void LoadStage8()
	{
		CurrentStage.nowStage=8;
		Application.LoadLevel(2);

	}
	public void LoadStage9()
	{
		CurrentStage.nowStage=9;
		Application.LoadLevel(2);

	}
	public void LoadStage10()
	{
		CurrentStage.nowStage=10;
		Application.LoadLevel(2);

	}
	public void LoadStage11()
	{

		CurrentStage.nowStage=11;
		Application.LoadLevel(2);
	}
	public void LoadStage12()
	{
		CurrentStage.nowStage=12;
		Application.LoadLevel(2);
	
	}
	public void LoadStage13()
	{
		CurrentStage.nowStage=13;
		Application.LoadLevel(2);
	
	}
	public void LoadStage14()
	{
		CurrentStage.nowStage=14;
		Application.LoadLevel(2);

	}
	public void LoadStage15()
	{
		CurrentStage.nowStage=15;
		Application.LoadLevel(2);

	}
	public void LoadStage16()
	{
		CurrentStage.nowStage=16;
		Application.LoadLevel(2);
	
	}
	public void LoadStage17()
	{
		CurrentStage.nowStage=17;
		Application.LoadLevel(2);
	
	}
	public void LoadStage18()
	{
		CurrentStage.nowStage=18;
		Application.LoadLevel(2);

	}
	public void LoadStage19()
	{
		CurrentStage.nowStage=19;
		Application.LoadLevel(2);
	
	}
	public void LoadStage20()
	{
		CurrentStage.nowStage=20;
		Application.LoadLevel(2);

	}
	public void LoadStage21()
	{

		CurrentStage.nowStage=21;
		Application.LoadLevel(2);
	}
	public void LoadStage22()
	{
		CurrentStage.nowStage=22;
		Application.LoadLevel(2);

	}
	public void LoadStage23()
	{
		CurrentStage.nowStage=23;
		Application.LoadLevel(2);

	}
	public void LoadStage24()
	{
		CurrentStage.nowStage=24;
		Application.LoadLevel(2);

	}
	public void LoadStage25()
	{
		CurrentStage.nowStage=25;
		Application.LoadLevel(2);

	}
	public void LoadStage26()
	{
		CurrentStage.nowStage=26;
		Application.LoadLevel(2);

	}
	public void LoadStage27()
	{
		CurrentStage.nowStage=27;
		Application.LoadLevel(2);

	}
	public void LoadStage28()
	{
		CurrentStage.nowStage=28;
		Application.LoadLevel(2);

	}
	public void LoadStage29()
	{
		CurrentStage.nowStage=29;
		Application.LoadLevel(2);

	}
	public void LoadStage30()
	{
		CurrentStage.nowStage=30;
		Application.LoadLevel(2);

	}
	*/

}
