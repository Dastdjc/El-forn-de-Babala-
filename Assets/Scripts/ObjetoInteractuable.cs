using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetoInteractuable : MonoBehaviour
{

    public string textos;
    private textImporter arrayTextos;

    public void Awake()
    {
 
    }

    private void OnMouseDown()
    {
        arrayTextos = FindObjectOfType<textImporter>();
        int n = arrayTextos.textLines.Length;
        textos = arrayTextos.textLines[Random.Range(0, n-1)];
        FindObjectOfType<ControlDialogos>().ActivarCartel(textos);
    }
}
