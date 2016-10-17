using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Facebook.Unity;

namespace PlayBento
{
	public class Analytic {

		public enum Provider
		{
			Facebook,
			Parse
		}

		// Default provider is Facebook
		private static Provider _currentProvider = Provider.Facebook;

		/// <summary>
		/// Get current using provider
		/// </summary>
		/// <value>Current provider</value>
		public static Provider CurrentProvider {
			get { return _currentProvider; }
		}

		/// <summary>
		/// Select the provider to connect with
		/// </summary>
		/// <param name="provider">Provider</param>
		public static void SelectProvider(Provider provider)
		{
			_currentProvider = provider;
		}

		/// <summary>
		/// Send an event to be tracked by the analytic system
		/// </summary>
		/// <param name="name">Name of the event</param>
		/// <param name="name">Payload of the event</param>
		public static void LogEvent (string name, Dictionary<string, string> dimensions = null)
		{
			switch(_currentProvider)
			{
				case Provider.Facebook:
					if(!FB.IsInitialized) {return;}

					if(dimensions != null){
						// Convert <string, string> to <string, object>
						Dictionary<string, object> dict = new Dictionary<string, object>();
						foreach (KeyValuePair<string, string> item in dimensions)
						{
							dict.Add(item.Key, item.Value);
						}
						
						FB.LogAppEvent(name, 0, dict);
					}
					else
					{
						FB.LogAppEvent(name, 0, null);
					}
				break;

				case Provider.Parse:
					Parse.ParseAnalytics.TrackEventAsync(name, dimensions).ContinueWith(t => {
						// Do nothing
					});
				break;
			}
		}
	}
}