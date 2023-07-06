/*********************************
 * Author:          Kyle Grenier
 * Date Created:    07/05
 /********************************/

using UnityEngine;

namespace KpattGames.Movement
{
    [RequireComponent(typeof(PlayerMotor2D))]
    public class PlayerMotor2DInput : MonoBehaviour
    {
        private PlayerMotor2D playerMotor;
        private Vector2 input;

        private void Awake()
        {
            playerMotor = GetComponent<PlayerMotor2D>();
        }

        private void Update()
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            input = new Vector2(horizontal, vertical).normalized;
        }

        private void FixedUpdate()
        {
            playerMotor.Move(input);
        }
    }
}