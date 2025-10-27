using Cysharp.Threading.Tasks;
using Firebase;
using Firebase.Auth;
using Firebase.Firestore;
using System;
using System.Collections;
using System.Threading.Tasks;

public interface IFirebaseService
{
    FirebaseApp App { get; }
    FirebaseAuth Auth { get; }
    FirebaseFirestore Firestore { get; }
    UniTask Initialize();
    void SetUserOnAuth(CloudUserModel currentUserOnAuth);
    CloudUserModel GetUserOnAuth();
}
