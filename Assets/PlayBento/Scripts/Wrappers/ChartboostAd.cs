using UnityEngine;
using System.Collections;
using ChartboostSDK;

namespace PlayBento
{
	public class ChartboostAd : IAdInterface {

		private bool videoCompleted = false;
		private AdConfig config;

		public void Init()
		{
			config = Local.GetConfig (typeof(AdConfig)) as AdConfig;

			#if UNITY_ANDROID || UNITY_IPHONE
			Chartboost.didCacheInterstitial += didCacheInterstitial;
			Chartboost.didCacheRewardedVideo += didCacheRewardedVideo;
			Chartboost.didFailToLoadInterstitial += didFailToLoadInterstitial;
			Chartboost.didFailToLoadRewardedVideo += didFailToLoadRewardedVideo;
			Chartboost.didDisplayInterstitial += didDisplayInterstitial;
			Chartboost.didDismissInterstitial += didDismissInterstitial;
			Chartboost.didCompleteRewardedVideo += didCompleteRewardedVideo;
			Chartboost.didCloseRewardedVideo += didCloseRewardedVideo;
			#endif

			// Initialize is now handled in the editor

			Cache ();
		}

		public void Cache()
		{
			#if UNITY_ANDROID || UNITY_IPHONE
			Chartboost.cacheRewardedVideo(CBLocation.Default);
			Chartboost.cacheInterstitial(CBLocation.Default);
			#endif
		}
		
		public void Show(Ad.Format format)
		{
			#if UNITY_ANDROID || UNITY_IPHONE
			if(format == Ad.Format.INTERSTITIAL)
			{
				Chartboost.showInterstitial (CBLocation.Default);
			}
			else if(format == Ad.Format.VIDEO)
			{
				Chartboost.showRewardedVideo(CBLocation.Default);
			}
			#endif
		}

		public bool Ready(Ad.Format format)
		{
			if(format == Ad.Format.INTERSTITIAL)
			{
				return Chartboost.hasInterstitial(CBLocation.Default);
			}
			else if(format == Ad.Format.VIDEO)
			{
				return Chartboost.hasRewardedVideo(CBLocation.Default);
			}
			else
			{
				return false;
			}
		}
		
		public int Priority
		{
			#if UNITY_EDITOR
			get{return config.Android.Chartboost.priority;}
			#elif UNITY_ANDROID
			get{return config.Android.Chartboost.priority;}
			#elif UNITY_IPHONE
			get{return config.iOS.Chartboost.priority;}
			#else
			get{return 0;}
			#endif
		}

		#if UNITY_ANDROID || UNITY_IPHONE
		private void didCacheInterstitial( CBLocation location ) {
			Debug.Log( "didCacheInterstitialEvent: " + location );
		}

		private void didCacheRewardedVideo( CBLocation location ) {
			Debug.Log( "didCacheRewardedVideo: " + location );
		}

		private void didFailToLoadInterstitial(CBLocation location, CBImpressionError error) {
			Debug.Log(string.Format("didFailToLoadInterstitialEvent: {0} at location {1}", error, location));
		}

		private void didFailToLoadRewardedVideo(CBLocation location, CBImpressionError error){
			Debug.Log(string.Format("didFailToLoadRewardedVideo: {0} at location {1}", error, location));
		}
		
		private void didDisplayInterstitial( CBLocation location ) {
			Debug.Log( "didDisplayInterstitial: " + location );
		}

		private void didDismissInterstitial( CBLocation location ) {
			Debug.Log( "didDismissInterstitial: " + location );
		}

		private void didCompleteRewardedVideo( CBLocation location, int reward ){
			videoCompleted = true;
			Debug.Log( "didCompleteRewardedVideo: " + location );
		}

		private void didCloseRewardedVideo( CBLocation location )
		{
			Ad.HandleVideoClosed (videoCompleted);
			videoCompleted = false;
			Debug.Log( "didCloseRewardedVideo: " + location );
		}
		#endif
	}
}