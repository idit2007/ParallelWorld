using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PlayBento;

public class TasteUI : MonoBehaviour {

	enum TastePage
	{
		Main,
		MainSocial,
		PostSocial,
		GiftList,
		MainCloud,
		MainLocal,
		PlayerProfile,
		IAP,
		Ads
	};

	private TastePage currentPage = TastePage.Main;
	private TastePage CurrentPage {
		get {
			return currentPage;
		}
		set {
			if(currentPage != value)
				lastPage = currentPage;

			currentPage = value;
		}
	}
	private TastePage lastPage = TastePage.Main;

	private string _headerShow = "";
	private string _buttonActiveName = "";
	private string _detail = "";
	private bool _isShowDetail;

	// Use this for initialization
	void Start () {
		PB.Init();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void SetShowDetail(string detail, float delay)
	{
		_detail = _buttonActiveName + " : " + detail;
		_isShowDetail = true;
		if(IsInvoking("StopShowDetail"))
		{
			CancelInvoke("StopShowDetail");
		}
		if(delay > 0)
		{
			Invoke("StopShowDetail", delay);
		}
	}

	void StopShowDetail()
	{
		_isShowDetail = false;
	}

	void OnGUI()
	{
		// Puts some basic buttons onto the screen.
		GUI.skin.button.fontSize = (int)(0.05f * Screen.height);
		GUI.skin.label.alignment = TextAnchor.MiddleCenter;
		GUI.skin.label.fontSize = 30;

		Rect rectHeader = new Rect (0.1f * Screen.width, 0.01f * Screen.height,
		                            0.8f * Screen.width, 0.15f * Screen.height);
		GUI.Label(rectHeader, _headerShow);

		GUI.skin.label.fontSize = 18;
		Rect rectDetail = new Rect (0.1f * Screen.width, 0.1f * Screen.height,
		                            0.8f * Screen.width, 0.15f * Screen.height);
		if(_isShowDetail)
			GUI.Label(rectDetail, _detail);

		switch(currentPage)
		{
		case TastePage.Main:
			MainPage();
			break;
		case TastePage.MainSocial:
			MainSocialPage();
			break;
		case TastePage.PostSocial:
			PostSocialPage();
			break;
		case TastePage.GiftList:
			GiftListSocialPage();
			break;
		case TastePage.MainLocal:
		case TastePage.MainCloud:
			LocalAndCloudPage();
			break;
		case TastePage.PlayerProfile:
			PlayerProfilePage();
			break;
		case TastePage.IAP:
			IAPPage();
			break;
		case TastePage.Ads:
			AdsPage();
			break;
		}

		if(currentPage != TastePage.Main)
		{
			Rect rectLeftDown = new Rect (0.01f * Screen.width, 0.93f * Screen.height,
			                              0.15f * Screen.width, 0.05f * Screen.height);
			GUI.skin.button.fontSize = 14;
			if(GUI.Button(rectLeftDown, "Close"))
			{
				if(_headerShow.Split(':').Length == 2)
				{
					CurrentPage = TastePage.Main;
				}
				else
					CurrentPage = lastPage;
			}
		}
	}
	
	private void MainPage()
	{
		_headerShow = "PlayBento";

		Rect rect1 = new Rect (0.1f * Screen.width, ((0.1875f * 1f) + 0.05f) * Screen.height,
		                      0.8f * Screen.width, 0.1f * Screen.height);
		if(GUI.Button(rect1, "Social   >>"))
		{
			CurrentPage = TastePage.MainSocial;
		}

		Rect rect2 = new Rect (0.1f * Screen.width, ((0.1875f * 1.75f) + 0.05f) * Screen.height,
		                       0.8f * Screen.width, 0.1f * Screen.height);
		if(GUI.Button(rect2, "Local   >>"))
		{
			CurrentPage = TastePage.MainLocal;
		}

		Rect rect3 = new Rect (0.1f * Screen.width, ((0.1875f * 2.5f) + 0.05f) * Screen.height,
		                       0.8f * Screen.width, 0.1f * Screen.height);
		if(GUI.Button(rect3, "Cloud   >>"))
		{
			CurrentPage = TastePage.MainCloud;
		}

		Rect rect4 = new Rect (0.1f * Screen.width, ((0.1875f * 3.25f) + 0.05f) * Screen.height,
		                       0.8f * Screen.width, 0.1f * Screen.height);
		if(GUI.Button(rect4, "IAP   >>"))
		{
			CurrentPage = TastePage.IAP;
		}

		Rect rect5 = new Rect (0.1f * Screen.width, ((0.1875f * 4f) + 0.05f) * Screen.height,
		                       0.8f * Screen.width, 0.1f * Screen.height);
		if(GUI.Button(rect5, "Ads   >>"))
		{
			CurrentPage = TastePage.Ads;
		}
	}

	private void MainSocialPage()
	{
		_headerShow = "Block : Social";
		Rect rect1 = new Rect (0.1f * Screen.width, ((0.1875f * 1f) + 0.05f) * Screen.height,
		                       0.8f * Screen.width, 0.1f * Screen.height);
		if(GUI.Button(rect1, "Login Facebook"))
		{
			if(!PlayBento.Social.IsLoggedIn)
			{
				_buttonActiveName = "Login Facebook";
				SetShowDetail("Loading", 0);
				PlayBento.Social.Login(LoginHandler);
			}
			else
			{
				_buttonActiveName = "";
				SetShowDetail("You're are already logged in", 3.0f);
			}
		}
		
		Rect rect2 = new Rect (0.1f * Screen.width, ((0.1875f * 1.75f) + 0.05f) * Screen.height,
		                       0.8f * Screen.width, 0.1f * Screen.height);
		if(GUI.Button(rect2, "Post Facebook >>"))
		{
			if(PlayBento.Social.IsLoggedIn)
			{
				CurrentPage = TastePage.PostSocial;
			}
			else
			{
				_buttonActiveName = "";
				SetShowDetail("Please Login First!", 3.0f);
			}
		}
		
		Rect rect3 = new Rect (0.1f * Screen.width, ((0.1875f * 2.5f) + 0.05f) * Screen.height,
		                       0.8f * Screen.width, 0.1f * Screen.height);
		if(GUI.Button(rect3, "Invite Facebook"))
		{
			if(PlayBento.Social.IsLoggedIn)
			{
				_buttonActiveName = "Invite Facebook";
				SetShowDetail("Loading", 0);
				PlayBento.Social.Invite(SuccessHandle);
			}
			else
			{
				_buttonActiveName = "";
				SetShowDetail("Please Login First!", 3.0f);
			}
		}
		
		Rect rect4 = new Rect (0.1f * Screen.width, ((0.1875f * 3.25f) + 0.05f) * Screen.height,
		                       0.8f * Screen.width, 0.1f * Screen.height);
		if(GUI.Button(rect4, "Send Gift To Self"))
		{
			if(PlayBento.Social.IsLoggedIn)
			{
				_buttonActiveName = "Send Gift To Self";
				SetShowDetail("Loading", 0);
				PlayBento.Social.SendGift(PlayBento.Social.UserID, "testobject", SendGiftHandle);
			}
			else
			{
				_buttonActiveName = "";
				SetShowDetail("Please Login First!", 3.0f);
			}
		}
		
		Rect rect5 = new Rect (0.1f * Screen.width, ((0.1875f * 4f) + 0.05f) * Screen.height,
		                       0.8f * Screen.width, 0.1f * Screen.height);
		if(GUI.Button(rect5, "List Gift   >>"))
		{
			if(PlayBento.Social.IsLoggedIn)
			{
				SetupGetGift();
			}
			else
			{
				_buttonActiveName = "";
				SetShowDetail("Please Login First!", 3.0f);
			}
		}
	}

	private void PostSocialPage()
	{
		_headerShow = "Block : Social : Post Facebook";
		Rect rect1 = new Rect (0.1f * Screen.width, ((0.1875f * 1f) + 0.05f) * Screen.height,
		                       0.8f * Screen.width, 0.1f * Screen.height);
		if(GUI.Button(rect1, "Post Recommended"))
		{
			_buttonActiveName = "Post Recommended";
			SetShowDetail("Loading", 0);
			PlayBento.Social.PostRecommend(SuccessHandle);
		}
		
		Rect rect2 = new Rect (0.1f * Screen.width, ((0.1875f * 1.75f) + 0.05f) * Screen.height,
		                       0.8f * Screen.width, 0.1f * Screen.height);
		if(GUI.Button(rect2, "Post 50 Score"))
		{
			_buttonActiveName = "Post 50 Score";
			SetShowDetail("Loading", 0);
			PlayBento.Social.PostScore(50, SuccessHandle);
		}
		
		Rect rect3 = new Rect (0.1f * Screen.width, ((0.1875f * 2.5f) + 0.05f) * Screen.height,
		                       0.8f * Screen.width, 0.1f * Screen.height);
		if(GUI.Button(rect3, "Post Achivement"))
		{
			_buttonActiveName = "Post Achivement";
			SetShowDetail("Loading", 0);
			PlayBento.Social.PostAchivement((Local.GetConfig(typeof(SocialConfig)) as SocialConfig).Objects[3].Id, SuccessHandle);
		}
	}

	private void GiftListSocialPage()
	{
		_headerShow = "Block : Social : List Gift";
		if(PlayBento.Social.Gifts.Count > 0)
		{
			for(int i = 0; i < PlayBento.Social.Gifts.Count; i++)
			{
				Rect rect =	new Rect (0.1f * Screen.width, ((0.1875f * (1f +(i* 0.75f))) + 0.05f) * Screen.height,
				                      0.8f * Screen.width, 0.1f * Screen.height);

				Rect rectButton = new Rect (0.4f * Screen.width, ((0.1875f * (1f +(i* 0.75f))) + 0.05f) * Screen.height,
				                            0.2f * Screen.width, 0.1f * Screen.height);
				Rect rectButton2 = new Rect (0.7f * Screen.width, ((0.1875f * (1f +(i* 0.75f))) + 0.05f) * Screen.height,
				                            0.2f * Screen.width, 0.1f * Screen.height);

				GUI.skin.box.alignment = TextAnchor.MiddleLeft;
				GUI.Box(rect, PlayBento.Social.Gifts[i].senderId);

				if(GUI.Button(rectButton, "A"))
				{
					_buttonActiveName = "Accept " + PlayBento.Social.Gifts[i].senderId;
					SetShowDetail("Loading", 0);
					Cloud.AcceptGift(PlayBento.Social.Gifts[i].requestId, SuccessHandle);
				}
				if(GUI.Button(rectButton2, "I"))
				{
					_buttonActiveName = "Ignore " + PlayBento.Social.Gifts[i].senderId;
					SetShowDetail("Loading", 0);
					Cloud.IgnoreGift(PlayBento.Social.Gifts[i].requestId, SuccessHandle);
				}
			}
		}
	}

	private void LocalAndCloudPage()
	{
		if(currentPage == TastePage.MainLocal)
		{
			_headerShow = "Block : Local";
		}
		else
		{
			_headerShow = "Block : Cloud";
		}

		Rect rect1 = new Rect (0.1f * Screen.width, ((0.1875f * 1f) + 0.05f) * Screen.height,
		                       0.8f * Screen.width, 0.1f * Screen.height);
		if(GUI.Button(rect1, "Show Player Profile >>"))
		{
			_buttonActiveName = "Show Player Profile >>";
			SetShowDetail("Loading", 0);
			CurrentPage = TastePage.PlayerProfile;
		}
		
		Rect rect2 = new Rect (0.1f * Screen.width, ((0.1875f * 1.75f) + 0.05f) * Screen.height,
		                       0.8f * Screen.width, 0.1f * Screen.height);
		if(GUI.Button(rect2, (currentPage == TastePage.MainLocal? "Save To Local":"Save To Cloud")))
		{
			if(currentPage == TastePage.MainLocal)
			{
				_buttonActiveName = "Save To Local";
				SetShowDetail("Succeeded", 3.0f);
				Local.SaveProfile();
			}
			else
			{
				if(PlayBento.Social.IsLoggedIn)
				{
					_buttonActiveName = "Save To Cloud";
					SetShowDetail("Loading", 0);
					Cloud.SaveProfile(SuccessHandle);
				}
				else
				{
					_buttonActiveName = "";
					SetShowDetail("Please Login Facebook First!", 3);
				}
			}
		}
		
		Rect rect3 = new Rect (0.1f * Screen.width, ((0.1875f * 2.5f) + 0.05f) * Screen.height,
		                       0.8f * Screen.width, 0.1f * Screen.height);
		if(GUI.Button(rect3, (currentPage == TastePage.MainLocal? "Load From Local":"Load From Cloud")))
		{
			if(currentPage == TastePage.MainLocal)
			{
				_buttonActiveName = "Load From Local";
				SetShowDetail("Succeeded", 3.0f);
				Local.LoadProfile();
			}
			else
			{
				if(PlayBento.Social.IsLoggedIn)
				{
					_buttonActiveName = "Load From Cloud";
					SetShowDetail("Loading", 0);
					Cloud.LoadProfile(SuccessHandle);
				}
				else
				{
					_buttonActiveName = "";
					SetShowDetail("Please Login Facebook First!", 3);
				}
			}
		}

		if(currentPage == TastePage.MainLocal)
		{
			Rect rect4 = new Rect (0.1f * Screen.width, ((0.1875f * 3.25f) + 0.05f) * Screen.height,
			                       0.8f * Screen.width, 0.1f * Screen.height);
			if(GUI.Button(rect4, "Add Text To Player Profile"))
			{

				{
					PlayerProfile p = Local.GetProfile(typeof(PlayerProfile)) as PlayerProfile;
					p.Name += "[m]";
				}
			}
		}
	}

	private void PlayerProfilePage()
	{
		if(lastPage == TastePage.MainLocal)
		{
			_headerShow = "Block : Local : PlayerProfile";
		}
		else
		{
			_headerShow = "Block : Cloud : PlayerProfile";
		}
		Rect rectShow = new Rect (0.1f * Screen.width, ((0.1875f * 1f) + 0.05f) * Screen.height,
		                       0.8f * Screen.width, 0.6f * Screen.height);
		GUI.Box(rectShow, (Local.GetProfile(typeof(PlayerProfile)) as PlayerProfile).GetXML());
	}
	
	private void AdsPage()
	{
		_headerShow = "Block : Ad";
		Rect rect1 = new Rect (0.1f * Screen.width, ((0.1875f * 1f) + 0.05f) * Screen.height,
		                       0.8f * Screen.width, 0.1f * Screen.height);
		if(GUI.Button(rect1, "Show"))
		{
			Debug.Log ("Any ad ready? " + Ad.Ready(Ad.Format.VIDEO));
			Ad.Show(Ad.Format.VIDEO);
		}
		Rect rect2 = new Rect (0.1f * Screen.width, ((0.1875f * 1.75f) + 0.05f) * Screen.height,
		                       0.8f * Screen.width, 0.1f * Screen.height);
		if(GUI.Button(rect2, "Show Admob"))
		{
			Ad.Show(Ad.Format.INTERSTITIAL, Ad.Network.ADMOB);
		}
		Rect rect3 = new Rect (0.1f * Screen.width, ((0.1875f * 2.5f) + 0.05f) * Screen.height,
		                       0.8f * Screen.width, 0.1f * Screen.height);
		if(GUI.Button(rect3, "Show Chartboost"))
		{
			Ad.Show(Ad.Format.VIDEO, Ad.Network.CHARTBOOST);
		}
		Rect rect4 = new Rect (0.1f * Screen.width, ((0.1875f * 3.25f) + 0.05f) * Screen.height,
		                       0.8f * Screen.width, 0.1f * Screen.height);
		if(GUI.Button(rect4, "Show Unity"))
		{
			Ad.Show(Ad.Format.VIDEO, Ad.Network.UNITY);
		}
		Rect rect5 = new Rect (0.1f * Screen.width, ((0.1875f * 4f) + 0.05f) * Screen.height,
		                       0.8f * Screen.width, 0.1f * Screen.height);
		if(GUI.Button(rect5, "Show AdColony"))
		{
			Ad.Show(Ad.Format.VIDEO, Ad.Network.ADCOLONY);
		}
	}

	private void IAPPage()
	{
		_headerShow = "Block : IAP";
		Rect rect1 = new Rect (0.1f * Screen.width, ((0.1875f * 1f) + 0.05f) * Screen.height,
		                       0.8f * Screen.width, 0.1f * Screen.height);
		if(GUI.Button(rect1, "Query Inventory"))
		{
			IAP.QueryInventory();
		}
		
		Rect rect2 = new Rect (0.1f * Screen.width, ((0.1875f * 1.75f) + 0.05f) * Screen.height,
		                       0.8f * Screen.width, 0.1f * Screen.height);
		if(GUI.Button(rect2, "Purchase Item 1"))
		{
			IAP.Purchase("th.co.progaming.playbento.sample1", 
			             (bool succeeded, string id) => { SetShowDetail("Succeeded = " + succeeded  + " with ID = " + id, 3);}
			);
		}

		Rect rect3 = new Rect (0.1f * Screen.width, ((0.1875f * 2.5f) + 0.05f) * Screen.height,
		                       0.8f * Screen.width, 0.1f * Screen.height);
		if(GUI.Button(rect3, "Purchase Item 2"))
		{
			IAP.Purchase("th.co.progaming.playbento.sample2",
			             (bool succeeded, string id) => { SetShowDetail("Succeeded = " + succeeded  + " with ID = " + id, 3);}
			);
		}
		
		Rect rect4 = new Rect (0.1f * Screen.width, ((0.1875f * 3.25f) + 0.05f) * Screen.height,
		                       0.8f * Screen.width, 0.1f * Screen.height);
		if(GUI.Button(rect4, "Restore Item"))
		{
			IAP.Restore(
				(bool succeeded) => { SetShowDetail("Succeeded = " + succeeded, 3); }
			);
		}
	}

	private void SetupGetGift()
	{
		CurrentPage = TastePage.GiftList;
		PlayBento.Social.GetGift(Handler);
	}

	private void Handler()
	{
		SetShowDetail("Succeeded", 3.0f);
	}

	private void LoginHandler(bool success)
	{
		if(success)
		{
			SetShowDetail("Succeeded", 3.0f);
		}
		else
		{
			SetShowDetail("Failed", 3.0f);
		}
		Debug.Log("LoginHandle = " + success);
	}

	private void SendGiftHandle(bool success, int delay)
	{
		if(success)
		{
			SetShowDetail("Succeeded", 3.0f);
		}
		else
		{
			SetShowDetail("Failed Delay = " + delay + " secs", 3.0f);
		}
		Debug.Log("SendGiftHandle = " + success + " delay = " + delay);
	}

	private void SuccessHandle(bool success)
	{
		if(success)
		{
			SetShowDetail("Succeeded", 3.0f);
		}
		else
		{
			SetShowDetail("falsed", 3.0f);
		}
		Debug.Log("SuccessHandle = " + success);
	}
}
