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

    private eventSystem manager;

    [HideInInspector] public int cantGO;

    private Vector2 spawn;
    private void Start()
    {
        manager = GameObject.Find("EventHandler").GetComponent<eventSystem>();
    }

    void Update()
    {
        if (cantGO < 8 && manager.playing)
        {
            if (Time.time > timeSpawn)
            {
                rnd = Random.Range(0, 4);
                spawn.x = Random.Range(xMin, xMax);
                spawn.y = Random.Range(yMin, yMax);
                GameObject clone = Instantiate(items[rnd], spawn, Quaternion.identity, this.transform);
                clone.transform.localScale = new Vector3(3, 3, 1);
                cantGO += 1;
                timeSpawn = Time.time + spawnRate;
            }
        }

    }
}
