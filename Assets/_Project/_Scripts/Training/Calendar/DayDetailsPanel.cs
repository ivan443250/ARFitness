using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class DayDetailsPanel : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject _panel;
    [SerializeField] private TMP_Text _dateText;
    [SerializeField] private Transform _activitiesContent;
    [SerializeField] private Transform _challengesContent;
    [SerializeField] private GameObject _activityItemPrefab;
    [SerializeField] private GameObject _challengeItemPrefab;
    [SerializeField] private Button _repeatButton;
    [SerializeField] private Button _planButton;
    [SerializeField] private Button _closeButton;

}