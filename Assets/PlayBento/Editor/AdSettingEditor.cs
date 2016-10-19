using Facebook;
using UnityEngine;
using UnityEditor;
using UnityEditor.FacebookEditor;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

[CustomEditor(typeof(AdSetting))]
public class AdSettingEditor : Editor {

	GUIStyle headTopicStyle = null;
	
	GUIContent admobTopicLabel = new GUIContent("AdMob");
	GUIContent admobAndroidPubIDLabel = new GUIContent("Android Publisher ID");
	GUIContent admobIosPubIDLabel = new GUIContent("iOS Publisher ID");
	GUIContent admobPriorityLabel = new GUIContent("Priority [?]", "Priority, 1 is highest");

	GUIContent chartboostTopicLabel = new GUIContent("Chartboost");
	GUIContent chartboostPriorityLabel = new GUIContent("Priority [?]", "Priority, 1 is highest");

	GUIContent unityTopicLabel = new GUIContent("Unity");
	GUIContent unityAndroidAppIDLabel = new GUIContent("Android Unity App ID");
	GUIContent unityIosAppIDLabel = new GUIContent("iOS Unity App ID");
	GUIContent unityPriorityLabel = new GUIContent("Priority [?]", "Priority, 1 is highest");

	GUIContent adColonyTopicLabel = new GUIContent("AdColony");
	GUIContent adColonyAndroidAppIDLabel = new GUIContent("Android App ID");
	GUIContent adColonyAndroidZoneIDLabel = new GUIContent("Android Zone ID");
	GUIContent adColonyIosAppIDLabel = new GUIContent("iOS App ID");
	GUIContent adColonyIosZoneIDLabel = new GUIContent("iOS Zone ID");
	GUIContent adColonyPriorityLabel = new GUIContent("Priority [?]", "Priority, 1 is highest");

	private AdSetting instance;
	
	public override void OnInspectorGUI()
	{
		instance = (AdSetting)target;

		if(headTopicStyle == null)
		{
			headTopicStyle = new GUIStyle(GUI.skin.label);
		}
		headTopicStyle.fontStyle = FontStyle.Bold;

		AdmobGUI();
		ChartboostGUI();
		UnityGUI ();	
		AdColonyGUI();

		if (GUILayout.Button("Save AdConfig"))
		{
			instance.CreateAdConfig();
		}
	}

	private void AdmobGUI()
	{
		EditorGUILayout.LabelField(admobTopicLabel, headTopicStyle);

		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField(admobAndroidPubIDLabel);
		instance.AdmobAndroidPublisherID = EditorGUILayout.TextField(instance.AdmobAndroidPublisherID);
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField(admobIosPubIDLabel);
		instance.AdmobIosPublisherID = EditorGUILayout.TextField(instance.AdmobIosPublisherID);
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField(admobPriorityLabel);
		instance.AdmobPriority = EditorGUILayout.IntField(instance.AdmobPriority);
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.Space();
	}

	private void ChartboostGUI()
	{
		EditorGUILayout.LabelField(chartboostTopicLabel, headTopicStyle);

		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField(chartboostPriorityLabel);
		instance.ChartboostPriority = EditorGUILayout.IntField(instance.ChartboostPriority);
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.Space();
	}

	private void UnityGUI()
	{
		EditorGUILayout.LabelField(unityTopicLabel, headTopicStyle);
		
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField(unityAndroidAppIDLabel);
		instance.UnityAndroidAppID = EditorGUILayout.TextField(instance.UnityAndroidAppID);
		EditorGUILayout.EndHorizontal();
		
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField(unityIosAppIDLabel);
		instance.UnityIosAppID = EditorGUILayout.TextField(instance.UnityIosAppID);
		EditorGUILayout.EndHorizontal();
		
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField(unityPriorityLabel);
		instance.UnityPriority = EditorGUILayout.IntField(instance.UnityPriority);
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.Space();
	}

	private void AdColonyGUI()
	{
		EditorGUILayout.LabelField(adColonyTopicLabel, headTopicStyle);
		
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField(adColonyAndroidAppIDLabel);
		instance.AdColonyAndroidAppID = EditorGUILayout.TextField(instance.AdColonyAndroidAppID);
		EditorGUILayout.EndHorizontal();
		
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField(adColonyAndroidZoneIDLabel);
		instance.AdColonyAndroidZoneID = EditorGUILayout.TextField(instance.AdColonyAndroidZoneID);
		EditorGUILayout.EndHorizontal();
		
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField(adColonyIosAppIDLabel);
		instance.AdColonyIosAppID = EditorGUILayout.TextField(instance.AdColonyIosAppID);
		EditorGUILayout.EndHorizontal();
		
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField(adColonyIosZoneIDLabel);
		instance.AdColonyIosZoneID = EditorGUILayout.TextField(instance.AdColonyIosZoneID);
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField(adColonyPriorityLabel);
		instance.AdColonyPriority = EditorGUILayout.IntField(instance.AdColonyPriority);
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.Space();
	}
}
