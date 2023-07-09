using UnityEngine;

public class HitByStoolBehaviour : HitByBehaviour
{
    protected override string GetRequiredSourceName()
    {
        return "Stool";
    }

    protected override HazardForceData GetData()
    {
        return container.StoolMultiplier;
    }
}