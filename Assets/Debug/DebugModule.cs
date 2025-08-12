using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DebugModule : MonoBehaviour
{
    [SerializeField] bool ActiveDebug = false;
    [SerializeField] GameObject testobject;
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
        ItemPool pool =  testobject.GetComponent<ItemPool>();
        if (pool != null)
        {
            pool.SpawnObject(Vector3.zero);
        }
    }


}
