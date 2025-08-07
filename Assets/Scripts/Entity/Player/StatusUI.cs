using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusUI : MonoBehaviour
{
    Entity entity;
    public Action<int> StatusActions;
    [SerializeField] StatusType statusType;
    [SerializeField] Image statusSprite;
    [SerializeField] TextMeshProUGUI nameTag;
    Status status;

    public void Init(Entity entity)
    {
        this.entity = entity;
        if (statusType == StatusType.None)
            throw new MissingFieldException("Undefined Status in StatusUI");
        status = this.entity.GetStatusByType(statusType);
        StatusActions = OnStatusChanged;
        this.entity.eventBus.SubscribeStatusChange(status, StatusActions);

        switch (statusType)
        {
            case StatusType.Health:
                nameTag.text = "Health";
                break;
        }
    }

    private void OnStatusChanged(int amount)
    {
        if (amount == 0)
            return;
        if (amount > 0)
            Debug.Log("Heal");
        else if (amount < 0)
            Debug.Log("Damaged");
        UpdateUI();
    }

    private void UpdateUI()
    {
        statusSprite.fillAmount = (float)status.curValue / status.MaxValue;
    }

    private void OnDestroy()
    {
        entity.eventBus.UnsubscribeStatusChange(status, StatusActions);
    }
}
