using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinijuegoDestroyer : MonoBehaviour
{
    public MinijuegoBee minijuego;

    public void DestroyMinijuego() 
    {
        MinijuegoBee minijuego = GameObject.FindGameObjectWithTag("Minijuego").GetComponent<MinijuegoBee>(); //FindGameObjectsWithTag("Minijuego");
        GameObject pantallaFinal = GameObject.Find("PantallaFinalMinijuego");
        Animator anim_pantallaFinal = pantallaFinal.GetComponent<Animator>();
        anim_pantallaFinal.SetTrigger("desaparicion");
        Invoke("Salir", 1f);

    }
    void Salir()
    {
        Destroy(minijuego.currentTask);
        //Destroy(minijuego);
        minijuego.hitbox.SetActive(true);
        minijuego.rb.bodyType = RigidbodyType2D.Dynamic;
        minijuego.BGmusic.mute = false;
    }
}
