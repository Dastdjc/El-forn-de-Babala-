using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowUIinteraction : MonoBehaviour
{
    public GameObject oldScreen;
    public GameObject newScreen;
    public bool derecha;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && derecha) 
        {
            newScreen.SetActive(true);
            oldScreen.SetActive(false);
        }
        else if (!derecha && Input.GetKeyDown(KeyCode.Q))
        {
            newScreen.SetActive(true);
            oldScreen.SetActive(false);
        }
    }
    public void OnClick() 
    {
        newScreen.SetActive(true);
        oldScreen.SetActive(false);
    }
}
