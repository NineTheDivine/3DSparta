using System;
using System.Collections;
using UnityEngine;

public class PlayerGrabber : MonoBehaviour, IGrab
{
    //Grab info
    Player player;
    [SerializeField] float throwForce;

    public IGrabable grabObject { get; set; }
    public bool isGrab { get; set; }

    public void Awake()
    {
        player = GetComponent<Player>();
    }
    public void OnGrabEnter()
    {
        isGrab = true;
        StartCoroutine(OnGrabAction());
    }

    public IEnumerator OnGrabAction()
    {
        player.controller.movementSpeed = player.controller.movementSpeed * 0.8f;
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
        isGrab = false;
        player.controller.movementSpeed = player.controller.movementSpeed * 1.25f;
        if (grabObject != null)
        {
            grabObject.GrabbedExitAction(player.controller.PlayerFowardMovement * throwForce * player.transform.forward);
            grabObject = null;
        }
        StopCoroutine(OnGrabAction());
    }
}
