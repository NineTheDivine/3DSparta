using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [SerializeField] LayerMask layerMask;
    [SerializeField] float jumpPower;
    [SerializeField] Vector3 jumpForce;
    [SerializeField] JumperTop jumperTop;
    [SerializeField] float jumpDelay;
    float lastJumpTime = 0.0f;
    bool isActivate;
    List<Rigidbody> collidiingObjects = new List<Rigidbody>();


    private void Awake()
    {
        jumpForce = jumpForce.normalized * jumpPower;
        jumperTop.force = jumpForce;
        isActivate = false;
    }

    private void Update()
    {
        if (isActivate && Time.time - lastJumpTime >= jumpDelay)
        {
            isActivate = false;
            jumperTop.ReloadJumper();
        }
        if (isActivate)
            return;
        if (collidiingObjects.Count != 0)
        {
            lastJumpTime = Time.time;
            isActivate = true;
            jumperTop.OnAction();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((layerMask & (1 << collision.gameObject.layer)) == 0)
            return;
        if (collision.gameObject.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            if(!collidiingObjects.Contains(rb)) 
                collidiingObjects.Add(rb);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if ((layerMask & (1 << collision.gameObject.layer)) == 0)
            return;
        if (collision.gameObject.TryGetComponent<Rigidbody>(out Rigidbody rb))
            collidiingObjects.Remove(rb);
    }
}
