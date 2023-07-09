public class HitByHeavyBehaviour : HitByBehaviour
{ 
    protected override string GetRequiredSourceName()
    {
        return "Heavy";
    }

    protected override HazardForceData GetData()
    {
        return container.HeavyMultiplier;
    }
}