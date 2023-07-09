/*********************************
 * Author:          Kyle Grenier
 * Date Created:    
 /********************************/

using System;
using UnityEngine;

namespace KpattGames.Movement
{
    public class SideScrollerMotor : PlayerMotor2D
    {
        private Vector2 cachedVel;

        [SerializeField] private float smoothTime;
        [SerializeField] private float groundCheckDistance;
        [SerializeField] private LayerMask whatIsGround;
        
        protected override void PerformMove(Vector2 input)
        {
            // If we are NOT grounded, do not move the player.
            if (!IsGrounded())
                return;
            
            Vector2 targetVel = input * motorData.movementSpeed;
            targetVel.y = rb.velocity.y;
            
            rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVel, ref cachedVel, smoothTime);
        }

        public bool IsGrounded()
        {
            return Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
        }
        
        public override void Rotate(float delta)
        {
            throw new System.NotImplementedException();
        }

        private void OnDrawGizmosSelected()
        {
            Debug.DrawRay(transform.position, Vector2.down * groundCheckDistance, Color.yellow);
        }
    }
}