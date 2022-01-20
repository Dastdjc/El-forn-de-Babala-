using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
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
        
        if (DONDESEA == true && Input.GetKeyDown(KeyCode.E))
        {
            
            if(currentTask == null)
            {
                // Permitir volver al pueblo una vez juguemos un minijuego
                if (GameManager.Instance.state == GameManager.GameState.Tutorial) {
                    GameManager.Instance.UpdateGameState(GameManager.GameState.TutorialCocina);
                }
                camara.GetComponent<CinemachineVirtualCamera>().Priority = 11;
                Player.GetComponent<PlayerMovement>().enabled = false;
                Player.GetComponent<Animator>().SetFloat("speed", 0f);
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
