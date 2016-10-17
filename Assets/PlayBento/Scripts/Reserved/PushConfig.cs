using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace PlayBento
{
	public class PushConfig : BentoConfig {
		public string AppId;
		public string GCMProjectId;
	}
}