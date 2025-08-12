using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InteractionControl : MonoBehaviour
{
    IGrab grabber;
    const float UPDATEDELAY = 0.05f;
    float lastUpdate = 0.0f;
    const float DETECTDISTANCE = 1.0f;
    Camera cam;
    [Header("CrossHair")]
    [SerializeField] Transform UIParent;
    [SerializeField] Sprite crosshairImg;
    RectTransform crosshair;
    Vector3 midPos;
    [Header("Interactables")]
    [SerializeField] LayerMask itemLayer;
    IGrabable currentGrabable;
    IInteractable currentInteractObject;
    

    private void Awake()
    {
        grabber = GetComponent<IGrab>();
        cam = Camera.main;
        crosshair = new GameObject("CrossHairUI").AddComponent<RectTransform>();
        crosshair.transform.SetParent(UIParent);
        crosshair.gameObject.AddComponent<Image>().sprite = crosshairImg;
        crosshair.gameObject.GetComponent<Image>().color = Color.red;
        crosshair.localScale = Vector2.one * 0.1f;
        crosshair.anchoredPosition = Vector3.zero;
        midPos = new Vector3(Screen.width / 2, Screen.height / 2);
    }

    private void Update()
    {
        if (PlayerManager.Instance.isUIAcvite)
            return;
        if (Time.time - lastUpdate < UPDATEDELAY)
            return;
        lastUpdate = Time.time;
        Ray ray = cam.ScreenPointToRay(midPos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, DETECTDISTANCE, itemLayer))
        {
            if (hit.collider.gameObject.TryGetComponent<IGrabable>(out IGrabable grabtarget))
            {
                if (grabtarget != currentGrabable)
                    currentGrabable = grabtarget;
            }
            else
                currentGrabable = null;
            if (hit.collider.gameObject.TryGetComponent<IInteractable>(out IInteractable itemtarget))
            {
                if (itemtarget != currentInteractObject)
                    currentInteractObject = itemtarget;
            }
            else
                currentInteractObject = null;
        }
        else
        {
            currentGrabable = null;
            currentInteractObject = null;
        }
    }

    public void OnGrabInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            //Set Player Grab mode
            grabber.OnGrabEnter();
            //If current Interactable is not null, Grab
            if(grabber.grabObject == null && currentGrabable != null)
                grabber.grabObject = currentGrabable;
        }
        else if (grabber.grabObject == null && currentGrabable != null && context.phase == InputActionPhase.Performed)
        {
            grabber.grabObject= currentGrabable;
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            //End Player Grab mode
            grabber.OnGrabExit();
        }
    }

    public void OnInteractInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            //If current Interactable is not null, add to Inventory
            if (currentInteractObject != null)
                currentInteractObject.OnInteract();
        }
    }

    public void SetUIActive(bool active)
    {
        crosshair.gameObject.SetActive(active);
    }
}
