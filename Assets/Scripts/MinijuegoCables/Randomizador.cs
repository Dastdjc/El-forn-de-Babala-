using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Randomizador : MonoBehaviour
{

    private void Awake()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            GameObject cableActual = transform.GetChild(i).gameObject;
            GameObject otroCable = transform.GetChild(Random.Range(0, transform.childCount)).gameObject;
        }
    }
}
