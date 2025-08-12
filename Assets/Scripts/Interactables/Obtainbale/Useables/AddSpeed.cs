using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddSpeed : MonoBehaviour, IUseable
{
    [SerializeField] float speedMultiplier = 1.0f;
    [SerializeField] float itemDurationTime;
    public void OnUse()
    {
        PlayerManager.Instance.player.controller.ActivateSpeed(speedMultiplier,itemDurationTime);
    }
}
