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
    public SpriteRenderer identidicadorRenderer;
    private Material mat;

    private void Start()
    {
        rb = Player.GetComponent<Rigidbody2D>();
        BGmusic = GameObject.Find("BG_Music").GetComponent<AudioSource>();
        mat = identidicadorRenderer.material;
    }

    public void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.E) && DONDESEA == true)
        {
            if(currentTask == null)
            {
                Player.GetComponent<PlayerMovement>().enabled = false;
                hitbox.SetActive(false);
                StartCoroutine(AudioFadeOut.FadeOut(BGmusic, 1f));
                currentTask = Instantiate(task, camara.transform);
                rb.bodyType = RigidbodyType2D.Static;
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            mat.SetFloat("Thickness", 0.03f);
            Texto.SetActive(true);
            DONDESEA = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            mat.SetFloat("Thickness", 0f);
            Texto.SetActive(false);
            DONDESEA = false;
        }
    }
}
