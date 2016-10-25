using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Facebook.MiniJSON;

namespace PlayBento
{
	public class PrivateSocial : ISocialInterface {

		public override string UserId
		{
			get
			{
				SetUserId ();
				return userId;
			}
		}

		public override void Init ()
		{
			isInit = true;
		}

		public override void Login(Social.SocialSuccessCallback callBack)
		{
			if(Social.isLoading)
			{
				Debug.LogWarning("Is Loading!");
				return;
			}
			SetUserId ();
			Debug.Log ("Login: " + userId);
			Social.SubmitScore (0);
			Social.SubmitUser ();
			isInit = true;
			isLoggedIn = true;
			LoadProfilePicture(UserId, SetMyPicture);
			if(callBack != null)
			{
				callBack(true);
			}
			
		}

		void SetUserId()
		{
			if(string.IsNullOrEmpty(userId))
			{
				if(PlayerPrefs.HasKey("GUID"))
				{
					userId = PlayerPrefs.GetString("GUID");
				}
				else
				{
					userId = System.Guid.NewGuid().ToString();
					PlayerPrefs.SetString("GUID", userId);
				}
			}
		}

		public override void LoadProfilePicture(string userId, Social.SocialPictureCallback callback)
		{
			
			var imageUrl = "https://app.progaming.co.th/playbento/images/no-pic.png";
			StartCoroutine(GetProfilePicture(imageUrl, callback));
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
	}
}
