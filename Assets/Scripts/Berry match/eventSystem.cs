using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class eventSystem : MonoBehaviour
{
    public TextMeshProUGUI textPuntuacion;
    public TextMeshProUGUI textoGanar;
    [HideInInspector] public int puntuacion = 0;
    public GameObject spawner;
    public Button botonSalir;

    private bool finish = false;

    private float startTime;

    void Start()
    {
        startTime = Time.time;
    }

    void Update()
    {

        if (!finish)
        {
            float t = Time.time - startTime;
            
            if (puntuacion < 0)
            {
                puntuacion = 0;
            }

            textPuntuacion.text = "Puntuacion: " + puntuacion.ToString();
            if (t > 30.00)
            {
                finish = true;
            }
        }
        else
        {
            Acabar();
        }
        
    }

    private void Acabar()
    {
        textPuntuacion.gameObject.SetActive(false);
        spawner.SetActive(false);
        Destroy(GameObject.FindWithTag("Azul"));
        Destroy(GameObject.FindWithTag("Negra"));
        Destroy(GameObject.FindWithTag("Verde"));
        Destroy(GameObject.FindWithTag("Rojo"));
        
        textoGanar.gameObject.SetActive(true);
        botonSalir.gameObject.SetActive(true);
        textoGanar.text = "Tu puntuación final es: " + puntuacion.ToString();
    }
}
