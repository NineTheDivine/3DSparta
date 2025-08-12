using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.WSA;

public class PlayerController : MonoBehaviour
{
    Rigidbody _rigidbody;

    [Header("Player Movement")]
    [SerializeField] float _movementSpeed;
    public float movementSpeed { get => _movementSpeed; set { _movementSpeed = value; } }
    public float movementSpeedMultiplier = 1.0f;

    float RunningCorutineSpeed = 1.0f;
    float RemainingBuffTime = 0.0f;
    [HideInInspector]public float PlayerFowardMovement 
    { get => currentMovementInput.y * _movementSpeed * movementSpeedMultiplier > 0 ? currentMovementInput.y * _movementSpeed * movementSpeedMultiplier : 0; }
    Vector2 currentMovementInput;
    Vector3 prevdir = Vector3.zero;
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
        _rigidbody = GetComponent<Rigidbody>();
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
        if (PlayerManager.Instance.isUIAcvite)
            return;
        if (context.phase == InputActionPhase.Performed)
            currentMovementInput = context.ReadValue<Vector2>();
        else if (context.phase == InputActionPhase.Canceled)
            currentMovementInput = Vector2.zero;
    }
    public void OnLookInput(InputAction.CallbackContext context)
    {
        if (PlayerManager.Instance.isUIAcvite)
            return;
        mouseDelta = context.ReadValue<Vector2>();
    }
    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (PlayerManager.Instance.isUIAcvite)
            return;
        if (context.phase == InputActionPhase.Started && IsGrounded())
        {
            _rigidbody.AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
        }
    }

    public void OnEnableInventory(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            GetComponent<Player>().inventory.gameObject.SetActive(true);
            PlayerManager.Instance.isUIAcvite = true;
            currentMovementInput = Vector2.zero;
            GetComponent<InteractionControl>().SetUIActive(true);
        }
    }

    public void OnDisableInventory(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            GetComponent<Player>().inventory.gameObject.SetActive(false);
            PlayerManager.Instance.isUIAcvite = false;
            GetComponent<InteractionControl>().SetUIActive(false);
        }
    }
    //Input Actions End

    //Player Actions
    void Move()
    {
        Vector3 curdir = transform.forward * currentMovementInput.y + transform.right * currentMovementInput.x;
        curdir = curdir * _movementSpeed * movementSpeedMultiplier * Time.fixedDeltaTime;
        _rigidbody.MovePosition(transform.position + curdir);
    }

    void Look()
    {
        cameraXRotation += mouseDelta.y * lookSensitivity;
        cameraXRotation = Mathf.Clamp(cameraXRotation, minLook, maxLook);
        cameraContainer.localEulerAngles = new Vector3(-cameraXRotation, 0, 0);

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }

    bool IsGrounded()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down)
        };

        for(int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.1f, groundLayerMask))
                return true;
        }
        return false;
    }




    //Speed Corutine
    public void ActivateSpeed(float speedMultiplier, float durationTime)
    {
        //override Buff while use during other buff
        if (RunningCorutineSpeed != speedMultiplier)
        {
            RunningCorutineSpeed = speedMultiplier;
            RemainingBuffTime = durationTime;
            this.movementSpeedMultiplier = speedMultiplier; 
            
            StartCoroutine(GiveSpeedBuff(speedMultiplier));
        }
        //if Same Speed buff, refresh duration time if remaining time is less than duration.
        else if (RemainingBuffTime < durationTime)
        {
            RemainingBuffTime = durationTime;
        }
    }


    IEnumerator GiveSpeedBuff(float speedMultiplier)
    {
        while (RunningCorutineSpeed == speedMultiplier && RemainingBuffTime > 0)
        {
            RemainingBuffTime -= Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        //Speed Buff End
        if (RunningCorutineSpeed == speedMultiplier)
        {
            RemainingBuffTime = 0;
            RunningCorutineSpeed = 1.0f;
            PlayerManager.Instance.player.controller.movementSpeedMultiplier = 1.0f;
        }
        //Otherwise, override Speed buff
    }
}
