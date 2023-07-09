/*********************************
 * Author:          Kyle Grenier
 * Date Created:    07/05
 /********************************/

using System.Collections;
using KpattCore.Controls;
using UnityEngine;

namespace KpattGames.Movement
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class PlayerMotor2D : MonoBehaviour
    {
        protected Rigidbody2D rb;
        public Rigidbody2D Rigidbody => rb;

        private bool active;

        public MotorData MotorData => motorData;
        [SerializeField] protected MotorData motorData;
        [SerializeField] private float activationTime = 1f;

        protected virtual void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            active = true;
        }

        public void Move(Vector2 input)
        {
            if (!active)
                return;
            
            PerformMove(input);
        }

        public void Deactivate(bool autoRecovery = true)
        {
            active = false;
            
            if (autoRecovery)
            {
                StartCoroutine(HandleActivation());
            }
            
            IEnumerator HandleActivation()
            {
                yield return new WaitForSeconds(activationTime);
                Activate();
            }
        }

        public void Activate()
        {
            active = true;
        }

        protected abstract void PerformMove(Vector2 input);
        public abstract void Rotate(float delta);
    }
}