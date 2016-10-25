using UnityEngine;
using System.IO;

using PlayBento;
using System.Xml;
using System.Xml.Serialization;

#if UNITY_EDITOR
using UnityEditor;

[InitializeOnLoad]
#endif

public class CloudSetting : ScriptableObject {

	const string playBentoRemoteSettingsAssetName = "Cloud";
	const string playBentoRemoteSettingsPath = "Resources/Bento";
	const string playBentoRemoteSettingsAssetExtension = ".asset";
	
	private static CloudSetting instance;
	
	static CloudSetting Instance
	{
		get
		{
			if (instance == null)
			{
				instance = Resources.Load("Bento/" + playBentoRemoteSettingsAssetName) as CloudSetting;
				if (instance == null)
				{
					// If not found, autocreate the asset object.
					instance = CreateInstance<CloudSetting>();
					#if UNITY_EDITOR
					string properPath = Path.Combine(Application.dataPath, playBentoRemoteSettingsPath);
					if (!Directory.Exists(properPath))
					{
						AssetDatabase.CreateFolder("Assets/Resources", "Bento");
					}
					
					string fullPath = Path.Combine(Path.Combine("Assets", playBentoRemoteSettingsPath),
					                               playBentoRemoteSettingsAssetName + playBentoRemoteSettingsAssetExtension
					                               );
					AssetDatabase.CreateAsset(instance, fullPath);
					#endif
				}
			}
			return instance;
		}
	}

	#if UNITY_EDITOR
	[MenuItem("PlayBento/Cloud Settings",false, 2)]
	public static void Edit()
	{
		Selection.activeObject = Instance;
	}


#endif

    #region Facebook Setting

    #endregion

    #region Share Settings

    [SerializeField]
    private string _serverLabel = "";
    [SerializeField]
	private string _applicationIDLabel = "";
	[SerializeField]
	private string _dotnetKeyLabel = "";

    public string ServerLabel
    {
        get { return _serverLabel; }
        set
        {
            if (_serverLabel != value)
            {
                _serverLabel = value;
                DirtyEditor();
            }
        }
    }

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

	public string DotnetKeyLabel
	{
		get { return _dotnetKeyLabel; }
		set
		{
			if (_dotnetKeyLabel != value)
			{
				_dotnetKeyLabel = value;
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
		get { return Instance.DotnetKeyLabel; }
	}

	public void CreateRemoteConfig()
	{
		string filePath = Path.Combine(Application.dataPath,"Resources/Katsu/CloudConfig.xml");

		CloudConfig remoteConfig = new CloudConfig();

        remoteConfig.Server = _serverLabel;
		remoteConfig.ApplicationID = _applicationIDLabel;
		remoteConfig.DotNetKey = _dotnetKeyLabel;

		XmlSerializer serializer = new XmlSerializer(typeof(CloudConfig));
		StringWriter writer = new StringWriter();
		serializer.Serialize(writer, remoteConfig);
		
		StreamWriter steramWriter = new StreamWriter(filePath);
		steramWriter.Write(writer.ToString().Replace("encoding=\"utf-16\"", "encoding=\"utf-8\""));
		steramWriter.Close();

		Debug.Log("CloudConfig was created");
	}

	private static void DirtyEditor()
	{
		#if UNITY_EDITOR
		EditorUtility.SetDirty(Instance);

		#endif
	}
	#endregion
}
