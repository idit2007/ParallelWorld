using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
#if UNITY_IOS
using UnityEditor.iOS.Xcode;
#endif

namespace PlayBento
{
	public static class XCodePostProcess
	{
		[PostProcessBuild]
		public static void OnPostProcessBuild(BuildTarget buildTarget, string path)
		{
			if (typeof(Katsu<>) == null) 
			{
				Debug.LogError ("Katsu is missing. Bento is not tasty!");
				return;
			}
			
			if (buildTarget == BuildTarget.iOS)
			{
				#if UNITY_IOS
				string projPath = PBXProject.GetPBXProjectPath(path);
           		PBXProject project = new PBXProject();
				project.ReadFromString(File.ReadAllText(projPath));
				string target = project.TargetGuidByName(PBXProject.GetUnityTargetName());
				
				project.AddFrameworkToProject(target, "CoreTelephony.framework", true);
				project.AddFrameworkToProject(target, "EventKit.framework", false);
				project.AddFrameworkToProject(target, "MessageUI.framework", true);
				project.AddFrameworkToProject(target, "StoreKit.framework", true);
				project.AddFrameworkToProject(target, "Webkit.framework", true);
				project.AddFrameworkToProject(target, "Social.framework", true);
				project.AddFrameworkToProject(target, "Accounts.framework", true);
				
				project.SetBuildProperty(target, "ENABLE_BITCODE", "NO");
				project.SetBuildProperty(target, "CLANG_ENABLE_MODULES", "YES");
				project.AddBuildProperty(target, "OTHER_LDFLAGS", "-ObjC");
				project.AddBuildProperty(target, "OTHER_LDFLAGS", "-lz");
				
				File.WriteAllText(projPath, project.WriteToString());
				#endif
				
				PlistMod.Update(path);
			}
			
			if (buildTarget == BuildTarget.Android)
			{
				// The default Bundle Identifier for Unity does magical things that causes bad stuff to happen
				/*
				if (PlayerSettings.bundleIdentifier == "com.Company.ProductName")
				{
					Debug.LogError("The default Unity Bundle Identifier (com.Company.ProductName) will not work correctly.");
				}
				*/
				ManifestMod.Update(Path.Combine(Application.dataPath, "Plugins/Android/AndroidManifest.xml"));
			}

			CreateLog();
		}

		private static void CreateLog()
		{
			string logPath = System.IO.Path.Combine(Application.dataPath, "PlayBento/Assets/log.xml");
			if(File.Exists(logPath)){File.Delete(logPath);}
			
			XmlDocument doc = new XmlDocument();
			XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
			doc.AppendChild(docNode);

			XmlNode logNode = doc.CreateElement("Log");
			doc.AppendChild(logNode);
			
			XmlNode bundleNode = doc.CreateElement("BundleIdentifier");
			bundleNode.InnerText = PlayerSettings.bundleIdentifier;
			logNode.AppendChild(bundleNode);

			doc.Save(logPath);
		}
	}
}
