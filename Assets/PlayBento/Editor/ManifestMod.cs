using UnityEngine;
using UnityEditor;
using System.IO;
using System.Xml;
using System.Text.RegularExpressions;

namespace PlayBento
{
	public class ManifestMod {

		public static void Update(string fullPath)
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(fullPath);
			
			if (doc == null)
			{
				Debug.LogError("Couldn't load " + fullPath);
				return;
			}
			
			XmlNode manNode = FindChildNode(doc, "manifest");
			XmlNode dict = FindChildNode(manNode, "application");
			
			if (dict == null)
			{
				Debug.LogError("Error parsing " + fullPath);
				return;
			}

			/* No longer used since Pushwoosh was replaced by OneSignal
			string ns = dict.GetNamespaceOfPrefix("android");

			XmlElement pwAppIdElement = FindElementWithAndroidName("meta-data", "name", ns, "PW_APPID", dict);
			pwAppIdElement.SetAttribute("value", ns, GetPushInfo("PushwooshAppId"));
			
			XmlElement pwProjectIdElement = FindElementWithAndroidName("meta-data", "name", ns, "PW_PROJECT_ID", dict);
			pwProjectIdElement.SetAttribute("value", ns, "A" + GetPushInfo("GCMProjectId"));
			*/
			
			doc.Save(fullPath);

			// If bundle ID changed, replace all with the new ID and notify the user to rebuild
			if (GetLoggedBundleIdentifier () != PlayerSettings.bundleIdentifier) 
			{
				Debug.LogError("Play Bento detected Bundle Identifier change. This build might not work properly, please rebuild the project");
				File.WriteAllText (fullPath, Regex.Replace (File.ReadAllText (fullPath), GetLoggedBundleIdentifier (), PlayerSettings.bundleIdentifier));
			}
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
		
		private static XmlElement FindElementWithAndroidName(string name, string androidName, string ns, string value, XmlNode parent)
		{
			var curr = parent.FirstChild;
			while (curr != null)
			{
				if (curr.Name.Equals(name) && curr is XmlElement && ((XmlElement)curr).GetAttribute(androidName, ns) == value)
				{
					return curr as XmlElement;
				}
				curr = curr.NextSibling;
			}
			return null;
		}
		private static string GetLoggedBundleIdentifier()
		{
			string logPath = System.IO.Path.Combine(Application.dataPath, "PlayBento/Assets/log.xml");
			
			XmlDocument doc = new XmlDocument();
			doc.Load (logPath);
			XmlNode rootNode = doc.FirstChild.NextSibling;
			XmlNode bundleNode = FindChildNode(rootNode, "BundleIdentifier");
			return bundleNode.InnerText;
		}

		private static string GetPushInfo(string nodeName)
		{
			string pushConfigPath = System.IO.Path.Combine(Application.dataPath, "Resources/Katsu/PushConfig.xml");

			XmlDocument doc = new XmlDocument();
			doc.Load (pushConfigPath);
			XmlNode rootNode = doc.FirstChild.NextSibling;
			XmlNode infoNode = FindChildNode(rootNode, nodeName);
			return infoNode.InnerText;
		}
	}
}