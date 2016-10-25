using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace PlayBento
{
	public class SocialConfig : BentoConfig 
	{
		public bool PublishPermission;
		public int GiftDelay;
		public string InviteMessage;
		public List<SocialObject> Objects = new List<SocialObject>();
	}

	public class SocialObject
	{
		[XmlAttribute]
		public string Id;

		public string Picture;
		public string Link;
		public string Name;
		public string Caption;
		public string Description;
	}

}