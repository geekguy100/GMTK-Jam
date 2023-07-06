using UnityEngine;

namespace KpattCore.Controls
{
    [CreateAssetMenu(fileName = "New Motor Data", menuName = "Characters/Motor Data", order = 0)]
    public class MotorData : ScriptableObject
    {
        [Header("Movement")]
        public float movementSpeed = 5f;
        public float airControl = 10f;
        
        [Header("Jumping")]
        public bool canJump = true;
        public float jumpHeight = 1f;
        public int maxJumps = 1;
        public float gravity = -9.81f;
    }
}