using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Scene Data", menuName = "Game Data/Scene Data", order = 0)]
public class SceneData : ScriptableObject
{
    [Header("Scenes")]
    public string titleSceneName;
    public string gameSceneName;
}
