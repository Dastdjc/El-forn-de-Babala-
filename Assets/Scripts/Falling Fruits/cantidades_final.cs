using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class cantidades_final : MonoBehaviour
{

    public TextMeshProUGUI cant_almendra, cant_limon, cant_boniato, cant_calabaza;
    public minijuegoMovement ingredientes;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cant_almendra.text = ingredientes.almendra.ToString();
        cant_boniato.text = ingredientes.boniato.ToString();
        cant_calabaza.text = ingredientes.calabaza.ToString();
        cant_limon.text = ingredientes.limon.ToString();
    }
}
