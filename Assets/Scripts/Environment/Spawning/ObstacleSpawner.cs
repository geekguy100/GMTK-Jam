using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private Transform objectParent;
    [SerializeField] private Vector2 direction;
    [SerializeField] private float directionVariance;
    [SerializeField] private float launchVariance;
    [SerializeField] private List<Obstacle> obstacles;


    private const float BASE_LAUNCH_FORCE = 20;
    private const float BASE_LAUNCH_TORQUE = 5;
    // Start is called before the first frame update
    void Start()
    {
        //transform = Screen.Wid
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnRandomObstacle()
    {
        Spawn(obstacles[Random.Range(0, obstacles.Count)]);
    }

    private void Spawn(Obstacle obsPrefab)
    {
        Obstacle obs =  Instantiate(obsPrefab, objectParent);
        Rigidbody2D rigidBody = obs.GetComponent<Rigidbody2D>();

        Vector2 dir = Quaternion.AngleAxis(Random.Range(-directionVariance, directionVariance), Vector3.forward) *direction;
        //May need to scale applied forces based on weight
        rigidBody.position = transform.position;
        rigidBody.AddTorque(Random.Range(-BASE_LAUNCH_TORQUE, BASE_LAUNCH_TORQUE));
        rigidBody.AddForce(dir * (BASE_LAUNCH_FORCE + Random.Range(0, launchVariance)));

    }
    
}
