/*********************************
 * Author:          Kyle Grenier
 * Date Created:    
 /********************************/

using System;
using UnityEngine;

namespace KpattCore.Channels
{
    [CreateAssetMenu(menuName = "Channels/Void Channel", fileName = "New Void Channel", order = 0)]
    public class Channel : ScriptableObject
    {
        public event Action OnEventRaised;

        public virtual void RaiseEvent()
        {
            OnEventRaised?.Invoke();
        }
    }

    public abstract class Channel<T> : Channel
    {
        public new event Action<T> OnEventRaised;

        public virtual void RaiseEvent(T value)
        {
            base.RaiseEvent();
            OnEventRaised?.Invoke(value);
        }
    }
}