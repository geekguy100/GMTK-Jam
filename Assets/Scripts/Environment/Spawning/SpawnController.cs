using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField] private int bottleCount;
    [SerializeField] private int stoolCount;
    [SerializeField] private int foodCount;
    [SerializeField] private float spawnFrequency;
    [SerializeField] private List<ObstacleSpawner> obstacleSpawners;

    private float timeElapsed;

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
                    obstacleStorage.Add(ObstacleType.Bottle, bottleCount);
                    break;
                case ObstacleType.Stool:
                    obstacleStorage.Add(ObstacleType.Stool, stoolCount);
                    break;
                case ObstacleType.Food:
                    obstacleStorage.Add(ObstacleType.Food, foodCount);
                    break;
                default:
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        if(timeElapsed >= spawnFrequency)
        {
            SpawnRandomObstacle();
            timeElapsed = 0;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            TestSpawnRandom();
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
            if(obstaclePair.Value > 0)
            {
                potentialObstacleSpawns.Add(obstaclePair.Key);
            }
        }

        if (potentialObstacleSpawns.Count > 0)
        {
            ObstacleType obs = potentialObstacleSpawns[Random.Range(0, potentialObstacleSpawns.Count)];
            Obstacle spawnedObs = obstacleSpawners[Random.Range(0, obstacleSpawners.Count)].SpawnObstacleType(obs);
            if(spawnedObs == null) { Debug.Log("Missing obstacle Type: " + obs); return; }
            obstacleStorage[spawnedObs.Type]--;
            spawnedObs.OnObstacleRemove += OnObstacleRemoveListener;
        }
       //obstacleSpawners[Random.Range(0, obstacleSpawners.Count)].SpawnRandomObstacle();
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
