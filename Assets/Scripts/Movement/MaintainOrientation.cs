/*********************************
 * Author:          Kyle Grenier
 * Date Created:    
 /********************************/

using System;
using System.Collections;
using UnityEngine;

namespace KpattGames.Movement
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class MaintainOrientation : MonoBehaviour
    {
        private Quaternion initialRot;
        private Rigidbody2D rb;

        private float reorientTime;

        [SerializeField] private float reorientWaitTime;
        [SerializeField] private float maxDegDifference;
        [SerializeField][Min(1)] private float forceMultiplier;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            initialRot = transform.localRotation;
        }

        private void FixedUpdate()
        {
            if (GetAngle() > maxDegDifference && (Time.time - reorientTime > reorientWaitTime || reorientTime == 0))
            {
                float signMultiplier = -Mathf.Sign(rb.rotation);
                float impulse = (maxDegDifference * signMultiplier * Mathf.Deg2Rad) * rb.inertia;
                rb.AddTorque(impulse * forceMultiplier, ForceMode2D.Impulse);
                
                reorientTime = Time.time;
            }
        }
        
        private float GetAngle()
        {
            return Quaternion.Angle(transform.localRotation, initialRot);
        }
    }
}