using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperTop : PlatformTrigger
{
    Material jumperTopMaterial;
    Animator jumperTopAnimator;
    public Vector3 force;
    Color reloadColor;

    protected override void Start()
    {
        jumperTopMaterial = GetComponent<MeshRenderer>().materials[0];
        reloadColor = jumperTopMaterial.color;
        jumperTopAnimator = GetComponent<Animator>();
        base.Start();
    }
    public override void OnAction()
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
}


