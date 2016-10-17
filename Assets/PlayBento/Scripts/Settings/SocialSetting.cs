using UnityEngine;
using System.IO;

using PlayBento;
using System.Xml;
using System.Xml.Serialization;

#if UNITY_EDITOR
using UnityEditor;

[InitializeOnLoad]
#endif

public class SocialSetting : ScriptableObject {

	const string playBentoSocialSettingsAssetName = "Social";
	const string playBentoSocialSettingsPath = "Resources/Bento";
	const string playBentoSocialSettingsAssetExtension = ".asset";
	
	private static SocialSetting instance;
	
	static SocialSetting Instance
	{
		get
		{
			if (instance == null)
			{
				instance = Resources.Load("Bento/" + playBentoSocialSettingsAssetName) as SocialSetting;
				if (instance == null)
				{
					// If not found, autocreate the asset object.
					instance = CreateInstance<SocialSetting>();
					#if UNITY_EDITOR
					string properPath = Path.Combine(Application.dataPath, playBentoSocialSettingsPath);
					if (!Directory.Exists(properPath))
					{
						AssetDatabase.CreateFolder("Assets/Resources", "Bento");
					}
					
					string fullPath = Path.Combine(Path.Combine("Assets", playBentoSocialSettingsPath),
					                               playBentoSocialSettingsAssetName + playBentoSocialSettingsAssetExtension
					                               );
					AssetDatabase.CreateAsset(instance, fullPath);
					#endif
				}
			}
			return instance;
		}
	}

	#if UNITY_EDITOR
	[MenuItem("PlayBento/Social Settings",false, 1)]
	public static void Edit()
	{
		Selection.activeObject = Instance;
	}

	[MenuItem("PlayBento/About us")]
	public static void OpenPlayBentoPage()
	{
		string url = "http://playbento.com/";
		Application.OpenURL(url);
	}
	

	#endif

	#region Facebook Setting

	[SerializeField]
	private string _inviteMessage = "Come play this great game!";
	[SerializeField]
	public static string inviteMessage {
		get {
			return Instance._inviteMessage;
		}
		set {
			if (Instance._inviteMessage != value)
			   {
				Instance._inviteMessage = value;
				DirtyEditor();
			}
		}
	}

	[SerializeField]
	private int _sendGiftDelay = 3600;
	[SerializeField]
	public static int sendGiftDelay {
		get {
			return Instance._sendGiftDelay;
		}
		set {
			if (Instance._sendGiftDelay != value)
			{
				Instance._sendGiftDelay = value;
				DirtyEditor();
			}
		}
	}

	#endregion
	
	#region Share Settings
	
	[SerializeField]
	private bool _publishAction = true;
	[SerializeField]
	private string[] _captionLabels = new[] { "Caption", "Caption"};
	[SerializeField]
	private string[] _descriptionLabels = new[] { "Description", "Description" };
	[SerializeField]
	private string[] _idLabels;
	[SerializeField]
	private string _pictureLabel = "";
	[SerializeField]
	private string _linkLabel = "";
	[SerializeField]
	private string _nameLabel = "";
	[SerializeField]
	private bool _isUseSocial = true;


	public static bool IsUseSocial {
		get {
			return Instance._isUseSocial;
		}
		set
		{
			if (Instance._isUseSocial != value)
			{
				Instance._isUseSocial = value;
				DirtyEditor();
			}
			
		}
	}

	[SerializeField]
	public static bool publish_actions
	{
		get{ return Instance._publishAction; }
		set
		{
			if (Instance._publishAction != value)
			{
				Instance._publishAction = value;
				DirtyEditor();
			}
			
		}
	}


	public string pictureLabel
	{
		get { return _pictureLabel; }
		set
		{
			if (_pictureLabel != value)
			{
				_pictureLabel = value;
				DirtyEditor();
			}
		}
	}

	public string linkLabel
	{
		get { return _linkLabel; }
		set
		{
			if (_linkLabel != value)
			{
				_linkLabel = value;
				DirtyEditor();
			}
		}
	}

	public string nameLabel
	{
		get { return _nameLabel; }
		set
		{
			if (_nameLabel != value)
			{
				_nameLabel = value;
				DirtyEditor();
			}
		}
	}

	public string[] descriptionLabels
	{
		get { return _descriptionLabels; }
		set
		{
			if (_descriptionLabels != value)
			{
				_descriptionLabels = value;
				DirtyEditor();
			}
		}
	}

	public void SetDescriptionLabel(int index, string value)
	{
		if (descriptionLabels[index] != value)
		{
			descriptionLabels[index] = value;
			DirtyEditor();
		}
	}

	public string[] captionLabels
	{
		get { return _captionLabels; }
		set
		{
			if (_captionLabels != value)
			{
				_captionLabels = value;
				DirtyEditor();
			}
		}
	}

	public void SetCaptionLabel(int index, string value)
	{
		if (_captionLabels[index] != value)
		{
			captionLabels[index] = value;
			DirtyEditor();
		}
	}

	public string[] idLabels
	{
		get { 
			return _idLabels; 
		}
		set
		{
			if (_idLabels != value)
			{
				_idLabels = value;
				DirtyEditor();
			}
		}
	}

	public void SetIdLabel(int index, string value)
	{
		if (_idLabels[index] != value)
		{
			idLabels[index] = value;
			DirtyEditor();
		}
	}

	public static string[] AllAppIds
	{
		get { return Instance.descriptionLabels; }
	}

	public void CreateShareConfig()
	{
		string filePath = Path.Combine(Application.dataPath,"Resources/Katsu/SocialConfig.xml");

		SocialConfig shareConfig = new SocialConfig();
		shareConfig.PublishPermission = _publishAction;
		shareConfig.GiftDelay = _sendGiftDelay;
		shareConfig.InviteMessage = _inviteMessage;
		SocialObject defaultObject = new SocialObject();
		defaultObject.Id = "default";
		defaultObject.Picture = _pictureLabel;
		defaultObject.Link = _linkLabel;
		defaultObject.Name = _nameLabel;
		shareConfig.Objects.Add(defaultObject);
		
		for(int i = 0; i < _captionLabels.Length; i++)
		{
			SocialObject shareObject = new SocialObject();
			if(i == 0) { shareObject.Id = "recommend"; }
			else if(i == 1) { shareObject.Id = "score"; }
			else { shareObject.Id = _idLabels[i-2]; }

			shareObject.Caption = _captionLabels[i];
			shareObject.Description = _descriptionLabels[i];
			shareConfig.Objects.Add(shareObject);

		}

		XmlSerializer serializer = new XmlSerializer(typeof(SocialConfig));
		StringWriter writer = new StringWriter();
		serializer.Serialize(writer, shareConfig);
		
		StreamWriter steramWriter = new StreamWriter(filePath);
		steramWriter.Write(writer.ToString().Replace("encoding=\"utf-16\"", "encoding=\"utf-8\""));
		steramWriter.Close();

		Debug.Log("SocialConfig was created");
	}

	private static void DirtyEditor()
	{
		#if UNITY_EDITOR
		EditorUtility.SetDirty(Instance);

		#endif
	}
	#endregion
}
