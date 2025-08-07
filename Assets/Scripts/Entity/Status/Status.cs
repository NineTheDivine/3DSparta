using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatusType
{
    None = 0,
    Health
}
[CreateAssetMenu(fileName = "Status", menuName = "ScriptableObject/EntityStatus")]
public class Status : ScriptableObject
{
    private EntityEventBus entityBus;
    public StatusType StatusType;
    public int curValue;
    public int MaxValue;
    public int MinValue;


    public void Init(Entity entity)
    {
        entityBus = entity.eventBus;
        if(curValue != MaxValue)
            curValue = MaxValue;
    }

    public void AddValue(int amount)
    {
        curValue = Mathf.Min(curValue + amount, MaxValue);
        if(entityBus != null)
            entityBus.InvokeEvent(this, amount);
    }
    public void SubtractValue(int amount)
    {
        curValue = Mathf.Max(curValue - amount, MinValue);
        if (entityBus != null)
            entityBus.InvokeEvent(this, -amount);
    }
}
