using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Table : MonoBehaviour
{
    [SerializeField] private float tiltCooldown;
    [SerializeField] private float tiltAngle;
    [SerializeField] private float tiltForce;

    private float clockwiseCoolDown;
    private float counterClockwiseCoodDown;
    private Vector2 originalPos;
    private Rigidbody2D rigidBody;

    private const float TABLE_ROTATE_SPEED = .25f;
    private const float TABLE_LIFT_MODIFIER = .1f;

    private void Start()
    { 
        rigidBody = GetComponent<Rigidbody2D>();
        originalPos = rigidBody.position;
    }

    private void Update()
    {
        if(clockwiseCoolDown > 0) { clockwiseCoolDown -= Time.deltaTime; }
        if(counterClockwiseCoodDown > 0) { counterClockwiseCoodDown -= Time.deltaTime; }

        float input = Input.GetAxisRaw("Horizontal");
        
        if (input < 0 && clockwiseCoolDown <=0)
        {
            clockwiseCoolDown = tiltCooldown;
            counterClockwiseCoodDown = tiltCooldown;
            StartCoroutine(TiltClockwise());
        }
        else if (input > 0 && counterClockwiseCoodDown <= 0)
        {
            clockwiseCoolDown = tiltCooldown;
            counterClockwiseCoodDown = tiltCooldown;
            StartCoroutine(TiltCounterClockwise());
        }

        
    }

    IEnumerator TiltClockwise()
    {
        float sec = 0;
        //tilt up
        while (sec < .25f)
        {
            rigidBody.MoveRotation(Mathf.Lerp(rigidBody.rotation, -tiltAngle, TABLE_ROTATE_SPEED));
            rigidBody.MovePosition(Vector2.Lerp(
                rigidBody.position, 
                new Vector2(originalPos.x, originalPos.y + tiltAngle * TABLE_LIFT_MODIFIER),
                TABLE_ROTATE_SPEED));
            sec += Time.deltaTime;
            yield return null;
        }

        //return to original state
        while (sec < .5f)
        {
            rigidBody.MoveRotation(Mathf.Lerp(rigidBody.rotation, 0, TABLE_ROTATE_SPEED));
            rigidBody.MovePosition(Vector2.Lerp(
                rigidBody.position,
                new Vector2(originalPos.x, originalPos.y),
                TABLE_ROTATE_SPEED));
            sec += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator TiltCounterClockwise()
    {
        float sec = 0;
        //tilt up
        while (sec < .25f)
        {
            rigidBody.MoveRotation(Mathf.Lerp(rigidBody.rotation, tiltAngle, TABLE_ROTATE_SPEED));
            rigidBody.MovePosition(Vector2.Lerp(
                rigidBody.position,
                new Vector2(originalPos.x, originalPos.y + tiltAngle * TABLE_LIFT_MODIFIER),
                TABLE_ROTATE_SPEED));
            sec += Time.deltaTime;
            yield return null;
        }

        //return to original state
        while(sec < .5f)
        {
            rigidBody.MoveRotation(Mathf.Lerp(rigidBody.rotation, 0, TABLE_ROTATE_SPEED));
            rigidBody.MovePosition(Vector2.Lerp(
                rigidBody.position,
                new Vector2(originalPos.x, originalPos.y),
                TABLE_ROTATE_SPEED));
            sec += Time.deltaTime;
            yield return null;
        }
    }
}
