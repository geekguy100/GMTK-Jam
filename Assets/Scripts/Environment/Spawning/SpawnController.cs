using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField] private float spawnFrequency;
    [SerializeField] private List<ObstacleSpawner> obstacleSpawners;


    private float timeElapsed;
    // Start is called before the first frame update
    void Start()
    {
        
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
