/*********************************
 * Author:          Kyle Grenier
 * Date Created:    
 /********************************/

using System;
using UnityEngine;

public class HitByBottleBehaviour : HitByBehaviour
{
    protected override string GetRequiredSourceName()
    {
        return "Bottle";
    }

    protected override HazardForceData GetData()
    {
        return container.BottleMultiplier;
    }
}