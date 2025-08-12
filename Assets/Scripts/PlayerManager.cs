using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager _playerManager;
    public static PlayerManager Instance
    {
        get
        {
            if (_playerManager == null)
            {
                Debug.Log("Call");
                _playerManager = new GameObject("PlayerManager").AddComponent<PlayerManager>();
            }
            return _playerManager;
        }
    }
    public Player player;
    public bool isUIAcvite = false;

    private void Awake()
    {
        if (_playerManager == null)
        {
            _playerManager = this;
            
            DontDestroyOnLoad(gameObject);
        }
        else if (_playerManager != this)
            Destroy(gameObject);
    }
}
