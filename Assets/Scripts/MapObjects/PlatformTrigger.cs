using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlatformTrigger : MonoBehaviour
{
    protected Collider platformCollider;
    protected List<Rigidbody> collidingObjects = new List<Rigidbody>();

    protected virtual void Start()
    {
        platformCollider = GetComponent<Collider>();
    }

    public abstract void OnAction();

    protected void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            if (!collidingObjects.Contains(rb))
            {
                collidingObjects.Add(rb);
            }
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            if (collidingObjects.Contains(rb))
            {
                collidingObjects.Remove(rb);
            }
        }
    }
}
