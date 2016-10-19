using Facebook;
using UnityEngine;
using UnityEditor;
using UnityEditor.FacebookEditor;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

[CustomEditor(typeof(SocialSetting))]
public class SocialSettingEditor : Editor {

	bool showPlayBentoShareSettings = true;

	GUIStyle headTopicStyle = null;

	GUIContent isuseSocial = new GUIContent("Use Social [?]", "Local Or Social App");
	GUIContent InviteMsgLabel = new GUIContent("Invite");
	GUIContent facebookPermissionLabel = new GUIContent("Facebook Extended Settings");
	GUIContent publishActionsLabel = new GUIContent("Publish Actions [?]", "Should the app ask for publish_actions permission?");
	GUIContent inviteMsgLabel = new GUIContent("Message [?]", "Message to show at invite dialog");
	GUIContent sendGiftDelayLabel = new GUIContent("Delay [?]", "How long in seconds to wait before the player can send gift to the same recipient again");

	GUIContent shareDefaultLabel = new GUIContent("Default [?]", "These settings applied to all posts shared");
	GUIContent shareRecommendLabel = new GUIContent("Recommend [?]", "Settings for Social.PostRecommend");
	GUIContent shareScoreLabel = new GUIContent("Score [?]", "Settings for Social.PostScore");
	GUIContent shareAnotherLabel = new GUIContent("Achievement [?]", "Settings for Social.PostAchievement");

	GUIContent shareIdLabel = new GUIContent("ID");
	GUIContent sharePictureLabel = new GUIContent("Picture Url [?]", "URL to the picture to share");
	GUIContent shareLinkLabel = new GUIContent("Link Url [?]", "URL of the sharing post");
	GUIContent shareNameLabel = new GUIContent("Name [?]", "Name of the sharing post");
	GUIContent shareCaptionLabel = new GUIContent("Caption");
	GUIContent shareDescriptionLabel = new GUIContent("Description");

	private SocialSetting instance;
	
	public override void OnInspectorGUI()
	{
		instance = (SocialSetting)target;

		if(headTopicStyle == null)
		{
			headTopicStyle = new GUIStyle(GUI.skin.label);
		}
		headTopicStyle.fontStyle = FontStyle.Bold;

		FacebookGUI();
		SocialConfigGUI();
	}

	private void FacebookGUI() 
	{
//		EditorGUILayout.LabelField(facebookLabel, headTopicStyle);
//		if (GUILayout.Button("Go To Facebook Setting"))
//		{
//			FBSettings.Edit();
//		}

//		EditorGUILayout.LabelField(facebookPermissionLabel, headTopicStyle);
		SocialSetting.IsUseSocial = EditorGUILayout.Toggle(isuseSocial, SocialSetting.IsUseSocial);
		EditorGUILayout.Space();

		if(SocialSetting.IsUseSocial)
		{
			EditorGUILayout.LabelField(facebookPermissionLabel, headTopicStyle);
			SocialSetting.publish_actions = EditorGUILayout.Toggle(publishActionsLabel, SocialSetting.publish_actions);
			EditorGUILayout.Space();

			EditorGUILayout.LabelField(InviteMsgLabel, headTopicStyle);
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(inviteMsgLabel);
			SocialSetting.inviteMessage = EditorGUILayout.TextField(SocialSetting.inviteMessage);
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.Space();
			EditorGUILayout.LabelField("Gift", headTopicStyle);
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(sendGiftDelayLabel);
			SocialSetting.sendGiftDelay = EditorGUILayout.IntField(SocialSetting.sendGiftDelay);
			EditorGUILayout.EndHorizontal();
			EditorGUILayout.Space();
		}
	}

	private void SocialConfigGUI()
	{
		if(SocialSetting.IsUseSocial)
		{
			EditorGUILayout.LabelField("Social Config", headTopicStyle);
			showPlayBentoShareSettings = EditorGUILayout.Foldout(showPlayBentoShareSettings, "Social Setting");
			if (showPlayBentoShareSettings)
			{
	//			EditorGUILayout.HelpBox("1) Add the Facebook App Id(s) associated with this game", MessageType.None);
				//default share
				EditorGUILayout.LabelField(shareDefaultLabel);
				//picture url
				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.LabelField(sharePictureLabel);
				instance.pictureLabel = EditorGUILayout.TextField(instance.pictureLabel);
				EditorGUILayout.EndHorizontal();
				//link url
				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.LabelField(shareLinkLabel);
				instance.linkLabel = EditorGUILayout.TextField(instance.linkLabel);
				EditorGUILayout.EndHorizontal();
				//name
				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.LabelField(shareNameLabel);
				instance.nameLabel = EditorGUILayout.TextField(instance.nameLabel);
				EditorGUILayout.EndHorizontal();

				EditorGUILayout.Space();

				//recommend share
				EditorGUILayout.LabelField(shareRecommendLabel);
				//caption url
				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.LabelField(shareCaptionLabel);
				instance.SetCaptionLabel(0, EditorGUILayout.TextField(instance.captionLabels[0]));
				EditorGUILayout.EndHorizontal();
				//description url
				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.LabelField(shareDescriptionLabel);
				instance.SetDescriptionLabel(0, EditorGUILayout.TextField(instance.descriptionLabels[0]));
				EditorGUILayout.EndHorizontal();

				EditorGUILayout.Space();

				//score share
				EditorGUILayout.LabelField(shareScoreLabel);
				//caption url
				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.LabelField(shareCaptionLabel);
				instance.SetCaptionLabel(1, EditorGUILayout.TextField(instance.captionLabels[1]));
				EditorGUILayout.EndHorizontal();
				//description url
				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.LabelField(shareDescriptionLabel);
				instance.SetDescriptionLabel(1, EditorGUILayout.TextField(instance.descriptionLabels[1]));
				EditorGUILayout.EndHorizontal();

				EditorGUILayout.Space();

				if(instance.idLabels != null && instance.idLabels.Length > 0)
				{
					for(int i = 2; i < instance.captionLabels.Length; i++)
					{
						EditorGUILayout.LabelField(shareAnotherLabel);
						if(instance.idLabels.Length < i-3)
						{
							List<string> changeList = new List<string>(instance.captionLabels);
							changeList.RemoveAt(instance.captionLabels.Length-1);
							instance.captionLabels = changeList.ToArray();

							changeList = new List<string>(instance.descriptionLabels);
							changeList.RemoveAt(instance.descriptionLabels.Length-1);
							instance.descriptionLabels = changeList.ToArray();

							continue;
						}
						//id
						EditorGUILayout.BeginHorizontal();
						EditorGUILayout.LabelField(shareIdLabel);
						instance.SetIdLabel(i-2, EditorGUILayout.TextField(instance.idLabels[i-2]));
						EditorGUILayout.EndHorizontal();
						//caption url
						EditorGUILayout.BeginHorizontal();
						EditorGUILayout.LabelField(shareCaptionLabel);
						instance.SetCaptionLabel(i, EditorGUILayout.TextField(instance.captionLabels[i]));
						EditorGUILayout.EndHorizontal();
						//description url
						EditorGUILayout.BeginHorizontal();
						EditorGUILayout.LabelField(shareDescriptionLabel);
						instance.SetDescriptionLabel(i, EditorGUILayout.TextField(instance.descriptionLabels[i]));
						EditorGUILayout.EndHorizontal();

						EditorGUILayout.Space();
					}
				}

				if (GUILayout.Button("Add Achievement"))
				{
					var captionLabel = new List<string>(instance.captionLabels);
					captionLabel.Add("Caption");
					instance.captionLabels = captionLabel.ToArray();
					
					var descriptionLabel = new List<string>(instance.descriptionLabels);
					descriptionLabel.Add("Description");
					instance.descriptionLabels = descriptionLabel.ToArray();
					List<string> idLabel;
					if(instance.idLabels == null)
					{
						idLabel = new List<string>();
					}
					else
					{
						idLabel = new List<string>(instance.idLabels);
					}

					idLabel.Add("ID");
					instance.idLabels = idLabel.ToArray();
				}
				if (instance.idLabels != null && instance.idLabels.Length > 0)
				{
					if (GUILayout.Button("Remove Last Achievement"))
					{
						var captionLabels = new List<string>(instance.captionLabels);
						captionLabels.RemoveAt(captionLabels.Count - 1);
						instance.captionLabels = captionLabels.ToArray();
						
						var descriptionLabels = new List<string>(instance.descriptionLabels);
						descriptionLabels.RemoveAt(descriptionLabels.Count - 1);
						instance.descriptionLabels = descriptionLabels.ToArray();

						var idLabels = new List<string>(instance.idLabels);
						idLabels.RemoveAt(idLabels.Count - 1);
						instance.idLabels = idLabels.ToArray();
					}
				}
				EditorGUILayout.Space();

				if (GUILayout.Button("Save SocialConfig"))
				{
					instance.CreateShareConfig();
				}
			}
			EditorGUILayout.Space();
		}
	}
}
