using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetoInteractuable : MonoBehaviour
{

    public string textos;
    private textImporter arrayTextos;
    private int n = 0;
    private void OnMouseDown()
    {
        textos = "";
        arrayTextos = FindObjectOfType<textImporter>();
        n = arrayTextos.textLines.Length;
        textos = arrayTextos.textLines[Random.Range(0, n-1)];
        FindObjectOfType<ControlDialogos>().ActivarCartel(textos);
    }
}
