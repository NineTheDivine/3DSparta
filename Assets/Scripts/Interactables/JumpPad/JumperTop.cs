using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperTop : MonoBehaviour
{
    Material jumperTopMaterial;
    Animator jumperTopAnimator;
    Collider jumperTopCollider;
    Color reloadColor;
    List<Rigidbody> collidingObjects = new List<Rigidbody>();

    private void Start()
    {
        jumperTopMaterial = GetComponent<MeshRenderer>().materials[0];
        reloadColor = jumperTopMaterial.color;
        jumperTopAnimator = GetComponent<Animator>();
        jumperTopCollider = GetComponent<Collider>();
    }
    public void JumpPadAction(Vector3 force)
    {
        jumperTopAnimator.SetBool("Jump", true);
        jumperTopMaterial.color = Color.gray;
        foreach (Rigidbody rigidbody in collidingObjects)
        {
            rigidbody.AddForce(force, ForceMode.Impulse);
        }
    }

    public void JumpAnimationEnd()
    {
        jumperTopAnimator.SetBool("Jump", false);
    }

    public void ReloadJumper()
    {
        jumperTopMaterial.color = reloadColor;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            if (!collidingObjects.Contains(rb))
                collidingObjects.Add(rb);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            if (collidingObjects.Contains(rb))
                collidingObjects.Remove(rb);
        }
    }
}


