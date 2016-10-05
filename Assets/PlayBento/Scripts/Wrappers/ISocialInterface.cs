using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace PlayBento
{
	public class ISocialInterface : MonoBehaviour {

		protected bool isLoading = false;
		public bool IsLoading
		{
			get{ return isLoading; }
		}

		protected bool isInit = false;
		public bool IsInit{
			get{ return isInit; }
			set{ isInit = value; }
		}
		protected string userId = "";
		public virtual string UserId{
			get{ return userId; }
		}
		protected bool isLoggedIn = false;
		public virtual bool IsLoggedIn
		{
			get{ return isLoggedIn; }
		}
		protected string firstName = "";
		public string FirstName {
			get {
				return firstName;
			}
			set {
				firstName = value;
			}
		}
		protected string lastName = "";
		public string LastName {
			get {
				return lastName;
			}
			set {
				lastName = value;
			}
		}
		protected string email = "";
		public string Email { get{ return email; } }
		
		protected Texture2D picture = null;
		public Texture2D Picture { get { return picture; } }

		protected Social.SocialSuccessCallback loginCallback = null;
		public Social.SocialSuccessCallback LoginCallback
		{
			get { return loginCallback; }
			set { loginCallback = value; }
		}
		protected List<UserInfo> friendList = new List<UserInfo>();
		public List<UserInfo> FriendList
		{
			get
			{
				return friendList;
			}
		}

		protected int limitTopFriend = -1;
		public int LimitTopFriend { 
			get { return limitTopFriend; } 
			set { limitTopFriend = value; }
		}

		protected int limitTopWorld = -1;
		public int LimitTopWorld { 
			get { return limitTopWorld; } 
			set { limitTopWorld = value; }
		}

		public virtual void Init(){}
		public virtual void Login(Social.SocialSuccessCallback callback){}
        public virtual void Login(Social.SocialSuccessCallback callback, string username, string password){}
		public virtual void LoginWithCompleteData(int _limitTopWorld, int _limitTopFriend, Social.SocialSuccessCallback callBack){}
		public virtual void LogOut(){}
		public virtual void Invite(Social.SocialSuccessCallback callback){}
		public virtual void PostScore(int score, Social.SocialSuccessCallback callback){}
		public virtual void PostRecommend(Social.SocialSuccessCallback callback){}
		public virtual void PostAchivement(string id, Social.SocialSuccessCallback callback){}
		public virtual void LoadProfilePicture(string userId, Social.SocialPictureCallback callback){}
	}
}
