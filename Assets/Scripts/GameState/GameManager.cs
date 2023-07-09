using System;
using System.Collections;
using System.Collections.Generic;
using EnemyAI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    
    [Header("Fighters")]
    public Fighter fighter1;
    public Fighter fighter2;


    [Header("Game State")]
    public TimeData timeData;
    [SerializeField] public float timeRemainingSeconds = 0;

    public bool isPaused { get; protected set; } = false;
    public bool isGameStarted {get; protected set; } = false;
    public bool isGameOver { get; protected set; } = false;

    public bool isGameActive { get { return isGameStarted && !isPaused && !isGameOver; } }

    public event Action OnGameStart;
    public event Action OnGameEnd;
    public event Action OnGameWin;
    public event Action OnGameLose;
    public event Action OnGameRestart;


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

    void Start()
    {
        isGameStarted = false;

        // Initialize the time remaining.
        timeRemainingSeconds = timeData.totalGameTimeSeconds;

        // Ensure the game starts with 1.0 time scale.
        timeData.timeScale = 1.0f;
    }

    public void StartGame()
    {
        isGameStarted = true;
        isPaused = false;
        isGameOver = false;

        // Reset the time remaining.
        timeRemainingSeconds = timeData.totalGameTimeSeconds;

        // Start the game.
        OnGameStart?.Invoke();
    }

    public void ResetGame()
    {
        AudioManager.Instance.StopMusicsAudio();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void Update()
    {
        // Bail out if game isn't started yet
        if(!isGameActive)
            return;
        
        HandleGameTime();

        HandleCheckForGameOver();
    }

    void HandleGameTime()
    {
        // If the game is paused or hasn't started, don't update the time.
        if(!isGameActive)
            return;

        // Subtract the time passed from the time remaining.
        timeRemainingSeconds -= Time.deltaTime;

        // Clamp the time remaining so we're never out of the time range.
        timeRemainingSeconds = Mathf.Clamp(timeRemainingSeconds, 0, timeData.totalGameTimeSeconds);
    }

    void HandleCheckForGameOver()
    {
        if(!isGameActive)
            return;

        // Check if time is up
        if(timeRemainingSeconds <= 0)
        {
            // Win!
            isGameOver = true;

            OnGameEnd?.Invoke();
            OnGameWin?.Invoke();
            return;
        }

        // Check if any of figherts are dead
        if(fighter1.GetHealthPercent() <= 0 || fighter2.GetHealthPercent() <= 0)
        {
            // Lose!
            isGameOver = true;

            OnGameEnd?.Invoke();
            OnGameLose?.Invoke();
            return;
        }
    }


}
