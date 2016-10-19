using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Facebook;
using Facebook.MiniJSON;
using Facebook.Unity;

namespace PlayBento
{
	public class FacebookSocial : ISocialInterface {

		public Social.SocialSuccessCallback postAchievementCallback = null;
		public Social.SocialSuccessCallback postRecommendCallback = null;
		public Social.SocialSuccessCallback postScoreCallback = null;
		public Social.SocialSuccessCallback inviteCallback = null;
		public Social.SocialDataCallback sendGiftCallback = null;

		private Social.SocialSuccessCallback postCallback = null;
		private string sendGiftReceiverId = "";
		private string sendGiftObjectId = "";
		private string sendGiftMessage = "";


		/// <summary>
		/// Get facebook is login or not.
		/// </summary>
		/// <value><c>true</c> if this instance is logged in; otherwise, <c>false</c>.</value>
		public override bool IsLoggedIn
		{
			get{ return FB.IsLoggedIn; }
		}
		/// <summary>
		/// Get facebook userid;
		/// </summary>
		/// <value>The user identifier.</value>
		public override string UserId
		{
			get
			{
				var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;
				return aToken.UserId;
			}
		}

		private SocialConfig socialConfig = null;

		public override void Init ()
		{
			FB.Init (OnFacebookInitComplete);
		}

		private void OnFacebookInitComplete()
		{
			Debug.Log("Facebook InitComplete");
			FB.ActivateApp ();
			isInit = true;
		}

		/// <summary>
		/// Login to Facebook. The user's (App scope) ID, email, friends will be retrieved
		/// User data will be submitted with Bento Cloud
		/// </summary>
		/// <param name="callBack">Call back.</param>
		public override void Login(Social.SocialSuccessCallback callBack)
		{
			if(Social.isLoading)
			{
				Debug.LogWarning("Is Loading!");
				return;
			}

			isLoading = true;
			limitTopWorld = -1;
			limitTopFriend = -1;
			loginCallback = callBack;

			if(!Social.IsInit)
			{
				StartCoroutine(WaitingLogin());
			}
			else
			{
				LoginFB();
			}

		}

		IEnumerator WaitingLogin()
		{
			while(!isInit){yield return null;}

			LoginFB();
		}

		/// <summary>
		/// Login to Facebook and get complete data of this user including
		/// ID, email, friends, score, gifts, top friends, top world
		/// </summary>
		/// <param name="pLimitTopWorld">Limit top world.</param>
		/// <param name="pLimitTopFriend">Limit top friend.</param>
		/// <param name="callBack">Call back.</param>
		public override void LoginWithCompleteData(int _limitTopWorld, int _limitTopFriend, Social.SocialSuccessCallback callBack)
		{
			if(Social.isLoading)
			{
				Debug.LogWarning("Is Loading!");
				return;
			}

			isLoading = true;
			limitTopWorld = _limitTopWorld;
			limitTopFriend = _limitTopFriend;
			loginCallback = callBack;

			if(!Social.IsInit)
			{
				StartCoroutine(WaitingLogin());
			}
			else
			{
				LoginFB();
			}
		}

		private void LoginFB()
		{
			FB.LogInWithReadPermissions(GetReadScope(), LoginReadHandle);
        }
		/// <summary>
		/// Log out Facebook.
		/// </summary>
		public override void LogOut()
		{
			FB.LogOut();
		}

		private void LoginReadHandle(IResult result)
		{
			if(result.Error != null || !FB.IsLoggedIn || string.IsNullOrEmpty(UserId))
			{
				if(result.Error != null)Debug.LogError(result.Error);
				else Debug.LogError("no login");
				isLoading = false;
				if(loginCallback != null)
				{
					loginCallback(false);
				}

				loginCallback = null;
			}
			else if(FB.IsLoggedIn)
			{
                if ((PlayBento.Local.GetConfig(typeof(SocialConfig)) as SocialConfig).PublishPermission)
                {
                    FB.LogInWithPublishPermissions(new List<string>() { "publish_actions" });
                }
                else
                {
                    GetData();
                }
                
			}
		}

        private void LogiPublishHandle(IResult result)
        {
            if (result.Error != null || !FB.IsLoggedIn || string.IsNullOrEmpty(UserId))
            {
                if (result.Error != null) Debug.LogError(result.Error);
                else Debug.LogError("no login");
                LogOut();
                isLoading = false;
                if (loginCallback != null)
                {
                    loginCallback(false);
                }
                
                loginCallback = null;
            }
            else if (FB.IsLoggedIn)
            {
                GetData();
            }
        }

        private void GetData()
        {
            string friendsFormat = ".fields(first_name,last_name)";

            FB.API("/me?fields=first_name,last_name,email,friends" + friendsFormat, HttpMethod.GET, SetMyData);
            LoadProfilePicture(UserId, SetMyPicture);
        }

        private void SetMyData(IResult result)
		{
			if(result.Error != null)
			{
				Debug.Log("=========== On SetMyFacebookData ===========");
				Debug.LogError(result.Error);
				Debug.Log(result.RawResult);
				if(loginCallback != null)
				{
					loginCallback(false);
				}
				loginCallback = null;

				return;
			}

			Dictionary<string, object> data = Json.Deserialize(result.RawResult) as Dictionary<string, object>;
			firstName = data["first_name"] as string;
			lastName = data["last_name"] as string;

			// Sometimes, Facebook does not return email address
			// It could be a bug from the user or Facebok
			// https://developers.facebook.com/bugs/340561996079829
			if(data.ContainsKey("email"))
			{
				email = data["email"] as string;
			}

			friendList.Clear();

			// If the users has no friends playing this game, friend data may come in an empty array or totally missing
			// To be safe, check it before using it
			if(data.ContainsKey("friends"))
			{
				Dictionary<string, object> friendDic = data["friends"] as Dictionary<string, object>;

				foreach(object obj in friendDic["data"] as List<object>)
				{
					Dictionary<string, object> friendData = obj as Dictionary<string, object>;
					UserInfo friend = new UserInfo();

					friend.id = friendData["id"] as string;
					friend.firstName = friendData["first_name"] as string;
					friend.lastName = friendData["last_name"] as string;
					LoadProfilePicture(friend.id, friend.SetPicture);
					friendList.Add(friend);

				}
			}
			Social.SubmitUser();
			Social.SubmitScore(0);

			if(limitTopFriend == -1 && limitTopWorld == -1)
			{
				if(loginCallback != null)
				{
					loginCallback(true);
				}
				loginCallback = null;
				isLoading = false;
			}
			else
			{
				Social.GetTopWorld(limitTopWorld);
			}

		}

		public override void LoadProfilePicture(string userId, Social.SocialPictureCallback callback)
		{

			FB.API(userId + "/picture?height=100&width=100&redirect=false", HttpMethod.GET, result =>
			       {
				if (result.Error != null)
				{
					Debug.LogError(result.Error);
					return;
				}


				var imageUrl = GetPictureUrl(result.RawResult);
				StartCoroutine(GetProfilePicture(imageUrl, callback));
			});
		}

		private string GetPictureUrl(string response)
		{
			object pictureObj = Json.Deserialize(response);
			var _picture = (Dictionary<string, object>)(((Dictionary<string, object>)pictureObj)["data"]);
			object urlH = null;
			if (_picture.TryGetValue("url", out urlH))
			{
				return (string)urlH;
			}

			return null;
		}

		IEnumerator GetProfilePicture(string url, Social.SocialPictureCallback callback)
		{
			WWW www = new WWW(url);
			yield return www;
			callback(www.texture);
		}

		private void SetMyPicture(Texture2D texture)
		{
			if(texture != null)
			{
				picture = texture;
			}
			else
			{
				LoadProfilePicture(UserId, SetMyPicture);
			}
		}

		/// <summary>
		/// Post recommend on facebook user wall.
		/// </summary>
		/// <param name="callback">Callback.</param>
		public override void PostRecommend(Social.SocialSuccessCallback callback)
		{
			postScoreCallback = callback;
			Post("recommend", PostScoreCallBack, 0);
		}

		/// <summary>
		/// ost score according to the data stored in ShareConfig
		/// Note: The text [score] will be replaced with the variable given
		/// </summary>
		/// <param name="score">Score.</param>
		/// <param name="callback">Callback.</param>
		public override void PostScore(int score, Social.SocialSuccessCallback callback)
		{
			postScoreCallback = callback;
			Post("score", PostScoreCallBack, score);
		}

		private void PostScoreCallBack(IShareResult result)
		{
			if(result.Error != null)
			{
				Debug.Log("PostScoreCallBack Err = " + result.Error );
			}

			if(postScoreCallback != null)
			{
				if(result.Error != null || result.RawResult.Contains("cancelled"))
				{
					postScoreCallback(false);
				}
				else
				{
					postScoreCallback(true);
				}
			}
		}

		/// <summary>
		///Post achievement unlock message according to the data stored in ShareConfig
		/// Note: ProEngine helps preventing duplicate post by checking with PlayerProfile first.
		/// </summary>
		/// <param name="id">Identifier.</param>
		/// <param name="callback">Callback.</param>
		public override void PostAchivement(string id, Social.SocialSuccessCallback callback)
		{
			postAchievementCallback = callback;
			Post(id, PostAchivementCallBack, 0);
		}

		private void PostAchivementCallBack(IShareResult result)
		{
			if(postAchievementCallback != null)
			{
				if(result.Error != null || result.RawResult.Contains("cancelled"))
				{
					postAchievementCallback(false);
				}
				else
				{
					postAchievementCallback(true);
				}
			}
		}

		/// <summary>
		/// Trigger Facebook invite dialog
		/// </summary>
		public override void Invite(Social.SocialSuccessCallback callback = null)
		{
			inviteCallback = callback;
			FB.AppRequest(
				message: (PlayBento.Local.GetConfig(typeof(SocialConfig)) as SocialConfig).InviteMessage,
				callback: InviteCallback
				);
		}

		private void InviteCallback(IResult result)
		{
			if(result != null)
			{
				var responseObject = Json.Deserialize(result.RawResult) as Dictionary<string, object>;
				object obj = 0;
				if(responseObject.TryGetValue("cancelled", out obj))
				{
					if(inviteCallback != null)
					{
						inviteCallback(false);
					}

					inviteCallback = null;
				}
				else if(responseObject.TryGetValue("error_message", out obj))
				{
					if(inviteCallback != null)
					{
						inviteCallback(false);
					}

					inviteCallback = null;
				}
				else if(responseObject.TryGetValue("request", out obj))
				{
					if(inviteCallback != null)
					{
						Debug.Log(obj.ToString());
						inviteCallback(true);
					}

					inviteCallback = null;
				}
			}
		}

		private void Post(string id, FacebookDelegate<IShareResult> callback, int score)
		{
			SocialObject shareObject = GetShareObject(id);
			SocialObject defaultObject = GetShareObject("default");
			if(shareObject == null){return;}
			string description = ( string.IsNullOrEmpty(shareObject.Description) ) ? defaultObject.Description: shareObject.Description;
			string link = ( string.IsNullOrEmpty(shareObject.Link) ) ? defaultObject.Link: shareObject.Link;
			string name = ( string.IsNullOrEmpty(shareObject.Name) ) ? defaultObject.Name: shareObject.Name;
			string picture = ( string.IsNullOrEmpty(shareObject.Picture) ) ? defaultObject.Picture: shareObject.Picture;
			string caption = ( string.IsNullOrEmpty(shareObject.Caption) ) ? defaultObject.Caption: shareObject.Caption;
			description = description.Replace("[score]", score + "");
			Debug.Log("description = " + description);
			Debug.Log("link = " + link);
			Debug.Log("name = " + name);
			Debug.Log("picture = " + picture);
			Debug.Log("caption = " + caption);
			//it will be have Debug Error when play on Editor
			//but they told don't worry if it's ok on device.
			FB.FeedShare(
				toId: "",
				link: new Uri(link),
				linkName: name,
				linkCaption: caption,
				linkDescription: description,
				picture: new Uri(picture),
				mediaSource: "",
				callback: callback
			);
		}

		private SocialObject GetShareObject(string id)
		{
			if(socialConfig == null)
			{
				socialConfig = Local.GetConfig(typeof(SocialConfig)) as SocialConfig;
			}
			foreach(SocialObject shareObj in socialConfig.Objects)
			{
				if(shareObj.Id == id)
				{
					return shareObj;
				}
			}
			return null;
		}

		private List<string> GetReadScope()
		{
			List<string> scopeList = new List<string>();
			scopeList.Add("public_profile");
			scopeList.Add("user_friends");
			scopeList.Add("email");

			return scopeList;
		}
	}
}
