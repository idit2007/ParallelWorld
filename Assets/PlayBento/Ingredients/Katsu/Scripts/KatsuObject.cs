using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Reflection;

namespace PlayBento
{
	public class KatsuObject {

		protected string _xml;
		protected int _code;

		public void Init(string xml)
		{
			XmlSerializer xmlSerial = new XmlSerializer(this.GetType());
			System.Object profile = xmlSerial.Deserialize(new StringReader(xml));
			
			FieldInfo[] myObjectFields = this.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
			foreach (FieldInfo fi in myObjectFields)
			{
				fi.SetValue(this, fi.GetValue(profile));
			}
			_xml = xml;
			_code = _xml.GetHashCode ();
		}

		public string GetXML()
		{
			XmlSerializer serializer = new XmlSerializer(this.GetType());
			StringWriter writer = new StringWriter();
			serializer.Serialize(writer, this);

			_xml = writer.ToString ().Replace ("encoding=\"utf-16\"", "encoding=\"utf-8\"");
			return _xml;
		}

		public string GetCode()
		{
			return _code.ToString();
		}

		public void RefreshCode()
		{
			_code = _xml.GetHashCode ();
		}
	}
}