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
    [SerializeField] private bool leftSide;

    public List<Obstacle> bottles = new List<Obstacle>();
    public List<Obstacle> stools = new List<Obstacle>();
    public List<Obstacle> foods = new List<Obstacle>();
    public List<Obstacle> heavys = new List<Obstacle>();

    private const float BASE_LAUNCH_FORCE = 5;
    private const float BASE_LAUNCH_TORQUE = 5;
    // Start is called before the first frame update
    void Start()
    {
        foreach(Obstacle obs in obstacles)
        {
            switch (obs.Type)
            {
                case ObstacleType.Bottle:
                    bottles.Add(obs);
                    break;
                case ObstacleType.Stool:
                    stools.Add(obs);
                    break;
                case ObstacleType.Food:
                    foods.Add(obs);
                    break;
                case ObstacleType.Heavy:
                    heavys.Add(obs);
                    break;
                    
            }
        }
        AssignEdgePosition();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //TODO: Scale launch force based on screen size
    private void AssignEdgePosition()
    {
        if (leftSide)
        {
            transform.position = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height * 3f/4f, 0));
            transform.Translate(new Vector3(-.5f, 0, 0));
        }
        else
        {
            transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height * 3f/4f, 0));
            transform.Translate(new Vector3(.5f, 0, 0));
        }
    }
    public Obstacle SpawnRandomObstacle()
    {
        return SpawnObstacleType((ObstacleType)Random.Range(0, System.Enum.GetNames(typeof(ObstacleType)).Length));
    }

    public Obstacle SpawnObstacleType(ObstacleType type)
    {
        Obstacle obs;
        switch (type)
        {
            case ObstacleType.Bottle:
                if (bottles.Count == 0) { return SpawnObstacleType(ObstacleType.Default); }
                obs = Spawn(bottles[Random.Range(0, bottles.Count)]);
                break;
            case ObstacleType.Stool:
                if(stools.Count == 0) { return SpawnObstacleType(ObstacleType.Default); }
                obs = Spawn(stools[Random.Range(0, stools.Count)]);
                break;
            case ObstacleType.Food:
                if(foods.Count == 0) { return SpawnObstacleType(ObstacleType.Default); }
                obs = Spawn(foods[Random.Range(0, stools.Count)]);
                break;
            case ObstacleType.Heavy:
                if (foods.Count == 0) { return SpawnObstacleType(ObstacleType.Default); }
                obs = Spawn(heavys[Random.RandomRange(0, heavys.Count)]);
                break;
            default:
                //obs = Spawn(obstacles[Random.Range(0, obstacles.Count)]);
                Debug.Log("Attempting to spawn unregistered object type: " + type);
                obs = null;
                break;
        }
        return obs;
    }


    private Obstacle Spawn(Obstacle obsPrefab)
    {
        Obstacle obs =  Instantiate(obsPrefab, objectParent);
        Rigidbody2D rigidBody = obs.GetComponent<Rigidbody2D>();

        Vector2 dir = Quaternion.AngleAxis(Random.Range(-directionVariance, directionVariance), Vector3.forward) *direction;
        //May need to scale applied forces based on weight
        rigidBody.position = transform.position;
        rigidBody.AddTorque(Random.Range(-BASE_LAUNCH_TORQUE, BASE_LAUNCH_TORQUE));
        //rigidBody.AddForce(dir * (BASE_LAUNCH_FORCE + Random.Range(0, launchVariance)));
        rigidBody.velocity = dir * (BASE_LAUNCH_FORCE + Random.Range(0, launchVariance));
        return obs;
    }
    
}
