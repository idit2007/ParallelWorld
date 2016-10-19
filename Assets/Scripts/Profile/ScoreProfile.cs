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
		public int Stars;
		public float height;
		public int Score;
	}
}