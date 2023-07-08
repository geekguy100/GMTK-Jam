/*********************************
 * Author:          Kyle Grenier
 * Date Created:    
 /********************************/
using UnityEngine;

namespace KpattGames.Movement
{
    public class SideScrollerMotor : PlayerMotor2D
    {
        public override void Move(Vector2 input)
        {
            Vector2 vel = input * motorData.movementSpeed;
            vel.y = rb.velocity.y;

            rb.velocity = vel;
        }

        public override void Rotate(float delta)
        {
            throw new System.NotImplementedException();
        }
    }
}