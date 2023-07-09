/*********************************
 * Author:          Kyle Grenier
 * Date Created:    
 /********************************/

using System;
using UnityEngine;

public class DestroyIfHitByFighterBehaviour : MonoBehaviour, IDamageableBehaviour
{
    public void PerformBehaviour(DamageData data)
    {
        if (data.sourceName != "Fighter")
            return;
        
        GetComponent<EnvironmentObject>().OnRemove();
    }
}