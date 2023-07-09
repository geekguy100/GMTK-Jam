/*********************************
 * Author:          Kyle Grenier
 * Date Created:    
 /********************************/
using UnityEngine;

public static class TransformHelper
{
    /// <summary>
    /// Returns true if Transform objToCheck is behind fighterOne.
    /// </summary>
    /// <param name="fighterOne">The first fighter, and the one to check the object with.</param>
    /// <param name="fighterTwo">The second fighter.</param>
    /// <param name="objToCheck">The Transform to check is behind fighter one.</param>
    /// <returns>True if the objToCheck is behind fighter one.</returns>
    public static bool IsObjectBehind(Transform fighterOne, Transform fighterTwo, Transform objToCheck)
    {
        Vector2 fighterOneFacingDir = (fighterTwo.position - fighterOne.position).normalized;
        fighterOneFacingDir.y = 0f;

        Vector2 objPos = objToCheck.position;
        objPos.y = 0;

        Vector2 fighterOneToObjDir = (objPos - fighterOneFacingDir).normalized;

        return (int)Mathf.Sign(fighterOneToObjDir.x) != (int)Mathf.Sign(objPos.x);
    }
}