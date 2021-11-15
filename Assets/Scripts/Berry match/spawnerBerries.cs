using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnerBerries : MonoBehaviour
{
    public GameObject[] items;
    public float xMax = 11f;
    public float xMin = -10.5f;
    public float yMin = 1;
    public float yMax = 5;

    public float spawnRate;

    private float timeSpawn = 0.0f;

    private int rnd;

    [HideInInspector] public int cantGO;

    private Vector2 spawn;

    void Update()
    {
        Debug.Log(cantGO);
        if (cantGO < 8)
        {
            if (Time.time > timeSpawn)
            {
                rnd = Random.Range(0, 4);
                spawn.x = Random.Range(xMin, xMax);
                spawn.y = Random.Range(yMin, yMax);
                Instantiate(items[rnd], spawn, Quaternion.identity);
                cantGO += 1;
                timeSpawn = Time.time + spawnRate;
            }
        }

    }
}
