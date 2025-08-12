using UnityEngine;

public interface IGrabable
{
    public Rigidbody rb { get; set; }
    public void GrabbedBehaviour(Vector3 grabberPos, Vector3 Angle);
    public void GrabbedExitAction(Vector3 throwForce);
}