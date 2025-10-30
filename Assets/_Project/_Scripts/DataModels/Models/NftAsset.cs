using Firebase.Firestore;
using System;

namespace DataModels
{
    [Serializable]
    [FirestoreData]
    public class NftAsset
    {
        [FirestoreProperty] public string Id { get; set; }
        [FirestoreProperty] public string OwnerUserId { get; set; }
        [FirestoreProperty] public string ContractAddress { get; set; }
        [FirestoreProperty] public string TokenId { get; set; }
        [FirestoreProperty] public string Name { get; set; }
        [FirestoreProperty] public string ImageUrl { get; set; }
        [FirestoreProperty] public DateTime AcquiredAt { get; set; }
    }
}
