using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowUIinteraction : MonoBehaviour
{
    public GameObject oldScreen;
    public GameObject newScreen;

    public void OnClick() 
    {
        newScreen.SetActive(true);
        oldScreen.SetActive(false);
    }
}
