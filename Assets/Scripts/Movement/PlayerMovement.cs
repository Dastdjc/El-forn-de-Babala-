using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D rb;
    private bool isDashing = false;
    private GameObject dashParticles;
    private SpriteRenderer sr;
    private DialogueManager dm;
    private Vector3 idleScale;

    private CinemachineImpulseSource cameraImpulse;

    private AudioSource dash_body;
    private AudioSource dash_init;
    private AudioSource dash_end;
    private AudioSource dash_moving;

    private bool dashMovingPlayed = false;

    private AudioSource stepSource;
    private float stepTimer;

    public float speed = 5;
    public float dashSpeed = 10;
    public Animator animator;
    public GameObject idle_anim;
    public AudioClip[] steps;
    public float stepTime;

    public Transform shadow;
 
    void Start()
    {
        dm = GameObject.Find("/Dialogue/DialogueManager").GetComponent<DialogueManager>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        dashParticles = GameObject.Find("Dore_player/DashParticles");
        dashParticles.SetActive(false);
        idleScale = new Vector3(1, 1, 1);

        sr = gameObject.GetComponent<SpriteRenderer>();
        cameraImpulse = this.GetComponent<CinemachineImpulseSource>();

        dash_init = GameObject.Find("Dash_init").GetComponent<AudioSource>();
        dash_body = GameObject.Find("Dash_body").GetComponent<AudioSource>();
        dash_end = GameObject.Find("Dash_end").GetComponent<AudioSource>();
        dash_moving = GameObject.Find("Dash_moving").GetComponent<AudioSource>();

        stepSource = GameObject.Find("Step").GetComponent<AudioSource>();
        stepTimer = stepTime;

        // inital position in the scene
        if (GameManager.Instance.state != GameManager.GameState.InicioJuego && GameManager.Instance.state != GameManager.GameState.Bosque && GameManager.Instance.state != GameManager.GameState.CinematicaFinal)
        {
            Debug.Log(GameManager.Instance.playerSpawnPosition);
            transform.localPosition = GameManager.Instance.playerSpawnPosition;
        }
            //transform.position = GameManager.Instance.playerSpawnPosition;
    }

    void Update()
    {
        if (!dm.inConversation) 
        {
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");
            // La dirección en sí no la magnitud, para el dash
            float xRaw = Input.GetAxisRaw("Horizontal");
            Vector2 dir = new Vector2(x, y);

           
            if (!isDashing)
            {
                this.transform.localScale = new Vector3(1.5f, 1.5f, 1);
                animator.SetFloat("speed", Mathf.Abs(dir.x));
                idle_anim.SetActive(true);

                stepTimer -= Time.deltaTime;
                if (Mathf.Abs(dir.x) != 0 && stepTimer <= 0) 
                {
                    stepSource.clip = steps[Random.Range(0, steps.Length)];
                    stepSource.Play();
                    stepTimer = stepTime;
                }
            }

            // Flip del sprite de Dore
            if (dir.x < 0)
            {
                idleScale.x = -1;
                idle_anim.SetActive(false);
                //transform.localScale = new Vector3(-1, 1, 1);
                sr.flipX = true;
                
            }
            else if (dir.x > 0)
            {
                idleScale.x = 1;
                idle_anim.SetActive(false);
                //transform.localScale = new Vector3(1, 1, 1);
                sr.flipX = false;
                
            }

            // Sonido del dash
            if (isDashing && Mathf.Abs(dir.x) != 0) // Si no se mueve y está dashing
            {
                if (isDashing && !dashMovingPlayed)
                {
                    dash_moving.Play();
                    dashMovingPlayed = true;
                }

            }
            else 
            {
                dash_moving.Stop();
                dashMovingPlayed = false;
            }

            idle_anim.transform.localScale = idleScale;

            // Lógica del movimiento

            Walk(dir);

            // Lógica del Dash
            if (Input.GetKey(KeyCode.LeftShift))
            {
                shadow.gameObject.SetActive(false);
                idle_anim.SetActive(false);
                animator.SetFloat("speed", 1f);
                animator.SetBool("isDashing", true);
                dashParticles.SetActive(true);
                Dash(xRaw);
            }
            else if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                shadow.gameObject.SetActive(true);
                rb.drag = 14;
                StartCoroutine("DashWait");

                dash_body.Stop();
                dash_moving.Stop();
                dash_end.Play();
            }
        }
        // Si está en conversación
        else
        {
            this.transform.localScale = new Vector3(1.5f, 1.5f, 1);
            idle_anim.transform.localScale = idleScale;
            rb.velocity = new Vector2(0f, 0f);
            animator.SetBool("isDashing", false);
            idle_anim.SetActive(true);
            animator.SetFloat("speed", 0);

            dash_body.Stop();
            dash_moving.Stop();
            dash_end.Stop();
        }
    }

    private void Walk(Vector2 dir) 
    {
        rb.velocity = (new Vector2(dir.x * speed, rb.velocity.y));
    }

    private void Dash(float x) 
    {
        if (!isDashing)
        {
            dash_init.Play();
            dash_body.Play();
        }
        
        isDashing = true;
        rb.velocity = Vector2.zero;
        Vector2 dash = new Vector2(x, 0);
        
        //rb.drag = 14;
        rb.velocity += dash.normalized * dashSpeed;
        rb.gravityScale = 0;

        cameraImpulse.GenerateImpulse();
        //CameraShake.Instance.ShakeCamera(5f, 0.1f);
       
        //StartCoroutine("DashWait"); // Parecido a un timer
    }

    IEnumerator DashWait() // Función que no se ejecuta en cada frame
    {
        //for (float i = 6; i >= 0; i--)
        //{
        //    rb.drag -= 1;
        //    yield return new WaitForSeconds(.05f);  // Tiempo que se espera en cada frame para volver a la ejecución de la función
        //}

        for (float i = 6; i >= 0; i--)
        {
            rb.drag -= 1;
            yield return new WaitForSeconds(.05f);  // Tiempo que se espera en cada frame para volver a la ejecución de la función
        }

        isDashing = false;

        rb.gravityScale = 1f;
        rb.drag = 0;

        //Debug.Log("STOP dash");
        // Deactivate Dash
        animator.SetBool("isDashing", false);
        dashParticles.SetActive(false);
    }
    public Rigidbody2D GetRB() 
    {
        return rb;
    }
}
