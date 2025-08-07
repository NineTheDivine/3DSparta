using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DebugModule : MonoBehaviour
{
    [SerializeField] bool ActiveDebug = false;
    DebugManager debugManager;
    private void Awake()
    {
#if UNITY_EDITOR
        if (ActiveDebug)
        {
            DontDestroyOnLoad(gameObject);
            debugManager = new DebugManager(ActiveDebug);
        }
        else
            Destroy(gameObject);
#else
        Destroy(gameObject);
#endif
    }


    public void OnDebugAction(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            DoAction();
        }
    }

    /* Implement Test Codes Here */
    private void DoAction()
    {
        Player player = GameObject.FindWithTag("Player").GetComponent<Player>();
        if (player == null)
            throw new MissingComponentException("Player not found");
        player.TakeDamage(5);
    }


}
