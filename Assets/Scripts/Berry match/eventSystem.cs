using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class eventSystem : MonoBehaviour
{
    public TextMeshProUGUI textPuntuacion;
    [HideInInspector] public int puntuacion = 0;
    void Start()
    {
        
    }

    void Update()
    {
        if (puntuacion < 0)
        {
            puntuacion = 0;
        }

        textPuntuacion.text = "Puntuacion: " + puntuacion.ToString();
    }
}
