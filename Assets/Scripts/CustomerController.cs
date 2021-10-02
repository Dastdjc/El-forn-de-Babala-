using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerController : MonoBehaviour
{
    enum Recetas
    {
        Mona = 0,
        Sandwich  = 1,
        Rosquilletas = 2
    }
    private Recetas[] pedido;
    // Start is called before the first frame update
    void Start()
    {
        pedido = new Recetas[Random.Range(1, 3)];
        for(int i = 0; i < pedido.Length; i++)
        {
            pedido[i] = new Recetas();
            pedido[i] = (Recetas)Random.Range(0, 2);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
