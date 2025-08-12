using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddHealth : MonoBehaviour, IUseable
{
    [SerializeField] int healthAmount;
    public void OnUse()
    {
        PlayerManager.Instance.player.TakeHeal(healthAmount);
    }
}
