using UnityEngine;
using System.IO;
using System.Xml;

namespace PlayBento
{
	public class PlistMod {

		public static void Update(string path)
		{
			const string fileName = "Info.plist";
			string fullPath = Path.Combine(path, fileName);
			
			var doc = new XmlDocument();
			doc.Load(fullPath);
			
			var dict = FindPlistDictNode(doc);
			if(dict == null)
			{
				Debug.LogError("Error parsing " + fullPath);
				return;
			}

			/* No longer used since Pushwoosh was replaced by OneSignal
			if(!HasKey(dict, "Pushwoosh_APPID"))
			{
				AddChildElement(doc, dict, "key", "Pushwoosh_APPID");
				AddChildElement(doc, dict, "string", GetPushInfo("PushwooshAppId"));
			}
			*/
			doc.Save(fullPath);
		}

		private static XmlNode FindPlistDictNode(XmlDocument doc)
		{
			var curr = doc.FirstChild;
			while(curr != null)
			{
				if(curr.Name.Equals("plist") && curr.ChildNodes.Count == 1)
				{
					var dict = curr.FirstChild;
					if(dict.Name.Equals("dict"))
						return dict;
				}
				curr = curr.NextSibling;
			}
			return null;
		}

		private static XmlElement AddChildElement(XmlDocument doc, XmlNode parent, string elementName, string innerText=null)
		{
			var newElement = doc.CreateElement(elementName);
			if(!string.IsNullOrEmpty(innerText))
				newElement.InnerText = innerText;
			
			parent.AppendChild(newElement);
			return newElement;
		}
		
		private static bool HasKey(XmlNode dict, string keyName)
		{
			var curr = dict.FirstChild;
			while(curr != null)
			{
				if(curr.Name.Equals("key") && curr.InnerText.Equals(keyName))
					return true;
				curr = curr.NextSibling;
			}
			return false;
		}

		private static string GetPushInfo(string nodeName)
		{
			Debug.Log (nodeName);
			string pushConfigPath = System.IO.Path.Combine(Application.dataPath, "Resources/Katsu/PushConfig.xml");
			
			XmlDocument doc = new XmlDocument();
			doc.Load (pushConfigPath);
			XmlNode rootNode = doc.FirstChild.NextSibling;
			XmlNode infoNode = FindChildNode(rootNode, nodeName);
			return infoNode.InnerText;
		}

		private static XmlNode FindChildNode(XmlNode parent, string name)
		{
			XmlNode curr = parent.FirstChild;
			while (curr != null)
			{
				if (curr.Name.Equals(name))
				{
					return curr;
				}
				curr = curr.NextSibling;
			}
			return null;
		}
	}
}