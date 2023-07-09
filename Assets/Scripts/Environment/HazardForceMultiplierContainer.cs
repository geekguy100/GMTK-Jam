/*********************************
 * Author:          Kyle Grenier
 * Date Created:    
 /********************************/

using UnityEngine;

[CreateAssetMenu(menuName = "Create HazardForceMultiplierContainer", fileName = "HazardForceMultiplierContainer", order = 0)]
public class HazardForceMultiplierContainer : ScriptableObject
{
    public float FoodMultiplier => foodMultiplier;
    public float BottleMultiplier => bottleMultiplier;
    public float HeavyMultiplier => heavyMultiplier;
    public float StoolMultiplier => stoolMultiplier;

    [SerializeField] private float heavyMultiplier;
    [SerializeField] private float bottleMultiplier;
    [SerializeField] private float foodMultiplier;
    [SerializeField] private float stoolMultiplier;

    public float GetMultiplier(ObstacleType obstacleType)
    {
        switch (obstacleType)
        {
            case ObstacleType.Bottle:
                return bottleMultiplier;
            case ObstacleType.Stool:
                return stoolMultiplier;
            case ObstacleType.Food:
                return foodMultiplier;
            case ObstacleType.Heavy:
                return heavyMultiplier;
        }

        return 1;
    }
}