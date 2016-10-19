using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace PlayBento
{
	public class Push {

		public static string playerID;

		private static List<string> allNotifications;
		
		public static void Init ()
		{
			#if UNITY_ANDROID || UNITY_IPHONE
			OneSignal.Init(
				(Local.GetConfig(typeof(PushConfig)) as PushConfig).AppId, 
				(Local.GetConfig(typeof(PushConfig)) as PushConfig).GCMProjectId
				);
			OneSignal.GetIdsAvailable(IdsAvailable);
			#endif
			allNotifications = new List<string> ();
		}

		public static void ScheduleLocalNotification(string type, string title, string message, int delay)
		{
			// Cancel notification of this type first to prevent repeat notification
			CancelLocalNotification(type);
			
			#if UNITY_IPHONE
			UnityEngine.iOS.LocalNotification notif = new UnityEngine.iOS.LocalNotification();
			IDictionary info = new Dictionary<string, string>();
			info["type"] = type;
			
			notif.alertAction = title;
			notif.alertBody = message;
			notif.fireDate = DateTime.Now.AddSeconds(delay);
			notif.userInfo = info;
			notif.soundName = UnityEngine.iOS.LocalNotification.defaultSoundName;
			notif.applicationIconBadgeNumber = 1;
			UnityEngine.iOS.NotificationServices.ScheduleLocalNotification(notif);
			#elif UNITY_ANDROID
			int notificationId = UnityEngine.Random.Range(0, int.MaxValue);
			PlayerPrefs.SetInt("notif_" + type, notificationId);
			allNotifications.Add(type);
			AndroidNotification.SendNotification(notificationId, delay, title, message, new Color32(0xff, 0x44, 0x44, 255));
			#endif
		}
		
		public static void CancelLocalNotification(string type)
		{
			#if UNITY_IPHONE
			foreach(UnityEngine.iOS.LocalNotification notification in UnityEngine.iOS.NotificationServices.scheduledLocalNotifications)
			{
				if(notification.userInfo["type"].ToString() == type)
				{
					UnityEngine.iOS.NotificationServices.CancelLocalNotification(notification);
				}
			}
			
			#elif UNITY_ANDROID
			AndroidNotification.CancelNotification(PlayerPrefs.GetInt("notif_" + type));
			#endif
		}
		
		public static void CancelAllLocalNotifications()
		{
			#if UNITY_IPHONE
			UnityEngine.iOS.NotificationServices.CancelAllLocalNotifications();
			#elif UNITY_ANDROID
			foreach(string notificaition in allNotifications)
			{
				CancelLocalNotification(notificaition);
			}
			#endif
		}


		private static void IdsAvailable(string _playerID, string _pushToken) 
		{
			playerID = _playerID;
		}
	}
}