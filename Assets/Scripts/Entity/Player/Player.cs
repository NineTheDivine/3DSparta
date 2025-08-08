using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity, IAttackable, IGrab
{
    PlayerController controller;
    [SerializeField] PlayerUIControl PlayerUIControl;

    //Grab info
    public IGrabable grabObject { get; set; }
    public bool isGrab { get; set; }

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
        Debug.Log("Player Dead");
    }

    public void ActiveGrab(bool activate)
    {
        isGrab = activate;
        if (activate)
            StartCoroutine(OnGrabAction());
        else
        {
            StopCoroutine(OnGrabAction());
            OnGrabExit();
        }
    }

    public IEnumerator OnGrabAction()
    {
        controller.movementSpeedMultiplier = 0.8f;
        while (isGrab)
        {
            if (grabObject != null)
            {
                grabObject.GrabbedBehaviour(transform.position, transform.forward);
            }
            yield return new WaitForFixedUpdate();
        }
    }

    public void OnGrabExit()
    {
        controller.movementSpeedMultiplier = 1.0f;
        if (grabObject != null)
        {
            grabObject.GrabbedExitAction();
            grabObject = null;
        }        
    }
}
