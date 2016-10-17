using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using OnePF;

public class OpenIABManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		OpenIABEventManager.billingSupportedEvent += OnBillingSupportedEvent; 
		OpenIABEventManager.billingNotSupportedEvent += OnBillingNotSupportedEvent; 
		OpenIABEventManager.queryInventorySucceededEvent+= OnQueryInventorySucceededEvent; 
		OpenIABEventManager.queryInventoryFailedEvent += OnQueryInventoryFailedEvent; 
		OpenIABEventManager.purchaseSucceededEvent += OnPurchaseSucceededEvent; 
		OpenIABEventManager.purchaseFailedEvent += OnPurchaseFailedEvent; 
		OpenIABEventManager.consumePurchaseSucceededEvent += OnConsumeSucceededEvent; 
		OpenIABEventManager.consumePurchaseFailedEvent += OnConsumeFailedEvent;

		OnePF.Options options = new OnePF.Options(); 
		options.checkInventory = false; 
		options.prefferedStoreNames = new string[] { OpenIAB_Android.STORE_GOOGLE }; 
		options.verifyMode = OptionsVerifyMode.VERIFY_SKIP;

		options.storeKeys.Add (OpenIAB_Android.STORE_GOOGLE, "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAkLtISYWVFujd/vqORWPmwGCYnqoba09Tgfcjyk04y80YsM/8orcWTPiKFSive4pY3ftmXFsjULTyd/uAjEfAYcnu2JTFFhFP61bEFvy+rduRbW3Mak9SfREaKpcV4/mYQB0J9Tv/Vpu8+lL/YhMetVJNhtWnz5ChADgDk+FknXyfA8iuYg0rSOC1HMrCkGpsLWFn+XBP9WbFWvQLA+dtkIJ+xdaAeuQ7mxX90BqJL3htRFEMy3nnxcedirkpDrnU08v3BakeuKdTIvY+r2G6itR4TxkKpwr/Knou1ODewqDpn3+BJKAEEWMEfRoyZbS9O7wlfI/EtvSO7prPOMaslwIDAQAB");
		OpenIAB.mapSku ("th.co.progaming.playbento.sample1", OpenIAB_Android.STORE_GOOGLE, "th.co.progaming.playbento.sample1");
		OpenIAB.mapSku ("th.co.progaming.playbento.sample1", OpenIAB_iOS.STORE, "th.co.progaming.playbento.sample1");
		OpenIAB.init (options);
	}

	void OnBillingSupportedEvent () {
	
	}

	void OnBillingNotSupportedEvent (string error) {
		
	}

	void OnQueryInventorySucceededEvent (OnePF.Inventory inventory) {
		Debug.Log ("IAB Query Inventory succeeded");
		List<SkuDetails> list = inventory.GetAllAvailableSkus ();
		Debug.Log ("Items received: " + list.Count);
		foreach (SkuDetails detail in list) {
			Debug.Log(detail.Json);
		}
	}

	void OnQueryInventoryFailedEvent (string error) {
		Debug.Log ("IAB Query Inventory failed");
	}

	void OnPurchaseSucceededEvent (OnePF.Purchase purchase) {
		Debug.Log ("IAB Purchase succeeded");
	}

	void OnPurchaseFailedEvent (int info, string error) {
		Debug.Log ("IAB Purchase failed");
	}

	void OnConsumeSucceededEvent (OnePF.Purchase purchase) {
		
	}

	void OnConsumeFailedEvent (string error) {
		
	}
}
