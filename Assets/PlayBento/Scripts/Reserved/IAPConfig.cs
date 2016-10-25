using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace PlayBento
{
	public class IAPConfig : BentoConfig {
		public string GoogleLicenseKey;
		public List<IAPItem> Items = new List<IAPItem>();
	}

	public class IAPItem{
		[XmlAttribute]
		public string Id;

		public string Title;
		public string Description;
		public float Price;
		public bool Consumable;
	}
}