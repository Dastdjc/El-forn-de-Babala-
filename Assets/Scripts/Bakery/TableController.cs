using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableController : MonoBehaviour
{
    static public bool isOnColision { get; private set; }
    public GameObject bowl;
    public GameObject Kiln;
    public void Start(){ isOnColision = false;}
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
                string[] names = { "Mona", "Fartons", "Farinada", "Bunyols de calabaza", "Pilotes de frare", "Flaons", "Coca de llanda", "Pasteles de boniato", "Mocadorà" };
                if(i != -1)
                    Debug.Log(names[i]);
                Kiln.GetComponent<KilnController>().GetToCook(i);
            }
            /*
             * Si está tocando la mesa y ha selecionado algo en el inventario
             * BowlController.MoveContent(int cantidad, int indice)
             */
        }
    }
    static public void PutIngredient(string name)
    {
        int index = 0;
        Debug.Log(name);
        string[] names = { "Harina", "Levadura", "Leche", "Mantequilla", "Azúcar", "Huevos", "Aceite", "Agua", "Limón", "Requesón", "Almendra", "Boniato", "Calabaza" };
        while(names[index] != name) { index++; }
        Debug.Log(names[index]);
        if (isOnColision) { BowlController.MoveContent(1, index); }
    }
}
