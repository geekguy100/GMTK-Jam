using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyedPiece : MonoBehaviour
{
    [SerializeField] private float duration;

    private SpriteRenderer sprite = null;
    private float timeElapsed = 0;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        timeElapsed += Time.deltaTime;
        if(timeElapsed >= duration)
        {
            Destroy(gameObject);
        }
    }


}
