using Facebook;
using UnityEngine;
using UnityEditor;
using UnityEditor.FacebookEditor;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

[CustomEditor(typeof(IAPSetting))]
public class IAPSettingEditor : Editor {

	GUIStyle headTopicStyle = null;
	
	GUIContent topicLabel = new GUIContent("In App Purchase");
	GUIContent googleLicenceKeyLabel = new GUIContent("Google Licence Key");
	GUIContent productIDLabel = new GUIContent("Product ID");
	GUIContent titleLabel = new GUIContent("Title");
	GUIContent descriptionLabel = new GUIContent("Description");
	GUIContent priceLabel = new GUIContent("Price");
	GUIContent consumableLabel = new GUIContent("Consumable");

	private IAPSetting instance;
	
	public override void OnInspectorGUI()
	{
		instance = (IAPSetting)target;

		if(headTopicStyle == null)
		{
			headTopicStyle = new GUIStyle(GUI.skin.label);
		}
		headTopicStyle.fontStyle = FontStyle.Bold;

		IAPGUI();
	}

	private void IAPGUI()
	{
		EditorGUILayout.LabelField(topicLabel, headTopicStyle);

		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField(googleLicenceKeyLabel);
		instance.GoogleLicenceKey = EditorGUILayout.TextField(instance.GoogleLicenceKey);
		EditorGUILayout.EndHorizontal();

//		EditorGUILayout.BeginHorizontal();
//		EditorGUILayout.LabelField(gcmProjectIDLabel);
//		instance.ProductIDLabels = EditorGUILayout.TextField(instance.ProductIDLabels);
//		EditorGUILayout.EndHorizontal();

		if(instance.ProductIDLabels.Length > 0)
		{
			for(int i = 0; i < instance.ProductIDLabels.Length; i++)
			{
				//product id
				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.LabelField(productIDLabel);
				instance.SetProductIDLabel(i, EditorGUILayout.TextField(instance.ProductIDLabels[i]));
				EditorGUILayout.EndHorizontal();
				//title
				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.LabelField(titleLabel);
				instance.SetTitleLabel(i, EditorGUILayout.TextField(instance.TitleLabels[i]));
				EditorGUILayout.EndHorizontal();
				//description
				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.LabelField(descriptionLabel);
				instance.SetDescriptionLabel(i, EditorGUILayout.TextField(instance.DescriptionLabels[i]));
				EditorGUILayout.EndHorizontal();
				//price
				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.LabelField(priceLabel);
				instance.SetPriceLabel(i, EditorGUILayout.FloatField(instance.PriceLabels[i]));
				EditorGUILayout.EndHorizontal();
				//consumable
				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.LabelField(consumableLabel);
				instance.SetConsumableLabel(i, EditorGUILayout.Toggle(instance.ConsumableLabels[i]));
				EditorGUILayout.EndHorizontal();
				
				EditorGUILayout.Space();
			}
		}

		if (GUILayout.Button("Add IAP"))
		{
			var productIDLabel = new List<string>(instance.ProductIDLabels);
			productIDLabel.Add("");
			instance.ProductIDLabels = productIDLabel.ToArray();

			var titleLabel = new List<string>(instance.TitleLabels);
			titleLabel.Add("");
			instance.TitleLabels = titleLabel.ToArray();
			
			var descriptionLabel = new List<string>(instance.DescriptionLabels);
			descriptionLabel.Add("");
			instance.DescriptionLabels = descriptionLabel.ToArray();

			var priceLabel = new List<float>(instance.PriceLabels);
			priceLabel.Add(0.0f);
			instance.PriceLabels = priceLabel.ToArray();

			var consumableLabel = new List<bool>(instance.ConsumableLabels);
			consumableLabel.Add(false);
			instance.ConsumableLabels = consumableLabel.ToArray();

		}
		if (instance.ProductIDLabels.Length > 1)
		{
			if (GUILayout.Button("Remove Last IAP"))
			{
				var productIDLabel = new List<string>(instance.ProductIDLabels);
				productIDLabel.RemoveAt(productIDLabel.Count-1);
				instance.ProductIDLabels = productIDLabel.ToArray();
				
				var titleLabel = new List<string>(instance.TitleLabels);
				titleLabel.RemoveAt(titleLabel.Count -1);
				instance.TitleLabels = titleLabel.ToArray();
				
				var descriptionLabel = new List<string>(instance.DescriptionLabels);
				descriptionLabel.RemoveAt(descriptionLabel.Count -1);
				instance.DescriptionLabels = descriptionLabel.ToArray();
				
				var priceLabel = new List<float>(instance.PriceLabels);
				priceLabel.RemoveAt(priceLabel.Count -1);
				instance.PriceLabels = priceLabel.ToArray();
				
				var consumableLabel = new List<bool>(instance.ConsumableLabels);
				consumableLabel.RemoveAt(consumableLabel.Count -1);
				instance.ConsumableLabels = consumableLabel.ToArray();
			}
		}

		EditorGUILayout.Space();

		if (GUILayout.Button("Save IAPConfig"))
		{
			instance.CreateIAPConfig();
		}
	}
}
