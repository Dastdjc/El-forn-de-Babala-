using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cables_minijuego : MonoBehaviour
{
    public int conexionesActuales;
    // Start is called before the first frame update
    public void ComprovarVictoria()
    {
        if (conexionesActuales == 4)
        {
            Destroy(this.gameObject);
        }
    }
}
