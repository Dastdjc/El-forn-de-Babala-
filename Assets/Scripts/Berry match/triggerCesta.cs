using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerCesta : MonoBehaviour
{
    private eventSystem cosa;

    void Start()
    {
        GameObject cesta = GameObject.Find("EventHandler");
        cosa = cesta.GetComponent<eventSystem>();
    }

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Azul")
        {
            Destroy(collision.gameObject);
            cosa.puntuacion += 30;
        }
        else if (collision.gameObject.tag != "Azul")
        {
            Destroy(collision.gameObject);
            cosa.puntuacion -= 20;
        }
    }
}
