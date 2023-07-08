using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField] private int bottleCount;
    [SerializeField] private int stoolCount;
    [SerializeField] private float spawnFrequency;
    [SerializeField] private List<ObstacleSpawner> obstacleSpawners;

    private float timeElapsed;

    private Dictionary<ObstacleType, int> obstacleStorage = new Dictionary<ObstacleType, int>();

    ///if an object is destroyed put it back in the queue;
    // Start is called before the first frame update
    void Start()
    {
        //Creates a key pair for every ObstacleType
        foreach(string type in System.Enum.GetNames(typeof(ObstacleType)))
        {
            obstacleStorage.Add((ObstacleType)System.Enum.Parse(typeof(ObstacleType), type), 0);
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

    private void SpawnRandomObstacle()
    {
        bool spawned = false;

        //TODO: Check which obstacles need to be spawned in
        //(ObstacleType)Random.Range(0, System.Enum.GetNames(typeof(ObstacleType)).Length)
        obstacleSpawners[Random.Range(0, obstacleSpawners.Count)].SpawnRandomObstacle();
    }
    private void TestSpawnRandom()
    {
        foreach(ObstacleSpawner spawner in obstacleSpawners)
        {
            spawner.SpawnRandomObstacle();
        }
    }
}
