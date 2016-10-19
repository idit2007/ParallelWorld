using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace PlayBento
{
	public class ParseSocial : ISocialInterface {

		public override string UserId
		{
			get
			{
				return userId;
			}
		}

		public override void Init ()
		{
			isInit = true;
		}

		public override void Login(Social.SocialSuccessCallback callback, string username, string password)
		{
			if(Social.isLoading)
			{
				Debug.LogWarning("Is Loading!");
				return;
			}
			StartCoroutine(GetUserInfo(callback, username, password));
		}
        
        IEnumerator GetUserInfo(Social.SocialSuccessCallback callback, string username, string password)
		{
            string serverURL = (Local.GetConfig(typeof(CloudConfig)) as CloudConfig).Server;
            string url = serverURL + "/login?username=" + WWW.EscapeURL(username) + "&password=" + WWW.EscapeURL(password);
            string appId = (Local.GetConfig(typeof(CloudConfig)) as CloudConfig).ApplicationID;
 
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers["X-Parse-Application-Id"] = appId;

            WWW www = new WWW(url, null, headers);
			yield return www;
            
            Dictionary<string, object> user = Facebook.MiniJSON.Json.Deserialize(www.text) as Dictionary<string, object>;
            if(!string.IsNullOrEmpty(user["objectId"] as string))
            {
                userId = user["objectId"] as string;
                Debug.Log ("Login: " + userId);
                Social.SubmitScore (0);
                Social.SubmitUser ();
                isLoggedIn = true;
                if(callback != null)
                {
                    callback(true);
                }
            }
            else
            {
                callback(false);
            }
        }
	}
}
