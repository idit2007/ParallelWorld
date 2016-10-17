using UnityEngine;
using System.IO;

using PlayBento;
using System.Xml;
using System.Xml.Serialization;

#if UNITY_EDITOR
using UnityEditor;

[InitializeOnLoad]
#endif

public class AdSetting : ScriptableObject {

	const string assetName = "Ad";
	const string settingsPath = "Resources/Bento";
	const string settingsAssetExtension = ".asset";
	
	private static AdSetting instance;
	
	static AdSetting Instance
	{
		get
		{
			if (instance == null)
			{
				instance = Resources.Load("Bento/" + assetName) as AdSetting;
				if (instance == null)
				{
					// If not found, autocreate the asset object.
					instance = CreateInstance<AdSetting>();
					#if UNITY_EDITOR
					string properPath = Path.Combine(Application.dataPath, settingsPath);
					if (!Directory.Exists(properPath))
					{
						AssetDatabase.CreateFolder("Assets/Resources", "Bento");
					}
					
					string fullPath = Path.Combine(Path.Combine("Assets", settingsPath),
					                               assetName + settingsAssetExtension
					                               );
					AssetDatabase.CreateAsset(instance, fullPath);
					#endif
				}
			}
			return instance;
		}
	}

	#if UNITY_EDITOR
	[MenuItem("PlayBento/Ad Settings",false, 4)]
	public static void Edit()
	{
		Selection.activeObject = Instance;
	}
	

	#endif
	
	#region Settings

	[SerializeField]
	private string _admobAndroidPublisherID = "";
	[SerializeField]
	private string _admobIosPublisherID = "";
	[SerializeField]
	private int _admobPriority = 1;

	public string AdmobAndroidPublisherID
	{
		get { return _admobAndroidPublisherID; }
		set
		{
			if (_admobAndroidPublisherID != value)
			{
				_admobAndroidPublisherID = value;
				DirtyEditor();
			}
		}
	}

	public string AdmobIosPublisherID
	{
		get { return _admobIosPublisherID; }
		set
		{
			if (_admobIosPublisherID != value)
			{
				_admobIosPublisherID = value;
				DirtyEditor();
			}
		}
	}

	public int AdmobPriority
	{
		get { return _admobPriority; }
		set
		{
			if (_admobPriority != value)
			{
				_admobPriority = value;
				DirtyEditor();
			}
		}
	}

	[SerializeField]
	private int _chartboostPriority = 2;

	public int ChartboostPriority
	{
		get { return _chartboostPriority; }
		set
		{
			if (_chartboostPriority != value)
			{
				_chartboostPriority = value;
				DirtyEditor();
			}
		}
	}

	[SerializeField]
	private string _unityAndroidAppID = "";
	[SerializeField]
	private string _unityIosAppID = "";
	[SerializeField]
	private int _unityPriority = 3;
	
	public string UnityAndroidAppID
	{
		get { return _unityAndroidAppID; }
		set
		{
			if (_unityAndroidAppID != value)
			{
				_unityAndroidAppID = value;
				DirtyEditor();
			}
		}
	}
	public string UnityIosAppID
	{
		get { return _unityIosAppID; }
		set
		{
			if (_unityIosAppID != value)
			{
				_unityIosAppID = value;
				DirtyEditor();
			}
		}
	}
	public int UnityPriority
	{
		get { return _unityPriority; }
		set
		{
			if (_unityPriority != value)
			{
				_unityPriority = value;
				DirtyEditor();
			}
		}
	}

	[SerializeField]
	private string _adColonyAndroidAppID = "";
	[SerializeField]
	private string _adColonyAndroidZoneID = "";
	[SerializeField]
	private string _adColonyIosAppID = "";
	[SerializeField]
	private string _adColonyIosZoneID = "";
	[SerializeField]
	private int _adColonyPriority = 4;
	
	public string AdColonyAndroidAppID
	{
		get { return _adColonyAndroidAppID; }
		set
		{
			if (_adColonyAndroidAppID != value)
			{
				_adColonyAndroidAppID = value;
				DirtyEditor();
			}
		}
	}
	public string AdColonyAndroidZoneID
	{
		get { return _adColonyAndroidZoneID; }
		set
		{
			if (_adColonyAndroidZoneID != value)
			{
				_adColonyAndroidZoneID = value;
				DirtyEditor();
			}
		}
	}
	public string AdColonyIosAppID
	{
		get { return _adColonyIosAppID; }
		set
		{
			if (_adColonyIosAppID != value)
			{
				_adColonyIosAppID = value;
				DirtyEditor();
			}
		}
	}
	public string AdColonyIosZoneID
	{
		get { return _adColonyIosZoneID; }
		set
		{
			if (_adColonyIosZoneID != value)
			{
				_adColonyIosZoneID = value;
				DirtyEditor();
			}
		}
	}
	public int AdColonyPriority
	{
		get { return _adColonyPriority; }
		set
		{
			if (_adColonyPriority != value)
			{
				_adColonyPriority = value;
				DirtyEditor();
			}
		}
	}

	public void CreateAdConfig()
	{
		string filePath = Path.Combine(Application.dataPath,"Resources/Katsu/AdConfig.xml");

		AdConfig adConfig = new AdConfig();

		adConfig.Android.AdMob.AdUnit = _admobAndroidPublisherID;
		adConfig.Android.AdMob.priority = _admobPriority;
		adConfig.iOS.AdMob.AdUnit = _admobIosPublisherID;
		adConfig.iOS.AdMob.priority = _admobPriority;

		adConfig.Android.Chartboost.priority = _chartboostPriority;
		adConfig.iOS.Chartboost.priority = _chartboostPriority;

		adConfig.Android.Unity.AppId = _unityAndroidAppID;
		adConfig.Android.Unity.priority = _unityPriority;
		adConfig.iOS.Unity.AppId = _unityIosAppID;
		adConfig.iOS.Unity.priority = _unityPriority;

		adConfig.Android.AdColony.AppId = _adColonyAndroidAppID;
		adConfig.Android.AdColony.ZoneId = _adColonyAndroidZoneID;
		adConfig.Android.AdColony.priority = _adColonyPriority;
		adConfig.iOS.AdColony.AppId = _adColonyIosAppID;
		adConfig.iOS.AdColony.ZoneId = _adColonyIosZoneID;
		adConfig.iOS.AdColony.priority = _adColonyPriority;

		XmlSerializer serializer = new XmlSerializer(typeof(AdConfig));
		StringWriter writer = new StringWriter();
		serializer.Serialize(writer, adConfig);
		
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
