using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Interact Data", menuName = "Interact/Interact Data", order = 0)]
public class MouseInteractData : ScriptableObject
{
    [Header("Interact")]

    /// <summary>
    /// The lerp rate of the interacted object is to the mouse position.
    /// </summary>
    public float mouseLerpSpeed = 0.1f;

    /// <summary>
    /// Power at wich the force is applied to the object once the mouse pointer is released.
    /// </summary>
    public float forcePower = 100f;

    /// <summary>
    /// The rate at which the mouse position is sampled.
    /// </summary>
    public float posSampleRateSeconds = 0.1f;

    /// <summary>
    /// The rate at which an object will follow the mouse
    /// </summary>
    public float mouseFollowMultiplier = 10f;
}
