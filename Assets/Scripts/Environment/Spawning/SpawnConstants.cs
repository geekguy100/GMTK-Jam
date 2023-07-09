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
    public const int INITIAL_BOTTLE_LIMIT = 5;
    public const int INITIAL_FOOD_LIMIT = 1;
    public const int INITIAL_STOOL_LIMIT = 1;
    public const int INITIAL_HEAVY_LIMIT = 0;

    public const float BOTTLE_SPAWN_COOLDOWN = 1f;
    public const float FOOD_SPAWN_COOLDOWN = 10f;
    public const float STOOL_SPAWN_COOLDOWN = 20f;
    public const float HEAVY_SPAWN_COOLDOWN = 60f;

    /// <summary>
    /// Angle Range Away from spawn direction vector (in Deg)
    /// </summary>
    public const float LAUNCH_ANGLE_VARIANCE = 10;
    /// <summary>
    /// Speed Variance added on top of base speed
    /// </summary>
    public const float LAUNCH_SPEED_VARIANCE = 10;
    /// <summary>
    /// Initial Launch speed
    /// </summary>
    public const float LAUNCH_BASE_SPEED = 25;
    /// <summary>
    /// Random applied torque on spawn
    /// </summary>
    public const float LAUNCH_BASE_TORQUE = 50;
    /// <summary>
    /// Applies how far up the screen is vertically;
    /// </summary>
    public const float LAUNCH_SPAWN_HEIGHT = .75f;
    /// <summary>
    /// Applies how far the spawn is away from the screen horizontally.
    /// </summary>
    public const float LAUNCH_SPAWN_OFFSET = 1f;
}
