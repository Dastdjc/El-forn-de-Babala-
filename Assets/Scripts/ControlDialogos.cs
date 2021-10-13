using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ControlDialogos : MonoBehaviour
{

    public Animator anim;
    private Queue<string> colaDialogos = new Queue<string>();
    Texto texto;
    [SerializeField] TextMeshProUGUI textoPantalla;

    public void ActivarCartel(Texto textoObjeto)
    {
        anim.SetBool("Cartel", true);
        texto = textoObjeto;
    }

    public void ActivaTexto()
    {
        colaDialogos.Clear();
        foreach (string textoGuardado in texto.arrayTexto)
        {
            colaDialogos.Enqueue(textoGuardado);
        }
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
