using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerUIControl : MonoBehaviour
{
    Canvas PlayerStatusParent;
    [SerializeField]StatusUI playerStatusUIPrefab;

    public void Init()
    {
        Player player = PlayerManager.Instance.player;
        PlayerStatusParent = new GameObject("PlayerStatusUI").AddComponent<Canvas>();
        PlayerStatusParent.transform.SetParent(transform);
        if (!PlayerStatusParent.TryGetComponent<RectTransform>(out RectTransform rect))
            throw new UnityException("No Rect Transform inside Canvas");
        rect.pivot = new Vector2(0.5f, 1f);
        rect.anchorMax = new Vector2(0.5f, 1);
        rect.anchorMin = new Vector2(0.5f, 1);
        rect.anchoredPosition = new Vector2(0, -50);
        foreach (Status status in PlayerManager.Instance.player.statusList)
        {
            StatusUI statusUI = Instantiate(playerStatusUIPrefab, PlayerStatusParent.transform);
            statusUI.Init(player);
        }
    }
}
