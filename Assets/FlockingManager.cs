using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockingManager : MonoBehaviour {
    //delcare game objects
    public GameObject prefab;
    public GameObject targetPrefab;

    //variables are static so that they can be called in other scripts

    //how many AI will be in the scene
    static int numberOfObjects = 300;
    //creating an array and setting its size to the amount of AI that will spawn
    public static GameObject[] prefabsToSpawn = new GameObject[numberOfObjects];

    //setting the area size
    public static float worldBoundaries = 50;

    //setting original tagert to zero
    public static Vector3 target = Vector3.zero;

    void Awake()
    {
        //prefab = GameObject.FindGameObjectWithTag("AI");
    }
    // Use this for initialization
    void Start ()
    {
        //checking if all Ai have been spawned in the scene
        for (int i = 0; i < numberOfObjects; i++)
        {
            //setting a random position to spawn at within the boundaries
            Vector3 spawnPositon = new Vector3(Random.Range(-worldBoundaries, worldBoundaries), Random.Range(-worldBoundaries, worldBoundaries), Random.Range(-worldBoundaries, worldBoundaries));
            //spawn the AI prefab
            prefabsToSpawn[i] = Instantiate(prefab, spawnPositon, Quaternion.identity);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        //checking if random interger between 0 and 10000 is below 50
        //used to randomly pick a new target postion
        if (Random.Range(0,10000) < 50)
        {
            //set a new target within the boundaries
            target = new Vector3(Random.Range(-worldBoundaries, worldBoundaries), Random.Range(-worldBoundaries, worldBoundaries), Random.Range(-worldBoundaries, worldBoundaries));

            //move target prefab to new target position
            targetPrefab.transform.position = target;
        }
    }
}
