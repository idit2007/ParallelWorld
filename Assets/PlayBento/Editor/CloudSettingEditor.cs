using Facebook;
using UnityEngine;
using UnityEditor;
using UnityEditor.FacebookEditor;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

[CustomEditor(typeof(CloudSetting))]
public class CloudSettingEditor : Editor {

	GUIStyle headTopicStyle = null;
	
	GUIContent parseLabel = new GUIContent("Parse");
    GUIContent parseServerLabel = new GUIContent("Server");
    GUIContent parseApplicationIDLabel = new GUIContent("Application ID");
	GUIContent parseDotnetKeyLabel = new GUIContent("Dotnet Key");

	private CloudSetting instance;
	
	public override void OnInspectorGUI()
	{
		instance = (CloudSetting)target;

		if(headTopicStyle == null)
		{
			headTopicStyle = new GUIStyle(GUI.skin.label);
		}
		headTopicStyle.fontStyle = FontStyle.Bold;

		ParseGUI();
	}

	private void ParseGUI()
	{
		EditorGUILayout.LabelField(parseLabel, headTopicStyle);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(parseServerLabel);
        instance.ServerLabel = EditorGUILayout.TextField(instance.ServerLabel);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField(parseApplicationIDLabel);
		instance.ApplicationIDLabel = EditorGUILayout.TextField(instance.ApplicationIDLabel);
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField(parseDotnetKeyLabel);
		instance.DotnetKeyLabel = EditorGUILayout.TextField(instance.DotnetKeyLabel);
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.Space();

		if (GUILayout.Button("Save CloudConfig"))
		{
			instance.CreateRemoteConfig();
		}
	}
}
