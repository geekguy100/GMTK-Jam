/*********************************
 * Author:          Kyle Grenier
 * Date Created:    07/05
 /********************************/

using KpattCore.Controls;
using UnityEngine;

namespace KpattGames.Movement
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class PlayerMotor2D : MonoBehaviour
    {
        protected Rigidbody2D rb;

        public MotorData MotorData => motorData;
        [SerializeField] protected MotorData motorData;

        protected virtual void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        public abstract void Move(Vector2 input);
        public abstract void Rotate(float delta);
    }
}