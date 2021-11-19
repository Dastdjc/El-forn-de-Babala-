using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] items;
    public float xMax = 11f;
    public float xMin = -10.5f;

    public float spawnRate;

    private float timeSpawn = 0.0f;

    private int rnd;

    private Vector2 spawn;

    void Update()
    {
        
        if (Time.time > timeSpawn)
        {
            rnd = Random.Range(0, 5);
            spawn.x = Random.Range(xMin, xMax);
            spawn.y = Random.Range(4.5f, 8);
            Instantiate(items[rnd], spawn, Quaternion.identity);
            timeSpawn = Time.time + spawnRate;
        }

        
    }
}
