using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

public class EntityEventBus
{
    Dictionary<Status, List<Action<int>>> OnStatusChangedFunctions  = new Dictionary<Status, List<Action<int>>>();

    public void SubscribeStatusChange(Status status, Action<int> action)
    {
        if (!OnStatusChangedFunctions.ContainsKey(status))
        {
            OnStatusChangedFunctions.Add(status, new List<Action<int>>());
        }
        if (!OnStatusChangedFunctions[status].Contains(action))
        {
            OnStatusChangedFunctions[status].Add(action);
        }
    }

    public void UnsubscribeStatusChange(Status status, Action<int> action)
    {
        if (OnStatusChangedFunctions.ContainsKey(status))
        {
            OnStatusChangedFunctions[status].Remove(action);
        }
    }

    public void InvokeEvent(Status status, int input)
    {
        if (OnStatusChangedFunctions.ContainsKey(status))
        {
            foreach (Action<int> action in OnStatusChangedFunctions[status])
            {
                action(input);
            }
        }
    }
}
