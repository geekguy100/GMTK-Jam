using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct DestructionConstants
{
    /// <summary>
    /// Minimum force required to damage an object
    /// </summary>
    public const float DAMAGE_BUFFER = 8f;
    /// <summary>
    /// Debris Damage reduction modifer
    /// </summary>
    public const float DAMAGE_MODIFIER = .2f; 


    /// <summary>
    /// Debris spawning force modifiersf
    /// </summary>
    public const float MIN_X_DESTRUCTION_DEVIATION = -5;
    public const float MAX_X_DESTRUCTION_DEVIATION = 5;
    public const float MIN_Y_DESTRUCTION_DEVIATION = 1;
    public const float MAX_Y_DESTRUCTION_DEVIATION = 3;
    public const float MIN_DESTRUCTION_TORQUE = -1f;
    public const float MAX_DESTRUCTION_TORQUE = 1f;

    
}
