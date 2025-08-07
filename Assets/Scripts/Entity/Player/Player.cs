using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity, IAttackable
{
    PlayerController controller;
    [SerializeField] PlayerUIControl PlayerUIControl;

    override protected void Awake()
    {
        base.Awake();
        PlayerManager.Instance.player = this;
        controller = GetComponent<PlayerController>();
        PlayerUIControl.Init();
    }

    public void ApplyDamage(IDamageable damageable, int damage)
    {
        throw new System.NotImplementedException();
    }

    public override void OnDead()
    {
        throw new System.NotImplementedException();
    }
}
