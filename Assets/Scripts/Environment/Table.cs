using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using KpattGames.Movement;

public class Table : MonoBehaviour
{
    [SerializeField] private float tiltCooldown;
    [SerializeField] private float tiltAngle;
    [SerializeField] private float tiltForce;

    [SerializeField] private SideScrollerMotor fighterOne;
    [SerializeField] private SideScrollerMotor fighterTwo;
    [SerializeField] private float pushAwayForce;

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
            PushFighters();
            StartCoroutine(TiltClockwise());
        }
        else if (input > 0 && counterClockwiseCoodDown <= 0)
        {
            clockwiseCoolDown = tiltCooldown;
            counterClockwiseCoodDown = tiltCooldown;
            PushFighters();
            StartCoroutine(TiltCounterClockwise());
        }
    }

    private void PushFighters()
    {
        Rigidbody2D fighterOneRb = fighterOne.Rigidbody;
        Rigidbody2D fighterTwoRb = fighterTwo.Rigidbody;
        Vector2 fighterOnePos = fighterOneRb.position;
        Vector2 fighterTwoPos = fighterTwoRb.position;

        Vector2 forceOrigin = (fighterOnePos + fighterTwoPos) / 2;

        if (fighterOne.IsGrounded())
        {
            Vector2 fighterOneForward = (fighterTwoPos - fighterOnePos).normalized;
            fighterOneForward.y = 0f;
            
            fighterOneRb.AddForceAtPosition(-fighterOneForward * pushAwayForce, forceOrigin, ForceMode2D.Impulse);
        }

        if (fighterTwo.IsGrounded())
        {
            Vector2 fighterTwoForward = (fighterOnePos - fighterTwoPos).normalized;
            fighterTwoForward.y = 0;
            
            fighterTwoRb.AddForceAtPosition(-fighterTwoForward * pushAwayForce, forceOrigin, ForceMode2D.Impulse);
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
