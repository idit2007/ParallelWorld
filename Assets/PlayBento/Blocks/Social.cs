using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace PlayBento
{
	public class Social : MonoBehaviour
	{

		public delegate void SocialCallback();

		public delegate void SocialSuccessCallback(bool succeeded);

		public delegate void SocialDataCallback(bool succeeded,string receiverId,string objectId,string message);

		public delegate void SocialPictureCallback(Texture2D texture);

		private static SocialCallback topFriendCallback = null;
		private static SocialCallback topWorldCallback = null;
		private static Cloud.CloudRequestCallback sendGiftCallback = null;

		public enum SocialNetwork
		{
			Facebook,
            Parse,
			Private
		}
		private static SocialNetwork _currentSocialNetwork = SocialNetwork.Facebook;
		private static Dictionary<SocialNetwork, ISocialInterface> networkControllerDic = new Dictionary<SocialNetwork, ISocialInterface> ();
		private static ISocialInterface _currentNetworkController = null;

		protected static ISocialInterface CurrentNetworkController {
			get {
					if (networkControllerDic.Count == 0) {
							SetupSocialNetwork();
					}
					return networkControllerDic [_currentSocialNetwork];
			}
		}
		/// <summary>
		/// Is Social block initialized?
		/// </summary>
		/// <value><c>true</c> if is initialized; otherwise, <c>false</c>.</value>
		public static bool IsInit {
			get{ return CurrentNetworkController.IsInit; }
			set{ CurrentNetworkController.IsInit = value; }
		}

		/// <summary>
		/// Is the player logged in?
		/// </summary>
		/// <value><c>true</c> if is logged in; otherwise, <c>false</c>.</value>
		public static bool IsLoggedIn {
			get{ return CurrentNetworkController.IsLoggedIn;}
		}
		/// <summary>
		/// Get current using network
		/// </summary>
		/// <value>Current network</value>
		public static SocialNetwork Network {
			get { return _currentSocialNetwork; }
		}
		/// <summary>
		/// Get user ID
		/// </summary>
		/// <value>The ID</value>
		public static string UserID {
			get { return CurrentNetworkController.UserId; }
		}
		/// <summary>
		/// Get user first name
		/// </summary>
		/// <value>The first name</value>
		public static string FirstName {
			get { return CurrentNetworkController.FirstName; }
		}
		/// <summary>
		/// Get user last name
		/// </summary>
		/// <value>The last name</value>
		public static string LastName {
			get { return CurrentNetworkController.LastName; }
		}
		/// <summary>
		/// Get user email
		/// </summary>
		/// <value>The email</value>
		public static string Email {
			get { return CurrentNetworkController.Email; }
		}
		/// <summary>
		/// Gets user profile picture
		/// </summary>
		/// <value>The picture</value>
		public static Texture2D Picture{
			get{ return CurrentNetworkController.Picture; }
		}
		/// <summary>
		/// Get user friend list
		/// </summary>
		/// <value>The friend list</value>
		public static List<UserInfo> FriendList {
			get { return CurrentNetworkController.FriendList; }
		}

		/// <summary>
		/// Is the network loading something?
		/// </summary>
		/// <value><c>true</c> if is loading; otherwise, <c>false</c>.</value>
		public static bool isLoading {
			get { return CurrentNetworkController.IsLoading; }
		}

		private static int _score = 0;
		/// <summary>
		/// Get user score
		/// </summary>
		/// <value>The score</value>
		public static int Score {
			get { return _score; }
		}

		private static List<UserInfo> _topFriends = new List<UserInfo> ();
		/// <summary>
		/// Get the list of the best players, friended with the user, ordered by score
		/// </summary>
		/// <value>The list</value>
		public static List<UserInfo> TopFriends {
			get { return _topFriends; }
		}

		private static List<UserInfo> _topWorlds = new List<UserInfo> ();
		/// <summary>
		/// Get the list of the best players ordered by score
		/// </summary>
		/// <value>The list</value>
		public static List<UserInfo> TopWorlds {
			get { return _topWorlds; }
		}

		private static List<RequestInfo> _gifts = new List<RequestInfo> ();
		/// <summary>
		/// Get the list of pending gifts for this user
		/// </summary>
		/// <value>The list</value>
		public static List<RequestInfo> Gifts {
			get { return _gifts; }
		}
		/// <summary>
		/// Select the network to connect with
		/// </summary>
		/// <param name="socialNetwork">Social network</param>
		public static void SelectNetwork(SocialNetwork socialNetwork)
		{
			if (socialNetwork != _currentSocialNetwork) {
					_currentSocialNetwork = socialNetwork;
					SetupSocialNetwork();
			}
		}

		public static void Init()
		{
			CurrentNetworkController.Init ();
		}

		/// <summary>
		/// Login to the selected social network
		/// </summary>
		/// <param name="callback">Callback</param>
		public static void Login(SocialSuccessCallback callback)
		{
			CurrentNetworkController.Login(callback);
		}
        /// <summary>
		/// Login to the selected social network
		/// </summary>
		/// <param name="callback">Callback</param>
        /// <param name="username">Username</param>
        /// <param name="password">Password</param>
		public static void Login(SocialSuccessCallback callback, string username, string password)
		{
			CurrentNetworkController.Login(callback, username, password);
		}
		/// <summary>
		/// Login to the selected social network and get all data
		/// </summary>
		/// <param name="limitTopWorld">Limit top world.</param>
		/// <param name="limitTopFriend">Limit top friend.</param>
		/// <param name="callBack">Call back.</param>
		public static void LoginWithCompleteData(int limitTopWorld, int limitTopFriend, SocialSuccessCallback callBack)
		{
			CurrentNetworkController.LoginWithCompleteData(limitTopWorld, limitTopFriend, callBack);
		}

		/// <summary>
		/// Logout from the selected social network
		/// </summary>
		public static void Logout()
		{
			if (IsLoggedIn)
				CurrentNetworkController.LogOut();
		}
		/// <summary>
		/// Send invitation to friends in the selected social network
		/// </summary>
		/// <param name="callback">Callback</param>
		public static void Invite(SocialSuccessCallback callback = null)
		{
			CurrentNetworkController.Invite (callback);
		}
		/// <summary>
		/// Post recommend referring the data in SocialConfig
		/// </summary>
		/// <param name="callback">Callback</param>
		public static void PostRecommend(SocialSuccessCallback callback)
		{
			CurrentNetworkController.PostRecommend(callback);
		}
		/// <summary>
		/// Post achievement referring the data in SocialConfig
		/// </summary>
		/// <param name="id">Achievement ID</param>
		/// <param name="callback">Callback</param>
		public static void PostAchivement(string id, SocialSuccessCallback callback)
		{
			CurrentNetworkController.PostAchivement(id, callback);
		}
		/// <summary>
		/// Post score referring the data in SocialConfig
		/// </summary>
		/// <param name="score">Score</param>
		/// <param name="callback">Callback</param>
		public static void PostScore(int score, SocialSuccessCallback callback)
		{
			CurrentNetworkController.PostScore(score, callback);
		}
		/// <summary>
		/// Submit user to the Cloud
		/// </summary>
		public static void SubmitUser()
		{
			Cloud.SubmitUser(UserID, Network, FirstName, LastName, "", Email, "", "", "");
		}

		/// <summary>
		/// Submit user score to the Cloud
		/// </summary>
		/// <param name="score">Score</param>
		public static void SubmitScore(int score)
		{
			_score = score;
			Cloud.SubmitScore(UserID, Network, FirstName, LastName, score);
		}
		/// <summary>
		/// Get the list of gifts this user has received
		/// </summary>
		/// <param name="callback">Callback</param>
		public static void GetGift(SocialCallback callback)
		{
			Cloud.GetGift(_gifts, callback);
		}
		/// <summary>
		/// Send gift to friend
		/// </summary>
		/// <param name="receiverId">Receiver ID</param>
		/// <param name="payload">Payload. Any additional info such as item type, amount, etc.</param>
		/// <param name="callback">Callback</param>
		public static void SendGift(string receiverId, string payload, Cloud.CloudRequestCallback callback)
		{
			sendGiftCallback = callback;
			Cloud.SendGift (receiverId, Social.Network, payload, sendGiftCallback);
		}
		/// <summary>
		/// Accept the gift with the id specified
		/// </summary>
		/// <param name="requestId">Request ID</param>
		/// <param name="callback">Callback</param>
		public static void AcceptGift(string requestId, SocialSuccessCallback callback)
		{
			Cloud.AcceptGift(requestId, callback);
		}

		/// <summary>
		/// Ignore the gift with the id specified
		/// </summary>
		/// <param name="requestId">Request ID</param>
		/// <param name="callback">Callback</param>
		public static void IgnoreGift(string requestId, SocialSuccessCallback callback)
		{
			Cloud.IgnoreGift(requestId, callback);
		}

		/// <summary>
		/// Get the top players who play this game ordered by score
		/// </summary>
		/// <param name="limit">Maximum number of players to get</param>
		/// <param name="callback">Callback</param>
		public static void GetTopWorld(int limit, SocialCallback callback = null)
		{
			topWorldCallback = callback;
			Cloud.GetTopWorld (Network, limit, TopWorldHandler);
		}

		/// <summary>
		/// Get the top friends who play this game ordered by score
		/// </summary>
		/// <param name="limit">Maximum number of players to get</param>
		/// <param name="callback">Callback</param>
		public static void GetTopFriend(int limit, SocialCallback callback = null)
		{
			topFriendCallback = callback;
			Cloud.GetTopFriends(Network, limit, TopFriendHandler);
		}

		/// <summary>
		/// Load profile picture
		/// </summary>
		/// <param name="userId">User ID</param>
		/// <param name="callback">Callback</param>
		public static void LoadProfilePicture(string userId, SocialPictureCallback callback)
		{
			CurrentNetworkController.LoadProfilePicture(userId, callback);
		}

		private static void TopWorldHandler(List<UserInfo> userInfoList)
		{
			_topWorlds = userInfoList;
			if (topWorldCallback != null) 
			{
				topWorldCallback ();
				topWorldCallback = null;
			}

			if (CurrentNetworkController.LimitTopWorld > -1 && CurrentNetworkController.LimitTopFriend > -1) 
			{
				GetTopFriend(CurrentNetworkController.LimitTopFriend);
			}
		}

		private static void TopFriendHandler(List<UserInfo> userInfoList)
		{
			_topFriends = userInfoList;
			if (topFriendCallback != null) {

				topFriendCallback ();
				topFriendCallback = null;
			}

			if (CurrentNetworkController.LimitTopWorld > -1 && CurrentNetworkController.LimitTopFriend > -1) {

				if (CurrentNetworkController.LoginCallback != null) {
					CurrentNetworkController.LoginCallback (true);
				}

				CurrentNetworkController.LoginCallback = null;
				CurrentNetworkController.LimitTopWorld = -1;
				CurrentNetworkController.LimitTopFriend = -1;
			}
		}

		private static void SetupSocialNetwork()
		{
			if (networkControllerDic.Count > 0) {
				Logout ();
			} else {
				networkControllerDic.Add(SocialNetwork.Facebook, GameObject.Find("PlayBentoObject").AddComponent<FacebookSocial>());
                networkControllerDic.Add(SocialNetwork.Parse, GameObject.Find("PlayBentoObject").AddComponent<ParseSocial>());
				networkControllerDic.Add(SocialNetwork.Private, GameObject.Find("PlayBentoObject").AddComponent<PrivateSocial>());
			}
		}
	}

	public class UserInfo
	{
		public string id;
		public string firstName;
		public string lastName;
		public Texture2D picture;
		public int score;

		public void SetPicture(Texture2D texture)
		{
			picture = texture;
		}
	}

	public class RequestInfo
	{
		public string requestId;
		public string senderId;
		public string payload;
		public string firstName;
		public string lastName;
		public Texture2D picture;
		
		public void SetPicture(Texture2D texture)
		{
			picture = texture;
		}
	}
}