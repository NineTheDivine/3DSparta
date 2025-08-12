using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddDamage : MonoBehaviour, IUseable
{
    [SerializeField] int damageAmount;
    public void OnUse()
    {
        PlayerManager.Instance.player.TakeDamage(damageAmount, true);
    }
}
