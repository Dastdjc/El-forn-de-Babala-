using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ControlDialogos : MonoBehaviour
{

    public Animator anim;
    private Queue<string> colaDialogos = new Queue<string>();
    string texto;
    [SerializeField] TextMeshProUGUI textoPantalla;

    public void ActivarCartel(string textoObjeto)
    {
        textoPantalla.text = "";
        anim.SetBool("Cartel", true);
        texto = textoObjeto;
        
    }

    public void Update()
    {
        //Debug.Log(colaDialogos.Count);
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
        Debug.Log(colaDialogos.Count);
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
