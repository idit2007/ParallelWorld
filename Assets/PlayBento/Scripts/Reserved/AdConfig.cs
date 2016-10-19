using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace PlayBento
{
	public class AdConfig : BentoConfig {
		public AdPlatform iOS = new AdPlatform ();
		public AdPlatform Android = new AdPlatform ();
	}

	public class AdPlatform{
		public AdMobUnit AdMob = new AdMobUnit ();
		public ChartboostUnit Chartboost = new ChartboostUnit();
		public UnityUnit Unity = new UnityUnit();
		public AdColonyUnit AdColony = new AdColonyUnit(); 
	}

	public class GenericAdUnit {
		[XmlAttribute]
		public int priority;
	}

	public class AdMobUnit : GenericAdUnit{
		public string AdUnit;
	}

	public class ChartboostUnit : GenericAdUnit{
	}

	public class UnityUnit : GenericAdUnit{
		public string AppId;
	}

	public class AdColonyUnit : GenericAdUnit{
		public string AppId;
		public string ZoneId;
	}
}