using UnityEngine;
using System.Collections;
using PlayBento;
using UnityEngine.UI;
public class DetailManagement : MonoBehaviour {
	public Text textGem;      			//Detail gem.
	public Text textBlock;				//Detail block.
	//Use this for config.
	private DetailProfile dp;
	private ScoreData sd;
	private DetailData dd;
	// Use this for initialization

	void Start()
	{
		//Config detail each stages.
		PB.Init ();
		dp = Local.GetProfile (typeof(DetailProfile)) as DetailProfile;
		dd=dp.DetailList[CurrentStage.nowStage-1];
		textGem.text = dd.gemDetail;
		textBlock.text = dd.blockDetail;
	}
}
