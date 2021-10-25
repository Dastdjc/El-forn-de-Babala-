using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] items;

    public float spawnRate;

    private float timeSpawn = 0.0f;

    private int rnd;

    private Vector2 spawn;

    void Update()
    {
        
        if (Time.time > timeSpawn)
        {
            rnd = Random.Range(0, 3);
            spawn.x = Random.Range(-5.65f, 12);
            spawn.y = Random.Range(4.5f, 8);
            Instantiate(items[rnd], spawn, Quaternion.identity);
            timeSpawn = Time.time + spawnRate;
        }

        
    }
}
