using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Parse;
using System.Threading.Tasks;

namespace PlayBento
{
	public class ParseCloud : MonoBehaviour {

		public IEnumerator ParseSubmit(string uid, string network, Dictionary<string, string> dataDic, Cloud.CloudAction actionID)
		{
			Debug.Log("Submit = " + actionID);
			string parseClass = "";
			if(actionID == Cloud.CloudAction.SUBMITUSER)
			{
				parseClass = "UserInfo";
			}
			else if(actionID == Cloud.CloudAction.SUBMITSCORE)
			{
				parseClass = "Score";
			}
			ParseObject parseObject = null;
			ParseQuery<ParseObject> query = ParseObject.GetQuery(parseClass).
				WhereEqualTo("network", network).
					WhereEqualTo("uid", uid);
			
			Task<ParseObject> task;
			task = query.FirstAsync();
			
			while(!task.IsCompleted) { yield return null; }

			if(task.IsFaulted)
			{
				ParseException parseEx = task.Exception.InnerExceptions[0] as ParseException;
				
				// If any error except object-not-found occurs, stop and return
				if(parseEx.Code != ParseException.ErrorCode.ObjectNotFound)
				{
					yield break;
				}
			}
			else
			{
				// There is an object found, store it
				parseObject = task.Result as ParseObject;
			}

			if( task.IsCanceled)
			{
				yield break;
			}
			
			if(parseObject == null)
			{
				parseObject = new ParseObject(parseClass);
			}
			
			Task save = null;

			if(parseClass == "UserInfo")
			{
				#if UNITY_ANDROID
				parseObject["onAndroid"] = "YES";
				#elif UNITY_IOS || UNITY_IPHONE
				parseObject["onIos"] = "YES";
				#elif UNITY_WEBPLAYER
				parseObject["onFacebook"] = "YES";
				#endif
			}

			foreach(KeyValuePair<string, string> item in dataDic)
			{
				if(actionID == Cloud.CloudAction.SUBMITSCORE)
				{
					Debug.Log(item.Key + ": " + item.Value);
				}

				if(item.Key == "score")
				{
					if(!parseObject.Keys.Contains("score") || parseObject.Get<int>(item.Key) < int.Parse(item.Value))
					{
						parseObject[item.Key] = int.Parse(item.Value);
					}
				}
				else { parseObject[item.Key] = item.Value; }
			}
			save = parseObject.SaveAsync();
			
			while(save != null && !save.IsCompleted) { yield return null; }
			Debug.Log(actionID + " Finished!");
		}

		public IEnumerator ParseGetTop(string network, Cloud.CloudAction actionID, int limit, Cloud.CloudDataCallback callback)
		{
			List<UserInfo> userInfoList = new List<UserInfo>();
			
			ParseQuery<ParseObject> query = ParseObject.GetQuery("Score").
					WhereEqualTo("network", network).
					OrderByDescending("score").
					Limit(limit);
			List<string> friendIDList = new List<string>();
			if(actionID == Cloud.CloudAction.GETTOPFRIEND)
			{
				for(int i = 0; i < Social.FriendList.Count; i++)
				{
					friendIDList.Add(Social.FriendList[i].id);
				}
				friendIDList.Add(Social.UserID);
				query = query.WhereContainedIn("uid", friendIDList);
			}
			
			Task<IEnumerable<ParseObject>> task;
			IEnumerable<ParseObject> parseScoreList = null;
			task = query.FindAsync();
			
			while(!task.IsCompleted) { yield return null; }

			if(task.IsFaulted)
			{
				ParseException parseEx = task.Exception.InnerExceptions[0] as ParseException;
				
				// If any error except object-not-found occurs, stop and return
				if(parseEx.Code != ParseException.ErrorCode.ObjectNotFound)
				{
					if(callback != null)
					{
						callback(userInfoList);
					}
					yield break;
				}
			}
			else
			{
				// There is an object found, store it
				parseScoreList = task.Result as IEnumerable<ParseObject>;
			}
			
			if(parseScoreList != null)
			{
				foreach(ParseObject parseScoreObject in parseScoreList)
				{
					UserInfo userScoreInfo = new UserInfo();
					userScoreInfo.id = parseScoreObject.Get<string>("uid");
					userScoreInfo.score = parseScoreObject.Get<int>("score");
					userScoreInfo.firstName = parseScoreObject.Get<string>("first_name");
					userScoreInfo.lastName = parseScoreObject.Get<string>("last_name");
					PlayBento.Social.LoadProfilePicture(userScoreInfo.id, userScoreInfo.SetPicture);
					userInfoList.Add(userScoreInfo);
				}
			}
			
			Debug.Log("Get Finish!!");
			if(callback != null)
			{
				callback(userInfoList);
			}
		}

		public IEnumerator ParseSaveLoadProfile(Cloud.CloudAction actionID , Cloud.CloudSuccessCallback callback = null)
		{
			Debug.Log("SaveLoadProfile");
			List<BentoProfile> profileList;
			ParseObject parseObject = null;
			ParseQuery<ParseObject> query = ParseObject.GetQuery("Save").
				WhereEqualTo("network", Social.Network.ToString()).
					WhereEqualTo("uid", Social.UserID);
			
			Task<ParseObject> task;
			task = query.FirstAsync();

			while(!task.IsCompleted) { yield return null; }
			
			if(task.IsCanceled)
			{
				if(callback != null){callback(false);}
				yield break;
			}
			
			if(task.IsFaulted)
			{
				ParseException parseEx = task.Exception.InnerExceptions[0] as ParseException;

				// If any error except object-not-found occurs, stop and return
				if(parseEx.Code != ParseException.ErrorCode.ObjectNotFound)
				{
					if(callback != null){callback(false);}
					yield break;
				}
			}
			else
			{
				// There is an object found, store it
				parseObject = task.Result as ParseObject;
			}
			
			if(actionID == Cloud.CloudAction.LOADPROFILE)
			{
				if(parseObject != null)
				{
					Katsu<BentoProfile> profileKatsu = new Katsu<BentoProfile>();
					profileList = profileKatsu.Objects;
					foreach(BentoProfile profile in profileList)
					{
						profile.Init(parseObject.Get<string>(profile.GetType().Name));
					}
					Local.UpdateProfileKatsu(profileKatsu);
				}
				else
				{
					Debug.Log("No Profile On Cloud");
				}
				if(callback != null) { callback(true); }
			}
			else if(actionID == Cloud.CloudAction.SAVEPROFILE)
			{
				if(parseObject == null)
				{
					parseObject = new ParseObject("Save");
				}
				
				profileList = Local.GetProfileKatsu().Objects;
				Task saveTask = null;

				parseObject["uid"] = Social.UserID;
				parseObject["network"] = Social.Network.ToString();
				foreach(BentoProfile profile in profileList)
				{
					parseObject[profile.GetType().Name] = profile.GetXML();
				}
				saveTask = parseObject.SaveAsync();

				while(!saveTask.IsCompleted) { yield return null; }
				
				if(callback != null)
				{
					if(saveTask.IsFaulted || saveTask.IsCanceled)
					{
						callback(false);
					}
					else
					{
						callback(true);
					}
				}
			}
			Debug.Log("Save/Load Profile Finish!!");
		}

		public IEnumerator ParseSendGift(string receiverId, string network, string payload,Cloud.CloudRequestCallback callback)
		{
			Debug.Log("Send Gift");
			int timeDelay;
			ParseObject giftData = null;
			ParseQuery<ParseObject> query = ParseObject.GetQuery("Gift").
				WhereEqualTo("network", network).
					WhereEqualTo("receiver", receiverId).
					WhereEqualTo("sender", Social.UserID).
					OrderByDescending("delaytime");
			
			Task<IEnumerable<ParseObject>> task;
			IEnumerable<ParseObject> giftDataList = null;
			task = query.FindAsync();
			
			while(!task.IsCompleted)
			{
				yield return null;
			}

			if(task.IsCanceled)
			{
				if(callback != null){callback(false, 0);}
				yield break;
			}
			
			if(task.IsFaulted)
			{
				ParseException parseEx = task.Exception.InnerExceptions[0] as ParseException;
				
				// If any error except object-not-found occurs, stop and return
				if(parseEx.Code != ParseException.ErrorCode.ObjectNotFound)
				{
					if(callback != null){callback(false, 0);}
					yield break;
				}
			}
			else
			{
				// There is an object found, store it
				giftDataList = task.Result as IEnumerable<ParseObject>;
			}
			
			if(giftDataList != null)
			{
				List<ParseObject> gDataList = new List<ParseObject>(giftDataList);
				if(gDataList.Count > 0)
				{
					timeDelay = gDataList[0].Get<int>("delaytime") - Cloud.GetTimestamp();
					if(timeDelay > 0)
					{
						Debug.Log("Send Gift Delay");
						callback(false, timeDelay);
						yield break;
					}
				}
				
			}
			
			giftData = new ParseObject("Gift");
			timeDelay = Cloud.GetTimestamp() + (Local.GetConfig(typeof(SocialConfig)) as SocialConfig).GiftDelay;
			
			Task save = null;
			giftData["network"] = network;
			giftData["receiver"] = receiverId;
			giftData["sender"] = Social.UserID;
			giftData["first_name"] = Social.FirstName;
			giftData["last_name"] = Social.LastName;
			giftData["payload"] = payload;
			giftData["delaytime"] = timeDelay;
			giftData["accept_status"] = "N";
			save = giftData.SaveAsync();
			
			while(!save.IsCompleted && !save.IsCanceled){ yield return null; }
			
			callback(true, 0);
			Debug.Log("Finish Send Gift");
			
		}

		public IEnumerator ParseGetGift(List<RequestInfo> requestInfoList, Social.SocialCallback callback)
		{
			Debug.Log("Get Gift");
			requestInfoList.Clear();
			ParseQuery<ParseObject> query = ParseObject.GetQuery("Gift").
				WhereEqualTo("network", Social.Network.ToString()).
					WhereEqualTo("receiver", Social.UserID).
					WhereEqualTo("accept_status", "N").
					OrderByDescending("delaytime");
			
			Task<IEnumerable<ParseObject>> task;
			IEnumerable<ParseObject> giftDataList = null;
			task = query.FindAsync();
			
			while(!task.IsCompleted){ yield return null; }

			if(task.IsCanceled)
			{
				if(callback != null){callback();}
				yield break;
			}
			
			if(task.IsFaulted)
			{
				ParseException parseEx = task.Exception.InnerExceptions[0] as ParseException;
				
				// If any error except object-not-found occurs, stop and return
				if(parseEx.Code != ParseException.ErrorCode.ObjectNotFound)
				{
					if(callback != null){callback();}
					yield break;
				}
			}
			else
			{
				// There is an object found, store it
				giftDataList = task.Result as IEnumerable<ParseObject>;
			}
			
			if(giftDataList != null)
			{
				foreach(ParseObject giftData in giftDataList)
				{
					RequestInfo requestInfo = new RequestInfo();
					requestInfo.requestId = giftData.ObjectId;
					requestInfo.senderId = giftData.Get<string>("sender");
					requestInfo.payload = giftData.Get<string>("payload");
					requestInfo.firstName = giftData.Get<string>("first_name");
					requestInfo.lastName = giftData.Get<string>("last_name");
					PlayBento.Social.LoadProfilePicture(requestInfo.senderId, requestInfo.SetPicture);
					requestInfoList.Add(requestInfo);
				}
				
			}
			
			Debug.Log("Finish Get Gift");
			callback();
		}

		public IEnumerator ParseGift(Cloud.CloudAction actionID, string requestId, Social.SocialSuccessCallback callback)
		{
			Debug.Log("Gift");
			ParseQuery<ParseObject> query = ParseObject.GetQuery("Gift");
			ParseObject requestData = null;
			Task<ParseObject> task;
			task = query.GetAsync(requestId);
			
			while(!task.IsCompleted){ yield return null; }

			if(task.IsCanceled)
			{
				if(callback != null) { callback(false); }
				yield break;
			}
			
			if(!task.IsFaulted)
			{
				requestData = task.Result as ParseObject;
			}
			
			if(requestData != null)
			{
				Task save = null;
				if(actionID == Cloud.CloudAction.ACCEPTGIFT)
					requestData["accept_status"] = "Y";
				else if(actionID == Cloud.CloudAction.IGNOREGIFT)
					requestData["accept_status"] = "I";
				save = requestData.SaveAsync();
				
				while(!save.IsCompleted && !save.IsCanceled){ yield return null; }
				
				if(callback != null)
				{
					if(save.IsFaulted)
					{
						callback(false);
					}
					else
					{
						callback(true);
						for(int i = 0; i < PlayBento.Social.Gifts.Count; i++)
						{
							if(PlayBento.Social.Gifts[i].requestId == requestId)
							{
								PlayBento.Social.Gifts.RemoveAt(i);
								break;
							}
						}
					}
				}
				Debug.Log("Finish Gift");
			}
			else
			{
				if(callback != null)
				{
					callback(false);
				}
			}
		}
	}
}