using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Obstacle : EnvironmentObject
{
    [SerializeField] private List<DestroyedPiece> destructionAssets;
    [SerializeField] private ObstacleType type;
    [SerializeField] private HazardForceMultiplierContainer forceMultiplierContainer;
    [SerializeField] private Slider slider;

    protected override void Update()
    {
        base.Update();
        if (!ReferenceEquals(slider, null))
            slider.value = GetHealthPercent();
    }

    public event Action<Obstacle> OnObstacleRemove;
    public ObstacleType Type => type;
    public override void OnRemove()
    {
        DestroyedPiece piece;
        Rigidbody2D rigidBody;
        foreach(DestroyedPiece piecePrefab in destructionAssets)
        {
            //piece.gameObject.SetActive(true);
            piece = Instantiate(piecePrefab,transform.parent);
            piece.transform.position = transform.position;
            rigidBody = piece.GetComponent<Rigidbody2D>();
            //can multiple force range by collision speed
            rigidBody.velocity = new Vector2(
                Random.Range(DestructionConstants.MIN_X_DESTRUCTION_DEVIATION, DestructionConstants.MAX_X_DESTRUCTION_DEVIATION),
                Random.Range(DestructionConstants.MIN_Y_DESTRUCTION_DEVIATION, DestructionConstants.MAX_Y_DESTRUCTION_DEVIATION)
                );
            /*
            rigidBody.AddForce(new Vector2(
                Random.Range(DestructionConstants.MIN_X_DESTRUCTION_DEVIATION, DestructionConstants.MAX_X_DESTRUCTION_DEVIATION), 
                Random.Range(DestructionConstants.MIN_Y_DESTRUCTION_DEVIATION, DestructionConstants.MAX_Y_DESTRUCTION_DEVIATION))
                );
            */
            rigidBody.AddTorque(Random.Range(DestructionConstants.MIN_DESTRUCTION_TORQUE, DestructionConstants.MAX_DESTRUCTION_TORQUE));
        }

        OnObstacleRemove?.Invoke(this);
        base.OnRemove();
    }
}
