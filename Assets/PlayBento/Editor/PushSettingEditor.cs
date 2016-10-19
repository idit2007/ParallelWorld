using Facebook;
using UnityEngine;
using UnityEditor;
using UnityEditor.FacebookEditor;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

[CustomEditor(typeof(PushSetting))]
public class PushSettingEditor : Editor {

	GUIStyle headTopicStyle = null;
	
	GUIContent topicLabel = new GUIContent("Push");
	GUIContent applicationIDLabel = new GUIContent("Application ID");
	GUIContent gcmProjectIDLabel = new GUIContent("GCM Project ID");

	private PushSetting instance;
	
	public override void OnInspectorGUI()
	{
		instance = (PushSetting)target;

		if(headTopicStyle == null)
		{
			headTopicStyle = new GUIStyle(GUI.skin.label);
		}
		headTopicStyle.fontStyle = FontStyle.Bold;

		PushGUI();
	}

	private void PushGUI()
	{
		EditorGUILayout.LabelField(topicLabel, headTopicStyle);

		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField(applicationIDLabel);
		instance.ApplicationIDLabel = EditorGUILayout.TextField(instance.ApplicationIDLabel);
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField(gcmProjectIDLabel);
		instance.GCMProjectID = EditorGUILayout.TextField(instance.GCMProjectID);
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.Space();

		if (GUILayout.Button("Save PushConfig"))
		{
			instance.CreatePushConfig();
		}
	}
}
