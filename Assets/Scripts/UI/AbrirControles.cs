using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbrirControles : MonoBehaviour
{
    public GameObject controles;
    public GameObject text;
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
        DesactivarControles();
    }
    private void Update()
    {
        if (inRange && Input.GetKeyDown(KeyCode.E) && !controles.active) 
        {
            controles.SetActive(true);
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
        Invoke("DesactivarControles", 0.5f);
    }
    private void DesactivarControles() 
    {
        controles.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        text.SetActive(true);
        mat.SetFloat("Thickness", 0.06f);
        inRange = true;
        transform.localScale = transform.localScale * 1.1f;//new Vector3(1.1f, 1.1f, 1.1f);
        transform.Rotate(new Vector3(0, 0, 5));
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        text.SetActive(false);
        mat.SetFloat("Thickness", 0f);
        inRange = false;
        transform.localScale = transform.localScale * 0.9f;//new Vector3(1f, 1f, 1f);
        transform.Rotate(new Vector3(0, 0, -5));
    }
}
