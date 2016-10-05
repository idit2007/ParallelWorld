//#define FREE_BENTO

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

using UnityEngine;

// TODO: Create snapshot of all objects loaded, then before saving, if an object is modified, save
namespace PlayBento
{
	/// <summary>
	/// A generic class that helps managing all classes derived from the class parameter T
	/// </summary>
	/// <typeparam name="T">The element type of the array</typeparam>
	public class Katsu <T> where T : KatsuObject
	{
		private List<T> objects;
		public List<T> Objects {
			get {
				return new List<T>(objects);
			}
		}

		private string dataPath;
		private string resourcePath;

		/// <summary>
		/// Initializes a new instance of the class.
		/// </summary>
		public Katsu()
		{
			objects = new List<T> ();

			if (Platform.isEditor ()) 
			{
				dataPath = Path.Combine (Application.dataPath, "KatsuData");
			} 
			else 
			{
				dataPath = Path.Combine (Application.persistentDataPath, "KatsuData");
			}
			resourcePath = "Katsu";

			// Scan through all classes and register configs and profiles
			Type[] typelist = Assembly.GetExecutingAssembly().GetTypes ();
			
			for (int i = 0; i < typelist.Length; i++)
			{
				// Ignore the class outside of PlayBento namespace
				if(typelist[i].Namespace == typeof(Katsu<T>).Namespace)
				{
					// if the class name is a subclass of the class parameter, add it to the array
					if(typelist[i].IsSubclassOf(typeof(T)))
					{
						objects.Add (Activator.CreateInstance (typelist[i]) as T);
					}
				}
			}
		}

		/// <summary>
		/// Get the object count.
		/// </summary>
		/// <value>The object count.</value>
		public int ObjectCount {
			get { return objects.Count; }
		}

		/// <summary>
		/// Get the Katsu object of the given type.
		/// </summary>
		/// <param name="type">Type name contained in this Katsu.</param>
		public KatsuObject Get (Type type)
		{
			#if FREE_BENTO
			ShowLicenseBadge ();
			#endif
			for (int i = 0; i < objects.Count; i++) {
				if (objects [i].ToString () == type.ToString ()) {
					return objects [i];
				}
			}
			return null;
		}

		/// <summary>
		/// Load all objects. This method must be called before <see cref="Get"/>.
		/// </summary>
		public void Load ()
		{
			#if FREE_BENTO
			ShowLicenseBadge ();
			#endif
			for (int i = 0; i < objects.Count; i++) {
				Read (objects[i]);
			}
		}

		/// <summary>
		/// Save all objects.
		/// </summary>
		public void Save ()
		{
			#if FREE_BENTO
			ShowLicenseBadge ();
			#endif
			if (Platform.isEditor ()) 
			{
				if (!Directory.Exists (dataPath)) {
					Directory.CreateDirectory (dataPath);
				}
			}

			for (int i = 0; i < objects.Count; i++) {
				Write(objects[i]);
			}
		}

		private void Write(T _object)
		{
			// Get class name of the object
			string className = _object.GetType ().ToString ();
			className = className.Substring (className.IndexOf (".") + 1);

			string xml = _object.GetType ().GetMethod ("GetXML").Invoke (_object, null) as string;

			// If this object is untouched, don't save. For performance sake
			if (xml.GetHashCode ().ToString() == _object.GetType ().GetMethod ("GetCode").Invoke (_object, null).ToString()) 
			{
				return;
			}
			Debug.Log (className + " is touched, save");

			if (Platform.isEditor ()) {
				// Write the file
				StreamWriter writer = new StreamWriter (Path.Combine (dataPath, className + ".xml"));
				writer.Write (xml);
				writer.Close ();
			}
			// On mobile, we save the data into player preferences
			else
			{
				PlayerPrefs.SetString(className, xml);
			}
			_object.GetType ().GetMethod ("RefreshCode").Invoke (_object, null);
		}

		private void Read (T _object)
		{
			// Get class name of the object
			string className = _object.GetType ().ToString ();
			className = className.Substring (className.IndexOf (".") + 1);

			string xml = "";

			// On editor, we will check for the files in 'Data' folder
			if (Platform.isEditor ()) {
				if (File.Exists (Path.Combine (dataPath, className + ".xml"))) {
					StreamReader reader = new StreamReader (Path.Combine (dataPath, className + ".xml"));
					xml = reader.ReadToEnd ();
					reader.Close ();
				} else {
					xml = ReadFromResources (className);
				}
			// On mobile and desktop, we will check for the key in Player Preferences first, if not exist, get from Resources
			} else if (Platform.isMobile () || Platform.isDesktop()) {
				if (PlayerPrefs.HasKey(className)) {
					xml = PlayerPrefs.GetString(className);
				} else {
					xml = ReadFromResources (className);
				}
			// On web, we always get from Resources
			} else if (Platform.isWeb ()) {
				xml = ReadFromResources (className);
			}
			// Init the object with the xml loaded
			System.Object[] param = new System.Object[1];
			param [0] = xml;
			_object.GetType ().GetMethod ("Init", new [] {typeof(string)}).Invoke (_object, param);
		}

		private string ReadFromResources(string className)
		{
			TextAsset textAsset = Resources.Load (resourcePath + "/" + className) as TextAsset;
			return textAsset.text;
		}

		#if FREE_BENTO
		private void ShowLicenseBadge()
		{
			if (GameObject.FindObjectOfType (typeof(LicenseBadge)) == null) {
				GameObject licenseBadge = new GameObject();
				licenseBadge.name = "License Badge";
				licenseBadge.AddComponent(typeof(LicenseBadge));
			}
		}
		#endif
	}
}