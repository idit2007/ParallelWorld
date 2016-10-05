using UnityEngine;
using System.IO;

using PlayBento;
using System.Xml;
using System.Xml.Serialization;

#if UNITY_EDITOR
using UnityEditor;

[InitializeOnLoad]
#endif

public class IAPSetting : ScriptableObject {

	const string assetName = "IAP";
	const string settingsPath = "Resources/Bento";
	const string settingsAssetExtension = ".asset";
	
	private static IAPSetting instance;
	
	static IAPSetting Instance
	{
		get
		{
			if (instance == null)
			{
				instance = Resources.Load("Bento/" + assetName) as IAPSetting;
				if (instance == null)
				{
					// If not found, autocreate the asset object.
					instance = CreateInstance<IAPSetting>();
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
	[MenuItem("PlayBento/IAP Settings",false, 5)]
	public static void Edit()
	{
		Selection.activeObject = Instance;
	}
	

	#endif

	#region Facebook Setting

	#endregion
	
	#region Share Settings

	[SerializeField]
	private string _googleLicenceKey = "";
	[SerializeField]
	private string[] _productIDLabels = new[] { ""};
	[SerializeField]
	private string[] _titleLabels =  new[] { ""};
	[SerializeField]
	private string[] _descriptionLabels = new[] { ""};
	[SerializeField]
	private float[] _priceLabels =  new[] { 0.0f};
	[SerializeField]
	private bool[] _consumableLabels =  new[] { false};

	public string GoogleLicenceKey
	{
		get { return _googleLicenceKey; }
		set
		{
			if (_googleLicenceKey != value)
			{
				_googleLicenceKey = value;
				DirtyEditor();
			}
		}
	}
	public string[] ProductIDLabels
	{
		get { return _productIDLabels; }
		set
		{
			if (_productIDLabels != value)
			{
				_productIDLabels = value;
				DirtyEditor();
			}
		}
	}
	public string[] TitleLabels
	{
		get { return _titleLabels; }
		set
		{
			if (_titleLabels != value)
			{
				_titleLabels = value;
				DirtyEditor();
			}
		}
	}
	public string[] DescriptionLabels
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
	public float[] PriceLabels
	{
		get { return _priceLabels; }
		set
		{
			if (_priceLabels != value)
			{
				_priceLabels = value;
				DirtyEditor();
			}
		}
	}
	public bool[] ConsumableLabels
	{
		get { return _consumableLabels; }
		set
		{
			if (_consumableLabels != value)
			{
				_consumableLabels = value;
				DirtyEditor();
			}
		}
	}

	public void SetProductIDLabel(int index, string value)
	{
		if (_productIDLabels[index] != value)
		{
			ProductIDLabels[index] = value;
			DirtyEditor();
		}
	}
	public void SetTitleLabel(int index, string value)
	{
		if (_titleLabels[index] != value)
		{
			TitleLabels[index] = value;
			DirtyEditor();
		}
	}
	public void SetDescriptionLabel(int index, string value)
	{
		if (_descriptionLabels[index] != value)
		{
			DescriptionLabels[index] = value;
			DirtyEditor();
		}
	}
	public void SetPriceLabel(int index, float value)
	{
		if (_priceLabels[index] != value)
		{
			PriceLabels[index] = value;
			DirtyEditor();
		}
	}
	public void SetConsumableLabel(int index, bool value)
	{
		if (_consumableLabels[index] != value)
		{
			ConsumableLabels[index] = value;
			DirtyEditor();
		}
	}

	public void CreateIAPConfig()
	{
		string filePath = Path.Combine(Application.dataPath,"Resources/Katsu/IAPConfig.xml");

		IAPConfig iapConfig = new IAPConfig();

		iapConfig.GoogleLicenseKey = GoogleLicenceKey;

		for(int i = 0; i < ProductIDLabels.Length; i++)
		{
			IAPItem iapItem = new IAPItem();

			iapItem.Id = ProductIDLabels[i];
			iapItem.Title = TitleLabels[i];
			iapItem.Description = DescriptionLabels[i];
			iapItem.Price = PriceLabels[i];
			iapItem.Consumable = ConsumableLabels[i];

			iapConfig.Items.Add(iapItem);

		}

		XmlSerializer serializer = new XmlSerializer(typeof(IAPConfig));
		StringWriter writer = new StringWriter();
		serializer.Serialize(writer, iapConfig);
		
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
