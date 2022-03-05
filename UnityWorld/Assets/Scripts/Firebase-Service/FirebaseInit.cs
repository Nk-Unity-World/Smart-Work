/*

=========== NOTE =========

Code is commented because this project does not contains Firebase plugin, 
You need to add firebase plugin manually and uncomment this code.

==========================

*/

using System.Collections;
using System.Collections.Generic;
//using Firebase.Messaging;
using UnityEngine;

[CreateAssetMenu(fileName = "FirebaseInit", menuName = "NkObjects/FirebaseInit", order = 3)]
public class FirebaseInit : ScriptableObject
{
    /*public Firebase.DependencyStatus dependencyStatus = Firebase.DependencyStatus.UnavailableOther;
    public Firebase.Auth.FirebaseAuth auth;
    public Firebase.Auth.FirebaseUser user;

    //==== Call In Start() Method of Other Script
    public void FirebaseStart()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                Debug.Log("Firebase initializing...");
                InitializeFirebase();
            }
            else
            {
                  print("Could not resolve all Firebase dependencies: " + dependencyStatus, "ERR");
            }
        });
    }

    void InitializeFirebase()
    {
#if !UNITY_WEBGL
        FirebaseMessaging.MessageReceived += OnMessageReceived;
        FirebaseMessaging.TokenReceived += OnTokenReceived;
        Debug.Log("Firebase Messaging Initialized");

        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);

#if UNITY_ANDROID
        ApiManager.apiManager.StartCoroutine(GetTokenAsync());//Use another script coroutine which is not extends ScriptableObject
#endif

#endif
    }




#if !UNITY_WEBGL

#if UNITY_ANDROID
    private IEnumerator GetTokenAsync()
    {
        var task = FirebaseMessaging.GetTokenAsync();

        while (!task.IsCompleted)
            yield return new WaitForEndOfFrame();

        Debug.Log("GET TOKEN ASYNC " + task.Result);
        ApiManager.apiManager.strDeviceToken = task.Result;
    }
#endif

    public void OnTokenReceived(object sender, TokenReceivedEventArgs token)
    {
        Debug.Log("Mobile Token = " + token.Token);
        ApiManager.apiManager.strDeviceToken = token.Token;
    }

    public void OnMessageReceived(object sender, MessageReceivedEventArgs e)
    {
        Debug.Log("Received a new message from: " + e.Message.From);

        FirebaseNotification fn = e.Message.Notification;
        //print(e.Message.MessageId);
        //print(e.Message.RawData);
        //print(e.Message.Data.Count);

	//Check notification by title
        if (fn.Title == "Task Assigned")
        {
            //Do Something....
        }
    }
#endif

    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        Debug.Log("Auth State Changed");
    }

    //==== Call In OnDestroy() Method of Other Script
    public void FirebaseEnd()
    {
        if (auth != null)
        {
            auth.StateChanged -= AuthStateChanged;
            auth = null;
        }
    }*/
}
