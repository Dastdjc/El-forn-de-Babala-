using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerCestaRojo : MonoBehaviour
{
    private eventSystem cosa;
    private spawnerBerries spawn;

    public GameObject particlePrefab;
    public GameObject particleMalPrefab;
    private GameObject particula;
    private GameObject particulaMal;

    public AudioSource sonidoAcertar;
    public AudioSource sonidoFallar;

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
        Destroy(particula);
        Destroy(particulaMal);

        if (collision.gameObject.tag == "Rojo")
        {
            particula = Instantiate(particlePrefab, gameObject.transform.position, Quaternion.identity);
            particula.transform.parent = gameObject.transform;
            sonidoAcertar.Play();
            Destroy(collision.gameObject);
            cosa.puntuacion += 30;
            spawn.cantGO -= 1;
        }
        else if (collision.gameObject.tag != "Rojo")
        {
            particulaMal = Instantiate(particleMalPrefab, gameObject.transform.position, Quaternion.identity);
            particulaMal.transform.parent = gameObject.transform;
            sonidoFallar.Play();
            Destroy(collision.gameObject);
            cosa.puntuacion -= 20;
            spawn.cantGO -= 1;
        }
    }
}
