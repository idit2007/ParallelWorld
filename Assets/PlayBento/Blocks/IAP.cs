using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Facebook.MiniJSON;
using Facebook.Unity;
using OnePF;

namespace PlayBento
{
	public class IAP {

		private static IAPConfig config;

		public delegate void IAPPurchaseCallback(bool succeeded, string id);
		public delegate void IAPRestoreCallback (bool succeeded);

		private static IAPPurchaseCallback purchaseCallback;
		private static IAPRestoreCallback restoreCallback;

		/// <summary>
		/// Initialize IAP block. Call this method before calling any other method
		/// </summary>
		public static void Init()
		{
			#if UNITY_ANDROID || UNITY_IOS || UNITY_WP8
			config = Local.GetConfig (typeof(IAPConfig)) as IAPConfig;

			OpenIABEventManager.billingSupportedEvent += OnBillingSupportedEvent;
			OpenIABEventManager.billingNotSupportedEvent += OnBillingNotSupportedEvent;
			OpenIABEventManager.queryInventorySucceededEvent+= OnQueryInventorySucceededEvent;
			OpenIABEventManager.queryInventoryFailedEvent += OnQueryInventoryFailedEvent;
			OpenIABEventManager.purchaseSucceededEvent += OnPurchaseSucceededEvent;
			OpenIABEventManager.purchaseFailedEvent += OnPurchaseFailedEvent;
			OpenIABEventManager.consumePurchaseSucceededEvent += OnConsumeSucceededEvent;
			OpenIABEventManager.consumePurchaseFailedEvent += OnConsumeFailedEvent;
			OpenIABEventManager.restoreSucceededEvent += OnRestoreSucceededEvent;
			OpenIABEventManager.restoreFailedEvent += OnRestoreFailedEvent;

			OnePF.Options options = new OnePF.Options();
			options.checkInventory = false;
			options.prefferedStoreNames = new string[] { OpenIAB_Android.STORE_GOOGLE };
			options.verifyMode = OptionsVerifyMode.VERIFY_SKIP;

			options.storeKeys.Add (OpenIAB_Android.STORE_GOOGLE, config.GoogleLicenseKey);

			foreach (IAPItem item in config.Items)
			{
				OpenIAB.mapSku (item.Id, OpenIAB_Android.STORE_GOOGLE, item.Id);
				OpenIAB.mapSku (item.Id, OpenIAB_iOS.STORE, item.Id);
			}

			OpenIAB.init (options);
			#endif
		}

		/// <summary>
		/// Queries product inventory
		/// </summary>
		public static void QueryInventory()
		{
			#if UNITY_ANDROID || UNITY_IOS || UNITY_WP8
			OpenIAB.queryInventory();
			#else
			Debug.LogError("OpenIAB billing currently not supported on this platform. Sorry.");
			#endif
		}

		/// <summary>
		/// Purchase product with the specified ID
		/// </summary>
		/// <param name="id">Product ID</param>
		/// <param name="callback">Callback</param>
		public static void Purchase(string id, IAPPurchaseCallback callback)
		{
			#if UNITY_ANDROID || UNITY_IOS || UNITY_WP8
			purchaseCallback = callback;
			OpenIAB.purchaseProduct(id);
			#elif UNITY_WEBPLAYER
			Application.ExternalCall("console.log", "Purchasing");
			FB.Canvas.Pay(
				// CS: Facebook Canvas URL must not be hard-coded
				product: "https://app.progaming.co.th/playbento/iap/" + id + ".html",
				quantity: 1,
				callback: delegate(IPayResult response) {
					Application.ExternalCall("console.log", "Purchase response = " + response.RawResult);

					var responseObject = Json.Deserialize(response.RawResult) as Dictionary<string, object>;
			        object obj = 0;
			        if (responseObject.TryGetValue ("signed_request", out obj))
			        {
			        	Application.ExternalCall("console.log", "signed_request key returned");
			        	callback (true, id);
			        }
			        else
			        {
			        	Application.ExternalCall("console.log", "signed_request not found");
			        	callback (false, "signed_request not found");
			        }

			        Application.ExternalCall("console.log", "Purchase completed");
				}
			);
			#else
			Debug.LogError("OpenIAB billing currently not supported on this platform. Sorry.");
			#endif

		}

		/// <summary>
		/// Restore all non-consumable purchased products
		/// </summary>
		public static void Restore(IAPRestoreCallback callback)
		{
			// CS: As experimented, on Android, the callback won't be called by the system
			#if UNITY_ANDROID
			callback(true);
			OpenIAB.restoreTransactions();
			#elif UNITY_IOS || UNITY_WP8
			restoreCallback = callback;
			OpenIAB.restoreTransactions();
			#else
			Debug.LogError("OpenIAB billing currently not supported on this platform. Sorry.");
			#endif
		}

		private static void OnBillingSupportedEvent () {
			Debug.Log ("Billing supported");
		}

		private static void OnBillingNotSupportedEvent (string error) {
			Debug.Log ("Billing not supported");
		}

		private static void OnQueryInventorySucceededEvent (OnePF.Inventory inventory) {
			Debug.Log ("IAB Query Inventory succeeded");
            
            // Consume the consumable items that may remain from the previous session
			List<Purchase> list = inventory.GetAllPurchases ();
			Debug.Log ("Items received: " + list.Count);
			foreach (Purchase purchased in list) {
				Debug.Log(purchased.Sku);
                foreach (IAPItem item in config.Items) 
                {
                    if(item.Id == purchased.Sku)
                    {
                        if(item.Consumable)
                        {
                            Debug.Log("Consuming SKU:" + purchased.Sku);
                            OpenIAB.consumeProduct(purchased);
                        }
                        break;
                    }
                }
			}
		}

		private static void OnQueryInventoryFailedEvent (string error) {
			Debug.Log ("IAB Query Inventory failed");
		}

		private static void OnPurchaseSucceededEvent (OnePF.Purchase purchase) {
			Debug.Log ("IAB Purchase succeeded: " + purchase.Sku);
			foreach (IAPItem item in config.Items)
			{
				if(item.Id == purchase.Sku)
				{
					if(item.Consumable)
					{
						OpenIAB.consumeProduct(purchase);
					}
					break;
				}
			}
			purchaseCallback (true, purchase.Sku);
		}

		private static void OnPurchaseFailedEvent (int info, string error) {
			Debug.Log ("IAB Purchase failed");
			purchaseCallback (false, error);
		}

		private static void OnConsumeSucceededEvent (OnePF.Purchase purchase) {
			Debug.Log ("IAB OnConsume succeeded: " + purchase.Sku);
		}

		private static void OnConsumeFailedEvent (string error) {
			Debug.Log ("IAB OnConsume failed");
		}

		private static void OnRestoreSucceededEvent(){
			Debug.Log ("IAB OnRestore succeeded");
			restoreCallback (true);
		}

		private static void OnRestoreFailedEvent(string error){
			Debug.Log ("IAB OnRestore failed: " + error);
			restoreCallback (false);
		}
	}
}
