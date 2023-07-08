using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Header("Game State")]
    public TimeData timeData;
    [SerializeField] private float timeRemainingSeconds = 0;
    [SerializeField] private bool isPaused = false;


    /// <summary>
    /// The time scale of the game. Changing this will effect the game simulation and physics time scale.
    /// </summary>
    public float TimeScale
    {
        get 
        {
            return timeData.timeScale;
        }
        set
        {
            timeData.timeScale = value;

            // Set game simulation and physics time scale.
            Time.timeScale = value;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
        }
    }


    void Awake()
    {
        timeRemainingSeconds = timeData.totalGameTimeSeconds;

        // Ensure the game starts with 1.0 time scale.
        timeData.timeScale = 1.0f;
    }

    void Update()
    {
        HandleGameTime();

        HandleCheckForGameOver();
    }

    void HandleGameTime()
    {
        // If the game is paused, don't update the time.
        if(isPaused)
            return;

        timeRemainingSeconds -= Time.deltaTime * timeData.timeScale;
    }

    // TODO
    void HandleCheckForGameOver()
    {
        // Check if time is up
        if(timeRemainingSeconds <= 0)
        {
        }

        // Check if any of figherts are dead
    }


}
