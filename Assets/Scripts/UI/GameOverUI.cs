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
        GameManager.Instance.OnGameWin += OnGameWin;
        GameManager.Instance.OnGameLose += OnGameLose;
    }

    void OnGameWin()
    {
        Debug.Log("Triggering Game Win Screen");
        gameOverAnimator.SetTrigger("GameWin");
    }

    void OnGameLose()
    {
        Debug.Log("Triggering Game Loses Screen");
        gameOverAnimator.SetTrigger("GameLose");
    }

}
