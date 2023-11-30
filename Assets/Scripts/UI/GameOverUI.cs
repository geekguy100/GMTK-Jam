using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class GameOverUI : MonoBehaviour
{
    Animator gameOverAnimator => GetComponent<Animator>();

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.OnGameWin.AddListener(OnGameWin);
        GameManager.Instance.OnGameLose.AddListener(OnGameLose);
    }

    private void OnDisable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnGameWin.RemoveListener(OnGameWin);
            GameManager.Instance.OnGameLose.RemoveListener(OnGameLose);
        }
    }

    void OnGameWin()
    {
        Debug.Log("Triggering Game Win Screen");
        gameOverAnimator.SetTrigger("GameWin");
        AudioManager.Instance.PlayGameWin();
    }

    void OnGameLose()
    {
        Debug.Log("Triggering Game Loses Screen");
        gameOverAnimator.SetTrigger("GameLose");
        AudioManager.Instance.PlayGameOver();
    }

}
