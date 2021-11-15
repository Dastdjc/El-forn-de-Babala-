using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerCesta : MonoBehaviour
{
    private eventSystem cosa;
    private spawnerBerries spawn;

    void Start()
    {
        GameObject cesta = GameObject.Find("EventHandler");
        cosa = cesta.GetComponent<eventSystem>();

        GameObject spawner = GameObject.Find("Spawner");
        spawn = spawner.GetComponent<spawnerBerries>();
    }

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Azul")
        {
            Destroy(collision.gameObject);
            cosa.puntuacion += 30;
            spawn.cantGO -= 1;
        }
        else if (collision.gameObject.tag != "Azul")
        {
            Destroy(collision.gameObject);
            cosa.puntuacion -= 20;
            spawn.cantGO -= 1;
        }
    }
}
