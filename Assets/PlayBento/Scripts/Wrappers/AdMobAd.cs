using UnityEngine;
using System;
using System.Collections;

using GoogleMobileAds;
using GoogleMobileAds.Api;

namespace PlayBento
{
	public class AdMobAd : IAdInterface 
	{
		private InterstitialAd interstitial;
		private string adUnitId;
		private AdConfig config;

		public void Init()
		{
			config = Local.GetConfig (typeof(AdConfig)) as AdConfig;
			#if UNITY_EDITOR
			adUnitId = "unused"; 
			#elif UNITY_ANDROID
			adUnitId = config.Android.AdMob.AdUnit;
			#elif UNITY_IPHONE
			adUnitId = config.iOS.AdMob.AdUnit;
			#else
			adUnitId = "unexpected_platform";
			#endif

			Cache ();
		}
		
		public void Cache()
		{
			// Create an interstitial.
			interstitial = new InterstitialAd(adUnitId);
			// Register for ad events.
			interstitial.OnAdClosed += HandleInterstitialClosed;

			// Load an interstitial ad.
			interstitial.LoadAd(createAdRequest());
		}
		
		public void Show(Ad.Format format)
		{
			if (Ready(format)) 
			{
				interstitial.Show ();
			}
		}

		public bool Ready(Ad.Format format)
		{
			// AdMob serves only interstitial ad
			if (format == Ad.Format.INTERSTITIAL) 
			{
				return interstitial.IsLoaded();
			} 
			else 
			{
				return false;
			}
		}

		public int Priority
		{
			#if UNITY_EDITOR
			get{return config.Android.AdMob.priority;}
			#elif UNITY_ANDROID
			get{return config.Android.AdMob.priority;}
			#elif UNITY_IPHONE
			get{return config.iOS.AdMob.priority;}
			#else
			get{return 0;}
			#endif
		}

		private AdRequest createAdRequest()
		{
			return new AdRequest.Builder().Build();
		}
		
		public void HandleInterstitialClosed(object sender, EventArgs args)
		{
			Cache();
			Debug.Log("HandleInterstitialClosed event received");
		}
	}
}