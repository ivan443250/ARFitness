using Cysharp.Threading.Tasks;
using Firebase;
using Firebase.Auth;
using Firebase.Firestore;
using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class FirebaseService : IFirebaseService
{
    public FirebaseApp App { get; private set; }
    public FirebaseAuth Auth { get; private set; }
    public FirebaseFirestore Firestore { get; private set; }
    public CloudUserModel UserOnAuth { get; private set; }

    public void SetUserOnAuth(CloudUserModel currnetUserOnAuth)
    {
        UserOnAuth = currnetUserOnAuth;
    }

    public CloudUserModel GetUserOnAuth()
    {
        return UserOnAuth;
    }

    public async UniTask Initialize()
    {
        if (App != null) return;

        var dependencyStatus = await FirebaseApp.CheckAndFixDependenciesAsync();

        if (dependencyStatus == DependencyStatus.Available)
        {
            App = FirebaseApp.DefaultInstance;
            Auth = FirebaseAuth.DefaultInstance;
            Firestore = FirebaseFirestore.DefaultInstance;

            if (App?.Options != null && string.IsNullOrEmpty(App.Options.DatabaseUrl?.AbsoluteUri))
            {
                App.Options.DatabaseUrl = new Uri("https://arfitness-1e168-default-rtdb.firebaseio.com/");
            }

            Debug.Log("Firebase initialized successfully");
        }
        else
        {
            Debug.LogError($"Could not resolve dependencies: {dependencyStatus}");
        }
    }
}
