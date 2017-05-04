using UnityEngine;
using UnityEngine.Advertisements;

public class ADsUnity : MonoBehaviour {

	public void buffHp()
	{
		if (Advertisement.IsReady())
		{
			Advertisement.Show();
			BuffStatus.buffStatus=1;
		}
	}

	public void buffvirus()
	{
		if (Advertisement.IsReady())
		{
			Advertisement.Show();
			BuffStatus.buffStatus=2;
		}
	}


	public void buffspeed()
	{
		if (Advertisement.IsReady())
		{
			Advertisement.Show();
			BuffStatus.buffStatus=3;
		}
	}
}
