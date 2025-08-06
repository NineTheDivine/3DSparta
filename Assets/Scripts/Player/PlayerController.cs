using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Rigidbody rigidbody;


    [Header("Player Movement")]
    [SerializeField] float _movementSpeed;
    [HideInInspector]public float MovementSpeed { get => _movementSpeed; }
    Vector2 currentMovementInput;
    [SerializeField] float _jumpPower;
    [HideInInspector]public float JumpPower { get => _jumpPower; }
    [SerializeField] LayerMask groundLayerMask;

    [Header("Player Look")]
    [SerializeField] Transform cameraContainer;
    [SerializeField] float minLook;
    [SerializeField] float maxLook;
    float cameraXRotation;
    [SerializeField] float lookSensitivity;
    Vector2 mouseDelta;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        Look();
    }


    //Input Actions
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();
    }
    public void OnLookInput(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    //Input Actions End

    //Player Actions
    void Move()
    {
        Vector3 dir = transform.forward * currentMovementInput.y + transform.right * currentMovementInput.x;
        dir = dir * _movementSpeed;
        dir.y = rigidbody.velocity.y;

        rigidbody.velocity = dir;
    }
    void Look()
    {
        cameraXRotation += mouseDelta.y * lookSensitivity;
        cameraXRotation = Mathf.Clamp(cameraXRotation, minLook, maxLook);
        cameraContainer.localEulerAngles = new Vector3(-cameraXRotation, 0, 0);

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }
}
