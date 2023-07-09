using System;
using UnityEngine;

public class HitByFighterBehaviour : HitByBehaviour
{
    protected override string GetRequiredSourceName()
    {
        return "Fighter";
    }

    protected override HazardForceData GetData()
    {
        return container.FighterMultiplier;
    }
}