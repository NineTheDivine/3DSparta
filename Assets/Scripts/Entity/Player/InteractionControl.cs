using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InteractionControl : MonoBehaviour
{
    Player player;
    const float UPDATEDELAY = 0.05f;
    float lastUpdate = 0.0f;
    const float DETECTDISTANCE = 1.0f;
    Camera cam;
    [Header("CrossHair")]
    [SerializeField] Transform UIParent;
    [SerializeField] Sprite crosshairImg;
    Vector3 midPos;
    [Header("Interactables")]
    [SerializeField] LayerMask itemLayer;
    IGrabable currentGrabable;
    

    private void Awake()
    {
        player = GetComponent<Player>();
        cam = Camera.main;
        RectTransform crosshair = new GameObject("CrossHairUI").AddComponent<RectTransform>();
        crosshair.transform.SetParent(UIParent);
        crosshair.gameObject.AddComponent<Image>().sprite = crosshairImg;
        crosshair.gameObject.GetComponent<Image>().color = Color.red;
        crosshair.localScale = Vector2.one * 0.1f;
        crosshair.anchoredPosition = Vector3.zero;
        midPos = new Vector3(Screen.width / 2, Screen.height / 2);
    }

    private void Update()
    {
        if (Time.time - lastUpdate < UPDATEDELAY)
            return;
        lastUpdate = Time.time;
        Ray ray = cam.ScreenPointToRay(midPos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, DETECTDISTANCE, itemLayer))
        {
            if (hit.collider.gameObject.TryGetComponent<IGrabable>(out IGrabable target))
            {
                if (target != currentGrabable)
                    currentGrabable = target;
            }
            else
            {
                currentGrabable = null;
            }
        }
        else
        {
            currentGrabable = null;
        }
    }

    public void OnInteractionInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            //Set Player Grab mode
            player.ActiveGrab(true);
            //If current Interactable is not null, Grab
            if(player.grabObject == null && currentGrabable != null)
                player.grabObject = currentGrabable;
        }
        else if (player.grabObject == null && currentGrabable != null && context.phase == InputActionPhase.Performed)
        {
            player.grabObject= currentGrabable;
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            //End Player Grab mode
            player.ActiveGrab(false);
        }
    }
}
