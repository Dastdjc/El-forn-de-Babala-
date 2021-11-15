using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerCestaVerde : MonoBehaviour
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
        if (collision.gameObject.tag == "Verde")
        {
            Destroy(collision.gameObject);
            spawn.cantGO -= 1;
            cosa.puntuacion += 30;
        }
        else if (collision.gameObject.tag != "Verde")
        {
            Destroy(collision.gameObject);
            spawn.cantGO -= 1;
            cosa.puntuacion -= 20;
        }
    }
}
