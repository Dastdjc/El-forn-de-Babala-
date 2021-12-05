using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinijuegoDestroyer : MonoBehaviour
{
    public MinijuegoBee minijuego;

    public void DestroyMusicalBees() 
    {
        foreach (GameObject minijueg in GameObject.FindGameObjectsWithTag("Minijuego")) 
        {
            if (minijueg.name == "Musical Bees")
                minijuego = minijueg.GetComponent<MinijuegoBee>();
        } //FindGameObjectsWithTag("Minijuego");
        GameObject pantallaFinal = GameObject.Find("PantallaFinalMinijuegoMB");
        Animator anim_pantallaFinal = pantallaFinal.GetComponent<Animator>();
        anim_pantallaFinal.SetTrigger("desaparicion");
        Invoke("Salir", 1f);

    }
    public void DestroyFallingFruits()
    {
        foreach (GameObject minijueg in GameObject.FindGameObjectsWithTag("Minijuego"))
        {
            if (minijueg.name == "Falling Fruits")
                minijuego = minijueg.GetComponent<MinijuegoBee>();
        } //FindGameObjectsWithTag("Minijuego");
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
        minijuego.Player.GetComponent<PlayerMovement>().enabled = true;
        minijuego.BGmusic.Play();
        minijuego.BGmusic.volume = 1;
    }
}
