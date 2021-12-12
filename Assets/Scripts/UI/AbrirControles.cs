using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbrirControles : MonoBehaviour
{
    public GameObject controles;
    private Animator UIanimator;
    private Material mat;
    private GameObject player;
    private bool inRange = false;
    // Start is called before the first frame update
    void Start()
    {
        UIanimator = controles.GetComponent<Animator>();
        mat = this.gameObject.GetComponent<SpriteRenderer>().material;
        player = GameObject.Find("Dore_player");
    }
    private void Update()
    {
        if (inRange && Input.GetKeyDown(KeyCode.E)) 
        {
            UIanimator.SetTrigger("aparicion");
            Invoke("DesactivarPlayer", 0.5f);
        }
    }
    private void DesactivarPlayer() 
    { 
        player.SetActive(false); 
    }

    public void CerrarControles() 
    {
        UIanimator.SetTrigger("desaparicion");
        player.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        mat.SetFloat("Thickness", 0.06f);
        inRange = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        mat.SetFloat("Thickness", 0f);
        inRange = false;
    }
}
