using Firebase.Firestore;
using System;
using System.Collections;
using UnityEngine;

public class FiirestoreDbTest : MonoBehaviour
{
    private FirebaseFirestore _db;

    private void Start()
    {
        _db = FirebaseFirestore.DefaultInstance;

        var training = new CloudEventModel(
            name: "”тренн€€ пробежка",
            organizerId: "trainer_123",
            eventType: "training",
            scheduledStart: DateTime.Today.AddDays(1).AddHours(8),
            scheduledEnd: DateTime.Today.AddDays(1).AddHours(10),
            routeId: "park_route_001"
        );

/*        StartCoroutine(AddEvent(training));*/
    }

/*    private IEnumerator AddEvent(CloudEventModel eventData)
    {
        await _db.Collection("events").AddAsync(training);
    }*/
}