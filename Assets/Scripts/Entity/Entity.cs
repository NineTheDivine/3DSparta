using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Entity : MonoBehaviour, IDamageable
{
    [Header("Entity Status")]
    public List<Status> statusList;
    public EntityEventBus eventBus { get; protected set; } = new EntityEventBus();

    //Damage Delay
    const float DamageDelay = 1.0f;
    float lastDamagedTime = 0.0f;

    virtual protected void Awake()
    {
        for (int i = 0; i < statusList.Count; i++)
        {
            statusList[i] = Instantiate(statusList[i]);
            statusList[i].Init(this);
        }
    }

    virtual public void TakeDamage(int amount)
    {
        Status Health = GetStatusByType(StatusType.Health);
        if (Health == null)
        {
            throw new FieldAccessException("No Health Status in Entity");
        }
        if (Time.time - lastDamagedTime < DamageDelay)
            return;
        lastDamagedTime = Time.time;
        Health.SubtractValue(amount);
        if (Health.curValue <= 0)
            OnDead();
        else
            OnDamaged(amount);
    }

    abstract public void OnDead();

    virtual public void OnDamaged(int amount) 
    {
        eventBus.InvokeEvent(GetStatusByType(StatusType.Health), amount);
    }

    public Status GetStatusByType(StatusType statusType)
    {
        IEnumerable<Status> query = from status in statusList
                                    where status.StatusType == statusType
                                    select status;
        if (query.Any())
            return query.First();
        return null;
    }

}
