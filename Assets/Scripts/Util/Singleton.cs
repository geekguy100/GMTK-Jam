using System;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    public static T Instance { get; private set; }

    private void Awake()
    {
        if (ReferenceEquals(Instance, null))
        {
            Instance = this as T;
        }
        else if (!ReferenceEquals(Instance, this))
        {
            Destroy(gameObject);
            return;
        }
        
        PostAwake();
    }

    protected void OnDestroy()
    {
        if (ReferenceEquals(Instance, this))
        {
            Instance = null;
        }
    }

    protected virtual void PostAwake(){}
}