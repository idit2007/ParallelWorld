using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace PlayBento
{
	public class CloudConfig : BentoConfig {

        public string Server = "";
		public string ApplicationID = "";
		public string DotNetKey = "";
	}
}