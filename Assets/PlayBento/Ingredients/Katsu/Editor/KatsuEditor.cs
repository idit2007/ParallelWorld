using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace PlayBento
{
	public class KatsuEditor : MonoBehaviour {

		/*
		[MenuItem ("PlayBento/Generate XML", false, 500)]
		public static void GenerateXML()
		{
			Generate (new Type[]{typeof(BentoProfile), typeof(BentoConfig)});
		}
		*/
		// We want this menu only in Katsu standalone, if there is
		[MenuItem ("PlayBento/Generate XML", false, 500)]
		public static void GenerateAllKatsuObjects()
		{
			Generate (new Type[]{typeof(KatsuObject)});
		}

		public static void Generate (Type[] list) 
		{
			// Sample: Run through all classes in the same namespace of KatsuObject
			Type[] types = Assembly.GetAssembly(typeof(KatsuObject)).GetTypes ();

			List<KatsuObject> objects = new List<KatsuObject>();

			for (int i = 0; i < types.Length; i++)
			{
				foreach(Type t in list)
				{
					if(types[i].IsSubclassOf(t))
					{
						objects.Add(Activator.CreateInstance(types[i]) as KatsuObject);
					}
				}
			}

			if(objects.Count > 0)
			{
				for(int i = 0; i < objects.Count; i++)
				{
					string className = objects[i].GetType().ToString();
					className = className.Substring(className.LastIndexOf(".") + 1);

					if(className == "ShareConfig")
					{
						continue;
					}

					string filePath = Path.Combine(Application.dataPath,"Resources/Katsu/" + className + ".xml");

					FieldInfo[] myObjectFields = objects[i].GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
					if(myObjectFields.Length == 0)
					{
						continue;
					}
					objects[i] = InitObject(objects[i]) as KatsuObject;

					if(!File.Exists(filePath))
					{
						XmlSerializer serializer = new XmlSerializer(objects[i].GetType());
						StringWriter writer = new StringWriter();
						serializer.Serialize(writer, objects[i]);

						StreamWriter steramWriter = new StreamWriter(filePath);
						steramWriter.Write(writer.ToString().Replace("encoding=\"utf-16\"", "encoding=\"utf-8\""));
						steramWriter.Close();

						Debug.Log(className + " was created");
					}
				}
				Debug.Log("All Katsu objects created successfully, existing files were not rewritten");
			}
		}

		static System.Object InitObject(System.Object o)
		{
			FieldInfo[] myObjectFields = o.GetType ().GetFields (BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
			foreach (FieldInfo fi in myObjectFields) {
				// Field is primitive type
				if (fi.FieldType.IsPrimitive || fi.FieldType == typeof(System.String)) {
					// Set default value for each primitive type
					if(fi.FieldType == typeof(int))	{fi.SetValue (o, default(int));}
					else if(fi.FieldType == typeof(float)) {fi.SetValue (o, default(float));}
					else if(fi.FieldType == typeof(bool)) {fi.SetValue (o, default(bool));}
					else if(fi.FieldType == typeof(System.String)){fi.SetValue (o, "[string]");}
				}
				// Field is List type
				else if (fi.FieldType.ToString().Contains("List")) {
					// Get the type contained in the list
					System.Object temp = Activator.CreateInstance(fi.FieldType);
					Type type = temp.GetType().GetGenericArguments()[0];

					if(type.IsPrimitive || type == typeof(System.String))
					{
						Type listType = typeof(List<>).MakeGenericType(type);
						IList list = (IList) Activator.CreateInstance(listType);

						// Set default value for each primitive type
						if(type == typeof(int))	{list.Add(default(int));}
						else if(type == typeof(float)) {list.Add(default(float));}
						else if(type == typeof(bool)) {list.Add(default(bool));}
						else if(type == typeof(System.String)){list.Add("[string]");}

						fi.SetValue (o, list);
					}	
					else
					{
						System.Object m = Activator.CreateInstance(type);
						m = InitObject(m);

						Type listType = typeof(List<>).MakeGenericType(type);
						IList list = (IList) Activator.CreateInstance(listType);
						list.Add(m);
						list.Add(m);
						fi.SetValue(o, list);
					}
				}
				// Field is Object type
				else{
					System.Object f = Activator.CreateInstance(fi.FieldType);
					f = InitObject(f);
					fi.SetValue (o, f);
				}
			}
			return o;
		}
	}
}