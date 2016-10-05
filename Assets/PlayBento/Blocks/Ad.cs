using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;

namespace PlayBento
{
	public class Ad {
		public enum Format
		{
			INTERSTITIAL,
			VIDEO
		};
		public enum Network
		{
			ADMOB,
			CHARTBOOST,
			UNITY,
			ADCOLONY
		}
		public static event Action<bool> OnVideoClosed;

		private static List<IAdInterface> ads;

		/// <summary>
		/// Initialize Ad block. Call this method before calling any other method
		/// </summary>
		public static void Init()
		{
			ads = new List<IAdInterface> ();

			ads.Add(new AdMobAd());
			ads.Add(new ChartboostAd());
			ads.Add(new UnityAd());
			ads.Add(new AdColonyAd());

			foreach (IAdInterface ad in ads) 
			{
				ad.Init();
			}
		}

		/// <summary>
		/// Check if any ad with given format is ready at the time
		/// </summary>
		public static bool Ready(Format format)
		{
			// Run through all ads
			for(int i = 1; i <= ads.Count; i++)
			{
				// Get current highest priority ad
				IAdInterface highestPriorityAd = GetAdAtPriority(i);
				
				// Check if not null and see if it is ready
				if(highestPriorityAd != null)
				{
					if(highestPriorityAd.Ready(format))
					{
						return true;
					}
					else
					{
						highestPriorityAd.Cache();
					}
				}
			}
			return false;
		}

		/// <summary>
		/// Show the ad from highest priority ad network available at that time
		/// </summary>
		public static void Show(Format format)
		{
			// Run through all ads
			for(int i = 1; i <= ads.Count; i++)
			{
				// Get current highest priority ad
				IAdInterface highestPriorityAd = GetAdAtPriority(i);

				// Check if not null and ready, otherwise, cache for next time
				if(highestPriorityAd != null)
				{
					if(highestPriorityAd.Ready(format))
					{
						highestPriorityAd.Show(format);
						break;
					}
					else
					{
						highestPriorityAd.Cache();
					}
				}
			}
		}

		/// <summary>
		/// Show ad from the specified network
		/// </summary>
		/// <param name="network">Network</param>
		public static void Show(Format format, Network network)
		{
			ads [(int)network].Show (format);
		}

		public static void HandleVideoClosed(bool finished)
		{
			if (OnVideoClosed != null) OnVideoClosed(finished);
		}

		private static IAdInterface GetAdAtPriority(int priority)
		{
			foreach(IAdInterface ad in ads)
			{
				if(ad.Priority == priority)
				{
					return ad;
				}
			}
			return null;
		}
	}
}