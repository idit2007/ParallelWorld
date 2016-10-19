using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(HoloManager))]
[CanEditMultipleObjects]
public class HoloManagerEditor : Editor {

	SerializedProperty patternVerticalAnimation, patternVerticalSpeed;
	SerializedProperty patternHorizontalAnimation, patternHorizontalSpeed;
	SerializedProperty noise, noiseSpeed;
	SerializedProperty animateColorIntensity, intensitySpeed, intensityRange;


	Texture2D CHSlogo;
	GUIStyle CHSstyle;

	void OnEnable() {
		patternVerticalAnimation = serializedObject.FindProperty("patternVerticalAnimation");
		patternVerticalSpeed = serializedObject.FindProperty("patternVerticalSpeed");

		patternHorizontalAnimation = serializedObject.FindProperty("patternHorizontalAnimation");
		patternHorizontalSpeed = serializedObject.FindProperty("patternHorizontalSpeed");

		noise = serializedObject.FindProperty("noise");
		noiseSpeed = serializedObject.FindProperty("noiseSpeed");

		animateColorIntensity = serializedObject.FindProperty("animateColorIntensity");
		intensitySpeed = serializedObject.FindProperty("intensitySpeed");
		intensityRange = serializedObject.FindProperty("intensityRange");


		CHSlogo = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/HologramShader/Hologram-Shader-Logo.png", typeof(Texture2D));
		CHSstyle = new GUIStyle();
		CHSstyle.fontSize = 20;
		CHSstyle.normal.textColor = Color.white;
		CHSstyle.fontStyle = FontStyle.BoldAndItalic;
	}

	public override void OnInspectorGUI() {
		serializedObject.Update();

		EditorGUILayout.Space();

		GUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		if( CHSlogo != null ) {
			GUILayout.Label( new GUIContent(CHSlogo), GUILayout.Height(100), GUILayout.Width(200) );
		} else {
			GUILayout.Label( "Customizable Hologram Shader", CHSstyle );
		}
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();

		EditorGUILayout.Space();

		EditorGUILayout.PropertyField(patternVerticalAnimation);
		serializedObject.ApplyModifiedProperties();

		if ( patternVerticalAnimation.enumValueIndex != 0 ) {
			EditorGUILayout.PropertyField(patternVerticalSpeed);
		}
		EditorGUILayout.Space();

		EditorGUILayout.PropertyField(patternHorizontalAnimation);
		serializedObject.ApplyModifiedProperties();

		if ( patternHorizontalAnimation.enumValueIndex != 0 ) {
			EditorGUILayout.PropertyField(patternHorizontalSpeed);
		}
		EditorGUILayout.Space();

		EditorGUILayout.PropertyField(noise);
		serializedObject.ApplyModifiedProperties();

		if ( noise.boolValue ) {
			EditorGUILayout.PropertyField(noiseSpeed);
		}
		EditorGUILayout.Space();

		EditorGUILayout.PropertyField(animateColorIntensity);
		serializedObject.ApplyModifiedProperties();

		if ( animateColorIntensity.boolValue ) {
			EditorGUILayout.PropertyField(intensitySpeed);
			EditorGUILayout.PropertyField(intensityRange);
		}

	}
}