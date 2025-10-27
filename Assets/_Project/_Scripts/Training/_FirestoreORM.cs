using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Firebase.Firestore;
using System.Linq;

public static class _FirestoreORM
{
    private static FirebaseFirestore _db;

    public static void Initialize()
    {
        if (_db == null)
        {
            _db = FirebaseFirestore.DefaultInstance;
        }
    }
    public static async Task DebugDocumentConversion<T>(string collectionName, string documentId) where T : class
    {
        try
        {
            Initialize();

            DocumentReference docRef = _db.Collection(collectionName).Document(documentId);
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

            if (snapshot.Exists)
            {
                var rawData = snapshot.ToDictionary();

                T item = snapshot.ConvertTo<T>();
                if (item == null)
                {
                    Debug.LogError("ConvertTo returned null");
                }
            }
            else
            {
                Debug.LogError($"Document {documentId} does not exist");
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Debug conversion failed: {ex.Message}");
        }
    }
    public static async Task<List<T>> GetAllAsync<T>(string collectionName) where T : class
    {
        try
        {
            Initialize();

            CollectionReference collectionRef = _db.Collection(collectionName);
            QuerySnapshot snapshot = await collectionRef.GetSnapshotAsync();

            var results = new List<T>();
            int successCount = 0;

            foreach (DocumentSnapshot document in snapshot.Documents)
            {
                try
                {
                    if (document.Exists)
                    {
                        T item = document.ConvertTo<T>();

                        if (item != null)
                        {
                            results.Add(item);
                            successCount++;
                        }
                        else
                        {
                            Debug.LogError($"ConvertTo returned null for document {document.Id}");
                        }
                    }
                }
                catch (Exception docEx)
                {
                    Debug.LogError($"Failed to convert document {document.Id}: {docEx.Message}");

                    try
                    {
                        var data = document.ToDictionary();
                    }
                    catch (Exception debugEx)
                    {
                        Debug.LogError($"Failed to debug document: {debugEx.Message}");
                    }
                }
            }

            return results;
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error getting {typeof(T).Name} from {collectionName}: {ex.Message}");
            return new List<T>();
        }
    }

    public static async Task<List<ActivityData>> GetAllActivitiesAsync()
    {
        return await GetAllAsync<ActivityData>("Activities");
    }

    public static async Task<List<ChallengeData>> GetAllChallengesAsync()
    {
        return await GetAllAsync<ChallengeData>("Challenges");
    }

    public static async Task<ActivityData> GetActivityByIdAsync(string activityId)
    {
        try
        {
            Initialize();
            DocumentReference docRef = _db.Collection("Activities").Document(activityId);
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

            if (snapshot.Exists)
            {
                return snapshot.ConvertTo<ActivityData>();
            }
            return null;
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error getting activity {activityId}: {ex.Message}");
            return null;
        }
    }

    public static async Task<ChallengeData> GetChallengeByIdAsync(string challengeId)
    {
        try
        {
            Initialize();
            DocumentReference docRef = _db.Collection("Challenges").Document(challengeId);
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

            if (snapshot.Exists)
            {
                return snapshot.ConvertTo<ChallengeData>();
            }
            return null;
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error getting challenge {challengeId}: {ex.Message}");
            return null;
        }
    }

    

    // Debug helper methods removed to reduce noise. Use DebugDocumentConversion<T> for targeted checks.

}