using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Time Data", menuName = "Game Data/Time Data", order = 0)]
public class TimeData : ScriptableObject
{
    [Header("Time")]
    public float timeScale = 1.0f;
    public float totalGameTimeSeconds = 60.0f;
}
