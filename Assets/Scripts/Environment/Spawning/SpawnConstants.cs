using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObstacleType
{
    Bottle,
    Stool,
    Food,
    Heavy,
    Default
}
public struct SpawnConstants
{
    public const int INITIAL_BOTTLE_LIMIT = 3;
    public const int INITIAL_FOOD_LIMIT = 1;
    public const int INITIAL_STOOL_LIMIT = 1;
    public const int INITIAL_HEAVY_LIMIT = 1;

    public const float BOTTLE_SPAWN_COOLDOWN = 2f;
    public const float FOOD_SPAWN_COOLDOWN = 5f;
    public const float STOOL_SPAWN_COOLDOWN = 20f;
    public const float HEAVY_SPAWN_COOLDOWN = 60f;

    /// <summary>
    /// Angle Range Away from spawn direction vector (in Deg)
    /// </summary>
    public const float LAUNCH_ANGLE_VARIANCE = 15;
    /// <summary>
    /// Speed Variance added on top of base speed
    /// </summary>
    public const float LAUNCH_SPEED_VARIANCE = 8;
    /// <summary>
    /// Initial Launch speed
    /// </summary>
    public const float LAUNCH_BASE_SPEED = 25;
    /// <summary>
    /// Random applied torque on spawn
    /// </summary>
    public const float LAUNCH_BASE_TORQUE = 20;
}
