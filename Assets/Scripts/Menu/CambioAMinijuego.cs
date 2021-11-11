using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CambioFallingFruits : MonoBehaviour
{

    public GameObject Texto;
    private bool DONDESEA;
    public GameObject Player;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = Player.GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && DONDESEA == true)
        {
            SceneManager.LoadScene("Falling Fruits", LoadSceneMode.Additive);
            rb.bodyType = RigidbodyType2D.Static;


        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Texto.SetActive(true);
            DONDESEA = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Texto.SetActive(false);
            DONDESEA = false;
        }
    }
}
