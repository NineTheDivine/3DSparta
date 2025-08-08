using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity, IAttackable
{
    [HideInInspector] public PlayerController controller;
    public PlayerUIControl playerUIControl;
    [HideInInspector] public IGrab playerGrabber;
    

    override protected void Awake()
    {
        base.Awake();
        PlayerManager.Instance.player = this;
        controller = GetComponent<PlayerController>();
        playerGrabber = GetComponent<IGrab>();
        playerUIControl.Init();
    }

    public void ApplyDamage(IDamageable damageable, int damage)
    {
        throw new System.NotImplementedException();
    }

    public override void OnDead()
    {
        Debug.Log("Player Dead");
    }
}
