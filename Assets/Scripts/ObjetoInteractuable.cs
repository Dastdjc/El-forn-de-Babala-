using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetoInteractuable : MonoBehaviour
{

    public Texto textos;

    public void Awake()
    {
        
    }

    private void OnMouseDown()
    {
        FindObjectOfType<ControlDialogos>().ActivarCartel(textos);
    }
}
