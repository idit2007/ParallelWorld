using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

namespace PlayBento
{
	public class UnityAd : IAdInterface {

		private AdConfig config;

		public void Init()
		{
			config = Local.GetConfig (typeof(AdConfig)) as AdConfig;

			if (Advertisement.isSupported) {
				Advertisement.allowPrecache = true;
				#if UNITY_ANDROID
				Advertisement.Initialize (config.Android.Unity.AppId);
				#elif UNITY_IPHONE
				Advertisement.Initialize (config.iOS.Unity.AppId);
				#endif
			} else {
				Debug.Log("Platform not supported");
			}

			Cache ();
		}

		public void Cache()
		{
		}
		
		public void Show(Ad.Format format)
		{
			if (Ready(format)) 
			{
				Advertisement.Show("rewardedVideoZone", new ShowOptions {
					pause = true,
					resultCallback = result => {
						//Debug.Log(result.ToString());
						if (result == ShowResult.Finished){Ad.HandleVideoClosed (true);}
						else {Ad.HandleVideoClosed (false);}
					}
				});
			} 
			else 
			{
				Debug.Log ("Unity Ads is not available");
			}
		}

		public bool Ready(Ad.Format format)
		{
			// Unity Ads serves only video ad
			if (format == Ad.Format.VIDEO) 
			{
				return Advertisement.isReady("rewardedVideoZone");
			} 
			else 
			{
				return false;
			}
		}
		
		public int Priority
		{
			#if UNITY_EDITOR
			get{return config.Android.Unity.priority;}
			#elif UNITY_ANDROID
			get{return config.Android.Unity.priority;}
			#elif UNITY_IPHONE
			get{return config.iOS.Unity.priority;}
			#else
			get{return 0;}
			#endif
		}
	}
}