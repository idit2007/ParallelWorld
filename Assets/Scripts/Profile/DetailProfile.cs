using UnityEngine;
using System.Collections;
using System.Collections.Generic;


	namespace PlayBento
	{
	   public class DetailProfile : BentoProfile {
		public List<DetailData> DetailList = new List<DetailData> ();
	}

		public class DetailData
	   {
			public int NumberStage;
		    public string gemDetail;
		    public string blockDetail;
			

	   }
}