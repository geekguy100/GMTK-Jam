using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct DestructionConstants
{
    /// <summary>
    /// Objects that fall below this height are killed
    /// </summary>
    public const float MIN_Y_STAGE_BUFFER = -5.5f;
    /// <summary>
    /// Minimum force required to damage an object
    /// </summary>
    public const float DAMAGE_BUFFER = 5f;
    /// <summary>
    /// Debris Damage reduction modifer
    /// </summary>
    public const float DAMAGE_MODIFIER = .2f; 


    /// <summary>
    /// Debris spawning force modifiersf
    /// </summary>
    public const float MIN_X_DESTRUCTION_DEVIATION = -5f;
    public const float MAX_X_DESTRUCTION_DEVIATION = 5f;
    public const float MIN_Y_DESTRUCTION_DEVIATION = 1f;
    public const float MAX_Y_DESTRUCTION_DEVIATION = 3f;
    public const float MIN_DESTRUCTION_TORQUE = -.5f;
    public const float MAX_DESTRUCTION_TORQUE = .5f;

    
}
