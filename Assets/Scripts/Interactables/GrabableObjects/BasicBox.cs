using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBox : MonoBehaviour, IGrabable
{
    public Rigidbody rb {  get; set; }
    public float grabDistance;

    public void Awake()
    {
        if (!TryGetComponent<Rigidbody>(out Rigidbody body))
        {
            gameObject.AddComponent<BoxCollider>();
            body = gameObject.AddComponent<Rigidbody>();
        }
        rb = body;
    }


    public void GrabbedBehaviour(Vector3 grabberpos, Vector3 angle)
    {
        if(!rb.isKinematic)
            rb.isKinematic = true;

        rb.position = grabberpos + Vector3.up  + angle * grabDistance;
    }

    public void GrabbedExitAction()
    {
        if(rb.isKinematic)
            rb.isKinematic = false;
    }
}
