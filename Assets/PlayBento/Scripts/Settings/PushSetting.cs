using UnityEngine;
using System.IO;

using PlayBento;
using System.Xml;
using System.Xml.Serialization;

#if UNITY_EDITOR
using UnityEditor;

[InitializeOnLoad]
#endif

public class PushSetting : ScriptableObject {

	const string playBentoPushSettingsAssetName = "Push";
	const string playBentoPushSettingsPath = "Resources/Bento";
	const string playBentoPushSettingsAssetExtension = ".asset";
	
	private static PushSetting instance;
	
	static PushSetting Instance
	{
		get
		{
			if (instance == null)
			{
				instance = Resources.Load("Bento/" + playBentoPushSettingsAssetName) as PushSetting;
				if (instance == null)
				{
					// If not found, autocreate the asset object.
					instance = CreateInstance<PushSetting>();
					#if UNITY_EDITOR
					string properPath = Path.Combine(Application.dataPath, playBentoPushSettingsPath);
					if (!Directory.Exists(properPath))
					{
						AssetDatabase.CreateFolder("Assets/Resources", "Bento");
					}
					
					string fullPath = Path.Combine(Path.Combine("Assets", playBentoPushSettingsPath),
					                               playBentoPushSettingsAssetName + playBentoPushSettingsAssetExtension
					                               );
					AssetDatabase.CreateAsset(instance, fullPath);
					#endif
				}
			}
			return instance;
		}
	}

	#if UNITY_EDITOR
	[MenuItem("PlayBento/Push Settings",false, 3)]
	public static void Edit()
	{
		Selection.activeObject = Instance;
	}
	

	#endif

	#region Facebook Setting

	#endregion
	
	#region Share Settings

	[SerializeField]
	private string _applicationIDLabel = "";
	[SerializeField]
	private string _gcmProjectIDLabel = "";

	public string ApplicationIDLabel
	{
		get { return _applicationIDLabel; }
		set
		{
			if (_applicationIDLabel != value)
			{
				_applicationIDLabel = value;
				DirtyEditor();
			}
		}
	}

	public string GCMProjectID
	{
		get { return _gcmProjectIDLabel; }
		set
		{
			if (_gcmProjectIDLabel != value)
			{
				_gcmProjectIDLabel = value;
				DirtyEditor();
			}
		}
	}

	public static string ParseApplicationID
	{
		get { return Instance.ApplicationIDLabel; }
	}

	public static string ParseDotnetKey
	{
		get { return Instance.GCMProjectID; }
	}

	public void CreatePushConfig()
	{
		string filePath = Path.Combine(Application.dataPath,"Resources/Katsu/PushConfig.xml");

		PushConfig pushConfig = new PushConfig();

		pushConfig.AppId = _applicationIDLabel;
		pushConfig.GCMProjectId = _gcmProjectIDLabel;

		XmlSerializer serializer = new XmlSerializer(typeof(PushConfig));
		StringWriter writer = new StringWriter();
		serializer.Serialize(writer, pushConfig);
		
		StreamWriter steramWriter = new StreamWriter(filePath);
		steramWriter.Write(writer.ToString().Replace("encoding=\"utf-16\"", "encoding=\"utf-8\""));
		steramWriter.Close();

		Debug.Log("PushConfig was created");
	}

	private static void DirtyEditor()
	{
		#if UNITY_EDITOR
		EditorUtility.SetDirty(Instance);

		#endif
	}
	#endregion
}
