using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class FireBaseInIt : MonoBehaviour {
	Firebase.Auth.FirebaseAuth auth;
	Firebase.Auth.FirebaseUser user;
	Firebase.DependencyStatus dependencyStatus = Firebase.DependencyStatus.UnavailableOther;
	DatabaseReference mDatabaseRef;
	private string email = "";
	private string password = "";
	public GameObject popup;
	public Text popupText;
	public InputField emailField;
	public InputField passField;
	public InputField newUserEmailField;
	public InputField newUserPassField;
	public InputField username;
	public static string UserAccount;
	void Start()
	{
		InitializeFirebase ();
		// Set up the Editor before calling into the realtime database.
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://parallelworld-1a50e.firebaseio.com/");

		// Get the root reference location of the database.
		mDatabaseRef = FirebaseDatabase.DefaultInstance.RootReference;
	}
	public void Signin() {
		email = emailField.text;
		password = passField.text;
		auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
			if (task.IsCanceled) {
				Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
				return;
			}
			if (task.IsFaulted) {
				Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
				popupText.text="Your email or passworld don't correct";
				popup.SetActive(true);
				ResetText ();
				return;
			}

			Firebase.Auth.FirebaseUser newUser = task.Result;
			Debug.LogFormat("User signed in successfully: {0} ({1})",
				newUser.DisplayName, newUser.UserId);
			UserAccount=newUser.UserId;
			Application.LoadLevel ("Menu");
		});
	

	}
	public void SignUp()
	{
		email = newUserEmailField.text;
		password = newUserPassField.text;

		auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
			if (task.IsCanceled) {
				Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
				return;
			}
			if (task.IsFaulted) {
				Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
				popupText.text="Signup error, Please try again";
				popup.SetActive(true);
				ResetText ();
				return;

			}

			// Firebase user has been created.
			Firebase.Auth.FirebaseUser newUser = task.Result;
			Debug.LogFormat("Firebase user created successfully: {0} ({1})",
				newUser.DisplayName, newUser.UserId);
			mDatabaseRef.Child("User").Child(newUser.UserId).Child("Email").SetValueAsync(email);
			mDatabaseRef.Child("User").Child(newUser.UserId).Child("Password").SetValueAsync(password);
			mDatabaseRef.Child("User").Child(newUser.UserId).Child("Username").SetValueAsync(username.text);
		});


	}
	// Track state changes of the auth object.


	void InitializeFirebase() {
		auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
		auth.StateChanged += AuthStateChanged;
		AuthStateChanged(this, null);
	}

	void AuthStateChanged(object sender, System.EventArgs eventArgs) {
		if (auth.CurrentUser != user) {
			bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;
			if (!signedIn && user != null) {
				Debug.Log("Signed out " + user.UserId);
			}
			user = auth.CurrentUser;
			if (signedIn) {
				Debug.Log("Signed in " + user.UserId);

			}
		}
	}
	void ResetText()
	{
		email = "";
		password = "";

	}

}
