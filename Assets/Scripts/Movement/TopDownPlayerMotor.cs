/*********************************
 * Author:          Kyle Grenier
 * Date Created:    07/05
 /********************************/

using UnityEngine;

namespace KpattGames.Movement
{
    public class TopDownPlayerMotor : PlayerMotor2D
    {
        protected override void PerformMove(Vector2 input)
        {
            Vector2 velocity = transform.TransformDirection(input) * (motorData.movementSpeed * Time.deltaTime);
            rb.MovePosition(rb.position + velocity);
        }

        public override void Rotate(float delta)
        {
            rb.MoveRotation(delta * Time.deltaTime);
        }
    }
}