using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField] private float spawnFrequency;
    [SerializeField] private List<ObstacleSpawner> obstacleSpawners;

    private float timeElapsed;


    /// <summary>
    /// Used to delay spawning objects until the game starts
    /// </summary>
    public bool isGameActive => GameManager.Instance.isGameActive;

    private Dictionary<ObstacleType, float> obstacleTimers = new Dictionary<ObstacleType, float>();
    private Dictionary<ObstacleType, int> obstacleStorage = new Dictionary<ObstacleType, int>();

    ///if an object is destroyed put it back in the queue;
    // Start is called before the first frame update
    void Start()
    {
        //Creates a key pair for every ObstacleType
        string[] obstacleTypeNames = System.Enum.GetNames(typeof(ObstacleType));
        foreach(string type in obstacleTypeNames)
        {
            switch((ObstacleType)System.Enum.Parse(typeof(ObstacleType), type))
            {
                case ObstacleType.Bottle:
                    obstacleStorage.Add(ObstacleType.Bottle, SpawnConstants.INITIAL_BOTTLE_LIMIT);
                    obstacleTimers.Add(ObstacleType.Bottle, SpawnConstants.BOTTLE_SPAWN_COOLDOWN);
                    break;
                case ObstacleType.Stool:
                    obstacleStorage.Add(ObstacleType.Stool, SpawnConstants.INITIAL_STOOL_LIMIT);
                    obstacleTimers.Add(ObstacleType.Stool, SpawnConstants.STOOL_SPAWN_COOLDOWN);
                    break;
                case ObstacleType.Food:
                    obstacleStorage.Add(ObstacleType.Food, SpawnConstants.INITIAL_FOOD_LIMIT);
                    obstacleTimers.Add(ObstacleType.Food, SpawnConstants.FOOD_SPAWN_COOLDOWN);
                    break;
                case ObstacleType.Heavy:
                    obstacleStorage.Add(ObstacleType.Heavy, SpawnConstants.INITIAL_HEAVY_LIMIT);
                    obstacleTimers.Add(ObstacleType.Heavy, SpawnConstants.HEAVY_SPAWN_COOLDOWN);
                    break;
                default:
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Never spawn objects if the game is not active
        if (!isGameActive)
            return;

        CalculateTime();
        if(timeElapsed >= spawnFrequency)
        {
            SpawnRandomObstacle();
            timeElapsed = 0;
        }
        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TestSpawnRandom();
        }
        */
    }

    /// <summary>
    /// Handles individual spawning cooldowns
    /// </summary>
    private void CalculateTime()
    {
        timeElapsed += Time.deltaTime;
        if(obstacleTimers[ObstacleType.Bottle] > 0)
        {
            obstacleTimers[ObstacleType.Bottle] -= Time.deltaTime;
        }

        if (obstacleTimers[ObstacleType.Food] > 0)
        {
            obstacleTimers[ObstacleType.Food] -= Time.deltaTime;
        }

        if (obstacleTimers[ObstacleType.Stool] > 0)
        {
            obstacleTimers[ObstacleType.Stool] -= Time.deltaTime;
        }

        if (obstacleTimers[ObstacleType.Heavy] > 0)
        {
            obstacleTimers[ObstacleType.Heavy] -= Time.deltaTime;
            Debug.Log("HEAVY TIMER: " + obstacleTimers[ObstacleType.Heavy]);
        }
    }

    /// <summary>
    /// Spawns a random obstacle while keeping staying within their respective limit counts
    /// </summary>
    List<ObstacleType> potentialObstacleSpawns = new List<ObstacleType>();
    private void SpawnRandomObstacle()
    {
        potentialObstacleSpawns.Clear();

        foreach(KeyValuePair<ObstacleType,int> obstaclePair in obstacleStorage)
        {
            if(obstaclePair.Value > 0 && obstacleTimers[obstaclePair.Key] <= 0)
            {
                Debug.Log(obstaclePair.Key + " -- " + obstacleTimers[obstaclePair.Key]);
                potentialObstacleSpawns.Add(obstaclePair.Key);
            }
        }

        if (potentialObstacleSpawns.Count > 0)
        {
            ObstacleType obs = potentialObstacleSpawns[Random.Range(0, potentialObstacleSpawns.Count)];
            Obstacle spawnedObs = obstacleSpawners[Random.Range(0, obstacleSpawners.Count)].SpawnObstacleType(obs);
            if(spawnedObs == null) { Debug.Log("Missing obstacle Type: " + obs); return; }
            obstacleStorage[spawnedObs.Type]--;
            obstacleTimers[spawnedObs.Type] = GetSpawnCooldownConstant(spawnedObs.Type);
            spawnedObs.OnObstacleRemove += OnObstacleRemoveListener;
            
        }
       //obstacleSpawners[Random.Range(0, obstacleSpawners.Count)].SpawnRandomObstacle();
    }

    private float GetSpawnCooldownConstant(ObstacleType type)
    {
        switch (type)
        {
            case ObstacleType.Bottle:
                return SpawnConstants.BOTTLE_SPAWN_COOLDOWN;
            case ObstacleType.Stool:
                return SpawnConstants.STOOL_SPAWN_COOLDOWN;
            case ObstacleType.Food:
                return SpawnConstants.FOOD_SPAWN_COOLDOWN;
            case ObstacleType.Heavy:
                return SpawnConstants.HEAVY_SPAWN_COOLDOWN;
            default:
                return 0;
        }
    }
    /// <summary>
    /// Handles when an obstacle in the scene is removed
    /// </summary>
    /// <param name="obs"></param>
    private void OnObstacleRemoveListener(Obstacle obs)
    {
        obstacleStorage[obs.Type]++;
    }
    private void TestSpawnRandom()
    {
        foreach(ObstacleSpawner spawner in obstacleSpawners)
        {
            spawner.SpawnRandomObstacle();
        }
    }
}
