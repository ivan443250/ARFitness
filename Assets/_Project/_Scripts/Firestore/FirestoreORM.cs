using Cysharp.Threading.Tasks;
using Firebase.Firestore;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Zenject;

public static class FirestoreORM
{
    private static IFirebaseService _firebaseService;
    private static FirebaseFirestore _db;

    public static void SetFirebaseService(IFirebaseService firebaseService)
    {
        _firebaseService = firebaseService;
        _db = _firebaseService.Firestore;
    }

    public static async UniTask<List<T>> ORM_GetAllObjectsAsync<T>(string collectionName) where T : class
    {
        try
        {
            CollectionReference objectsReference = _db.Collection(collectionName);
            QuerySnapshot snapshot = await objectsReference.GetSnapshotAsync();

            var objects = new List<T>();

            foreach (DocumentSnapshot document in snapshot.Documents)
            {
                if (document.Exists)
                {
                    T obj = document.ConvertTo<T>();
                    objects.Add(obj);
                }
            }

            return objects;
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error getting objects: {ex.Message}");
            return new List<T>();
        }
    }

    public static async UniTask<string> AddToFirestoreAsync<T>(T data, string collectionName) where T : class
    {
        try
        {
            CollectionReference collectionRef = _db.Collection(collectionName);
            DocumentReference docRef = await collectionRef.AddAsync(data);
            return docRef.Id;
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to add {typeof(T).Name} to Firestore: {ex.Message}", ex);
        }
    }
}
