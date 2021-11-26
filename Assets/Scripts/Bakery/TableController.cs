using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableController : MonoBehaviour
{
    private bool isOnColision = false;
    public GameObject bowl;
    public GameObject Kiln;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        isOnColision = true;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        isOnColision = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isOnColision)
        {
            if (Input.GetKeyDown(KeyCode.C) && Kiln.GetComponent<KilnController>().ImOpen())
            {
                int i = bowl.GetComponent<BowlController>().DeterminateFood();
                bowl.GetComponent<BowlController>().Resset();
                Kiln.GetComponent<KilnController>().GetToCook(i);
            }
        }
    }
}
