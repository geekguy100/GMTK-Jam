using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DestroyedPiece : MonoBehaviour
{
    [SerializeField] private float duration;

    private SpriteRenderer sprite = null;
    private float timeElapsed = 0;
    private bool startFadeOut = false;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        timeElapsed += Time.deltaTime;
        if(timeElapsed >= duration && !startFadeOut)
        {
            startFadeOut = true;
            FadeOut();
        }
    }

    private void FadeOut()
    {
        sprite.DOFade(0, .5f).OnComplete(delegate { Destroy(gameObject); });
    }
}
