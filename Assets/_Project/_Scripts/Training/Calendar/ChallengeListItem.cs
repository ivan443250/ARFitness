﻿using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChallengeListItem : MonoBehaviour, IInitializable<ChallengeData>
{
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private Button _registerButton;
    [SerializeField] private GameObject _registeredBadge;

    private ChallengeData _challengeData;

    public void Initialize(ChallengeData data)
    {
        _challengeData = data;

        _nameText.text = data.Name;
        SetupRegistrationButton();
    }

    private void SetupRegistrationButton()
    {
        if (_challengeData.IsRegistered)
        {
            _registerButton.gameObject.SetActive(false);
            _registeredBadge.SetActive(true);
        }
        else
        {
            _registerButton.gameObject.SetActive(true);
            _registerButton.GetComponentInChildren<TMP_Text>().text = "Участвовать";
            _registerButton.onClick.RemoveAllListeners();
            _registerButton.onClick.AddListener(OnRegisterClicked);
        }
    }

    private void OnRegisterClicked()
    {
        ModalManager.Instance.ShowChoiceModal(
            $"Участвовать в челлендже \"{_challengeData.Name}\"?",
            onApprove: () => RegisterForChallenge(),
            onCancel: null,
            approveText: "Участвовать",
            cancelText: "Отмена"
        );
    }

    private void RegisterForChallenge()
    {
        CalendarManager.Instance.RegisterForChallenge(_challengeData.Id);
        _registeredBadge.SetActive(true);
        _registerButton.gameObject.SetActive(false);

        ModalManager.Instance.ShowAlertModal(
            $"🎉 Вы участвуете в челлендже!\n\"{_challengeData.Name}\"",
            closeText: "Отлично!",
            onClose: () =>
            {
                var mgr = CalendarManager.Instance;
                mgr?.UpdateCalendarDisplay();
            }
        );
    }
}