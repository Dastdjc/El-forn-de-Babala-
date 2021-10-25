using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ControlDialogos : MonoBehaviour
{

    public Animator anim;
    private Queue<string> colaDialogos = new Queue<string>();
    string texto;
    public GameObject[] gameObjecto;
    [SerializeField] TextMeshProUGUI textoPantalla;

    public void ActivarCartel(string textoObjeto)
    {
        textoPantalla.text = "";
        anim.SetBool("Cartel", true);
        texto = textoObjeto;
        
    }

    
    public void ActivaTexto()
    {
        colaDialogos.Clear();
        colaDialogos.Enqueue(texto);
        SiguienteFrase();
    }

    public void SiguienteFrase()
    {
        if (colaDialogos.Count == 0)
        {
            cierraCartel();
            return;
        }
        string fraseActual = colaDialogos.Dequeue();
        textoPantalla.text = fraseActual;
        StartCoroutine(MostrarCaracteres(fraseActual));
    }

    public void cierraCartel()
    {
        anim.SetBool("Cartel", false);
    }

    IEnumerator MostrarCaracteres(string textoAMostrar)
    {
        textoPantalla.text = "";
        foreach (char caracter in textoAMostrar.ToCharArray())
        {
            textoPantalla.text += caracter;
            yield return new WaitForSeconds(0.02f);
        }
    }
}
