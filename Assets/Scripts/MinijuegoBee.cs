using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinijuegoBee : MonoBehaviour
{
    public GameObject camara;
    public GameObject task;
    public GameObject Texto;

    [HideInInspector]public GameObject currentTask;
    private bool DONDESEA;
    public GameObject Player;
    public GameObject hitbox;
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public AudioSource BGmusic;

    private void Start()
    {
        rb = Player.GetComponent<Rigidbody2D>();
        BGmusic = GameObject.Find("Music").GetComponent<AudioSource>();
    }

    public void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.E) && DONDESEA == true)
        {
            if(currentTask == null)
            {
                hitbox.SetActive(false);
                BGmusic.mute = true;
                currentTask = Instantiate(task, camara.transform);
                rb.bodyType = RigidbodyType2D.Static;
            }
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
