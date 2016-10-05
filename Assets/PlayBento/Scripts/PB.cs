using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook;
using Parse;
using ChartboostSDK;

namespace PlayBento
{
	public class PB : ScriptableObject{
		private static bool isInit = false;

		/// <summary>
		/// Initialize Play Bento. Call this method before calling any other Play Bento method
		/// </summary>
		public static void Init()
		{
			if(!isInit)
			{
				GameObject playBento = new GameObject();
				playBento.name = "PlayBentoObject";

				if(SocialSetting.IsUseSocial)
				{
					PlayBento.Social.Init();
				}
				Local.Init();

                // Initialize Parse
				GameObject parse = new GameObject("ParseObject");
				ParseInitializeBehaviour parseInitailize = parse.AddComponent<ParseInitializeBehaviour>();
				ParseClient.Configuration config = new ParseClient.Configuration();
                
                // Leave server config as null if we intend to use Parse.com
                string serverURL = (Local.GetConfig(typeof(CloudConfig)) as CloudConfig).Server;
                if(!String.IsNullOrEmpty(serverURL))
                {
                    config.Server = serverURL;
                }
                
                config.ApplicationId = (Local.GetConfig(typeof(CloudConfig)) as CloudConfig).ApplicationID;
                config.WindowsKey = (Local.GetConfig(typeof(CloudConfig)) as CloudConfig).DotNetKey;
                ParseClient.Initialize(config);
				ParseAnalytics.TrackAppOpenedAsync();

				playBento.AddComponent<ParseCloud>();

				GameObject openIAB = new GameObject();
				openIAB.AddComponent<OpenIABEventManager>();

				GameObject chartboost = new GameObject("Chartboost");
				chartboost.AddComponent<Chartboost>();

				GameObject adColony = new GameObject("AdColony");
				adColony.AddComponent<AdColony>();

				Ad.Init();
				IAP.Init();
				Push.Init();

				GameObject playBentoGroup = new GameObject();
				playBentoGroup.name = "PlayBento";
				DontDestroyOnLoad(playBentoGroup);

				playBento.transform.parent = playBentoGroup.transform;
				parse.transform.parent = playBentoGroup.transform;
				openIAB.transform.parent = playBentoGroup.transform;
				chartboost.transform.parent = playBentoGroup.transform;
				adColony.transform.parent = playBentoGroup.transform;

				isInit = true;
			}
		}
	}
}
