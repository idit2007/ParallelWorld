using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Parse;
using System.Threading.Tasks;

namespace PlayBento
{
	public class Cloud : MonoBehaviour {

		public enum CloudAction
		{
			SUBMITUSER,
			SUBMITSCORE,
			GETTOPFRIEND,
			GETTOPWORLD,
			SAVEPROFILE,
			LOADPROFILE,
			ACCEPTGIFT,
			IGNOREGIFT
		}

		public static readonly DateTime unixTime = new DateTime (1970, 1, 1);

		public delegate void CloudCallback();
		public delegate void CloudSuccessCallback(bool succes);
		public delegate void CloudDataCallback(List<UserInfo> userInfoList);
		public delegate void CloudRequestCallback(bool success, int delay);

		private static ParseCloud _cloud = null;
		protected static ParseCloud cloud
		{
			get
			{
				if(_cloud == null)
				{
					_cloud = GameObject.Find("PlayBentoObject").GetComponent<ParseCloud>();
				}
				return _cloud;
			}
		}

		public static bool IsConnectingInternet {
			get {
				return Application.internetReachability != NetworkReachability.NotReachable;
			}
		}

		/// <summary>
		/// Load user profiles form cloud. Social network connect is required
		/// </summary>
		/// <param name="callback">Callback</param>
		public static void LoadProfile(CloudSuccessCallback callback = null)
		{
			if(!Social.IsLoggedIn)
			{
				Debug.Log("Please login to a social network first");
				if(callback != null)
					callback(false);
				return;
			}
			cloud.StartCoroutine(cloud.ParseSaveLoadProfile(CloudAction.LOADPROFILE, callback));
		}
		/// <summary>
		/// Save user profiles to cloud. Social network connect is required
		/// </summary>
		/// <param name="callback">Callback</param>
		public static void SaveProfile(CloudSuccessCallback callback = null)
		{
			if(!Social.IsLoggedIn)
			{
				Debug.Log("Please login to a social network first");
				if(callback != null)
					callback(false);
				return;
			}
			cloud.StartCoroutine(cloud.ParseSaveLoadProfile(CloudAction.SAVEPROFILE, callback));
		}

		/// <summary>
		/// Get the timestamp
		/// </summary>
		public static int GetTimestamp()
		{
			return (int)(DateTime.Now.ToUniversalTime() - unixTime).TotalSeconds;
		}

		/*! \cond PRIVATE */

		/// <summary>
		/// Submit user info to cloud
		/// </summary>
		public static void SubmitUser(string  uId, Social.SocialNetwork network, string first_name, string last_name, 
		                              string real_name, string email, string address, string tel, 
		                              string country)
		{
			Dictionary<string, string> dataDic = new Dictionary<string, string>();
			dataDic["uid"] = uId;
			dataDic["network"] = network.ToString();
			dataDic["first_name"] = first_name;
			dataDic["last_name"] = last_name;
			dataDic["real_name"] = real_name;
			dataDic["email"] = email;
			dataDic["address"] = address;
			dataDic["tel"] = tel;
			dataDic["country"] = country;
			dataDic["push_id"] = Push.playerID;
			cloud.StartCoroutine(cloud.ParseSubmit(uId, network.ToString(), dataDic, CloudAction.SUBMITUSER));
		}
		/// <summary>
		/// Send score to cloud
		/// </summary>
		public static void SubmitScore(string uid, Social.SocialNetwork network, string first_name, string last_name, int score)
		{
			Dictionary<string, string> dataDic = new Dictionary<string, string>();
			dataDic["score"] = score.ToString();
			dataDic["uid"] = uid;
			dataDic["network"] = network.ToString();
			dataDic["first_name"] = first_name;
			dataDic["last_name"] = last_name;
			cloud.StartCoroutine(cloud.ParseSubmit(uid, network.ToString(), dataDic, CloudAction.SUBMITSCORE));
		}
		/// <summary>
		/// Get top friends
		/// </summary>
		public static void GetTopFriends(Social.SocialNetwork network, int limit, CloudDataCallback callback)
		{
			cloud.StartCoroutine(cloud.ParseGetTop(network.ToString(), CloudAction.GETTOPFRIEND, limit, callback));
		}
		/// <summary>
		/// Get top world
		/// </summary>
		public static void GetTopWorld(Social.SocialNetwork network, int limit, CloudDataCallback callback)
		{
			cloud.StartCoroutine(cloud.ParseGetTop(network.ToString(), CloudAction.GETTOPWORLD, limit, callback));
		}
		/// <summary>
		/// Send gift to friend
		/// </summary>
		public static void SendGift(string receiverId, Social.SocialNetwork network, string payload, CloudRequestCallback callback)
		{
			cloud.StartCoroutine(cloud.ParseSendGift(receiverId, network.ToString(), payload, callback));
		}
		/// <summary>
		/// Get user gift list
		/// </summary>
		public static void GetGift(List<RequestInfo> requestInfoList, Social.SocialCallback callback)
		{
			cloud.StartCoroutine(cloud.ParseGetGift(requestInfoList, callback));
		}
		/// <summary>
		/// Accept the gift
		/// </summary>
		public static void AcceptGift(string requestId, Social.SocialSuccessCallback callback)
		{
			cloud.StartCoroutine(cloud.ParseGift(CloudAction.ACCEPTGIFT, requestId, callback));
		}
		/// <summary>
		/// Ignore the gift
		/// </summary>
		public static void IgnoreGift(string requestId, Social.SocialSuccessCallback callback)
		{
			cloud.StartCoroutine(cloud.ParseGift(CloudAction.IGNOREGIFT, requestId, callback));
		}

		/*! \endcond */
	}
}