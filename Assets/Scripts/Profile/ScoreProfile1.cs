using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace PlayBento
{
	public class ScoreProfile : BentoProfile {
		public List<ScoreData> ScoreList = new List<ScoreData> ();
	}

	public class ScoreData{
		public int NumberStage;
		public float time;
		public int Score;
	}

	public class DownlodeProfile : BentoProfile {
		public List<DownlodeData> DownlodeList = new List<DownlodeData> ();
	}

	public class DownlodeData{
		public int NumberStage;
		public string NameStage;
		public float time;
		public int Score;
	}
}