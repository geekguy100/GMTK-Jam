/*********************************
 * Author:          Kyle Grenier
 * Date Created:    
 /********************************/
using UnityEngine;

namespace KpattGames.Movement
{
    public class SideScrollerMotor : PlayerMotor2D
    {
        private Vector2 cachedVel;

        [SerializeField] private float smoothTime;
        
        // Prevent movement in air
        protected override void PerformMove(Vector2 input)
        {
            Vector2 targetVel = input * motorData.movementSpeed;
            targetVel.y = rb.velocity.y;
            
            rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVel, ref cachedVel, smoothTime);
        }

        public override void Rotate(float delta)
        {
            throw new System.NotImplementedException();
        }
    }
}