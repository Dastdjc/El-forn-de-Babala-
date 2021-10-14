using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetoInteractuable : MonoBehaviour
{

    private Texto textos;

    public void Awake()
    {
        textos = "hola";
    }

    private void OnMouseDown()
    {
        FindObjectOfType<ControlDialogos>().ActivarCartel(textos);
    }
}
