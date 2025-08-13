using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] Vector3 destination;
    [SerializeField] float platformMoveTime = 1.0f;
    [SerializeField] float waitDelay = 0.0f;
    Vector3 startPos = Vector3.zero;

    Vector3 currentPos = Vector3.zero;
    Vector3 Incremental;
    bool isMovingPositive = true;

    const float threshold = 0.01f;

    float waitTime = 0.0f;
    Rigidbody platformRigidbody;
    Collider platformCollider;
    protected List<Rigidbody> collidingObjects = new List<Rigidbody>();

    void Start()
    {
        platformRigidbody = GetComponent<Rigidbody>();
        platformCollider = GetComponent<Collider>();
        if (platformMoveTime == 0.0f)
            throw new Exception("Platform MoveTime is zero");
        Incremental = destination * Time.fixedDeltaTime / platformMoveTime;

        platformRigidbody = GetComponent<Rigidbody>();
        if (destination.x == 0)
            platformRigidbody.constraints = platformRigidbody.constraints | RigidbodyConstraints.FreezePositionX;
        if (destination.y == 0)
            platformRigidbody.constraints = platformRigidbody.constraints | RigidbodyConstraints.FreezePositionY;
        if (destination.z == 0)
            platformRigidbody.constraints = platformRigidbody.constraints | RigidbodyConstraints.FreezePositionZ;


        startPos = platformRigidbody.position;
        destination = platformRigidbody.position + destination;
        currentPos = startPos;

    }

    // Update is called once per frame
    void Update()
    {
        if (waitDelay != 0.0f && Time.fixedTime - waitTime < waitDelay)
            return;

        currentPos += Incremental;
        platformRigidbody.MovePosition(currentPos);
        if (isMovingPositive)
        {
            if ((destination - currentPos).sqrMagnitude < threshold * threshold)
            {
                Incremental *= -1;
                currentPos = destination;
                isMovingPositive = false;
                waitTime = Time.fixedTime;
                platformRigidbody.MovePosition(destination);
            }
        }
        else
        {
            if ((currentPos - startPos).sqrMagnitude < threshold * threshold)
            {
                Incremental *= -1;
                currentPos = startPos;
                isMovingPositive = true;
                waitTime = Time.fixedTime;
                platformRigidbody.MovePosition(startPos);
            }
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            if (!collidingObjects.Contains(rb))
            {
                Ray[] rays = new Ray[4]
                {
                    new Ray(rb.transform.position + (rb.transform.forward * 0.2f) + (rb.transform.up * 0.01f), Vector3.down),
                    new Ray(rb.transform.position + (-rb.transform.forward * 0.2f) + (rb.transform.up * 0.01f), Vector3.down),
                    new Ray(rb.transform.position + (rb.transform.right * 0.2f) + (rb.transform.up * 0.01f), Vector3.down),
                    new Ray(rb.transform.position + (-rb.transform.right * 0.2f) + (rb.transform.up * 0.01f), Vector3.down)
                };
                for(int i = 0; i < rays.Length; i++)
                {
                    if (!Physics.Raycast(rays[i], out RaycastHit hit, 0.1f) || hit.collider != platformCollider)
                    {
                        Debug.Log(hit.collider);
                        return;
                    }
                }
                Debug.Log("Enter");
                collidingObjects.Add(rb);
                collision.transform.SetParent(transform, true);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            if (collidingObjects.Contains(rb))
            {
                Ray[] rays = new Ray[4]
                {
                    new Ray(rb.transform.position + (rb.transform.forward * 0.2f) + (rb.transform.up * 0.01f), Vector3.down),
                    new Ray(rb.transform.position + (-rb.transform.forward * 0.2f) + (rb.transform.up * 0.01f), Vector3.down),
                    new Ray(rb.transform.position + (rb.transform.right * 0.2f) + (rb.transform.up * 0.01f), Vector3.down),
                    new Ray(rb.transform.position + (-rb.transform.right * 0.2f) + (rb.transform.up * 0.01f), Vector3.down)
                };
                RaycastHit hit;
                for (int i = 0; i < rays.Length; i++)
                {
                    if (Physics.Raycast(rays[i], out hit, 0.1f) && hit.collider == platformCollider)
                        return;
                }
                Debug.Log("Exit");
                collidingObjects.Remove(rb);
                collision.transform.SetParent(null, true);
            }
        }
    }

}
