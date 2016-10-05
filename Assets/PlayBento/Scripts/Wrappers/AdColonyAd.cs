using UnityEngine;
using System.Collections;

namespace PlayBento
{
	public class AdColonyAd : IAdInterface {

		private AdConfig config;

		public void Init()
		{
			config = Local.GetConfig (typeof(AdConfig)) as AdConfig;

			#if UNITY_ANDROID
			AdColony.Configure("1.0", config.Android.AdColony.AppId, config.Android.AdColony.ZoneId);
			#elif UNITY_IPHONE
			AdColony.Configure("1.0", config.iOS.AdColony.AppId, config.iOS.AdColony.ZoneId);
			#endif

			AdColony.OnVideoFinished += OnVideoFinished;
		}

		public void Cache()
		{
		}
		
		public void Show(Ad.Format format)
		{
			if (Ready(format)) 
			{
				#if UNITY_ANDROID
				AdColony.ShowVideoAd (config.Android.AdColony.ZoneId);
				#elif UNITY_IPHONE
				AdColony.ShowVideoAd (config.iOS.AdColony.ZoneId);
				#endif
			} 
			else 
			{
				Debug.Log ("AdColony is not available");
			}
		}

		public bool Ready(Ad.Format format)
		{
			// AdColony serves only video ad
			if (format == Ad.Format.VIDEO) 
			{
				#if UNITY_ANDROID
				return AdColony.IsVideoAvailable (config.Android.AdColony.ZoneId);
				#elif UNITY_IPHONE
				return AdColony.IsVideoAvailable (config.iOS.AdColony.ZoneId);
				#endif
			} 
			else 
			{
				return false;
			}
			return false;
		}

		public int Priority
		{
			#if UNITY_EDITOR
			get{return config.Android.AdColony.priority;}
			#elif UNITY_ANDROID
			get{return config.Android.AdColony.priority;}
			#elif UNITY_IPHONE
			get{return config.iOS.AdColony.priority;}
			#else
			get{return 0;}
			#endif
		}

		private void OnVideoFinished(bool ad_shown)
		{
			Ad.HandleVideoClosed (ad_shown);
		}
	}

}