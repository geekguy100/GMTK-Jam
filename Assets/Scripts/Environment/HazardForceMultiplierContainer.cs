/*********************************
 * Author:          Kyle Grenier
 * Date Created:    
 /********************************/

using UnityEngine;

[CreateAssetMenu(menuName = "Create HazardForceMultiplierContainer", fileName = "HazardForceMultiplierContainer", order = 0)]
public class HazardForceMultiplierContainer : ScriptableObject
{
    public HazardForceData BottleMultiplier => bottleMultiplier;
    public HazardForceData HeavyMultiplier => heavyMultiplier;
    public HazardForceData FighterMultiplier => fighterMultiplier;
    public HazardForceData StoolMultiplier => stoolMultiplier;

    [SerializeField] private HazardForceData heavyMultiplier;
    [SerializeField] private HazardForceData bottleMultiplier;
    [SerializeField] private HazardForceData fighterMultiplier;
    [SerializeField] private HazardForceData stoolMultiplier;
}