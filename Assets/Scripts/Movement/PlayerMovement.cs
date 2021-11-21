using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D rb;
    private bool isDashing = false;
    private GameObject dashParticles;
    private SpriteRenderer sr;

    public float speed = 5;
    public float dashSpeed = 10;
    public Animator animator;
 
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        dashParticles = GameObject.Find("Dore_player/DashParticles");
        dashParticles.SetActive(false);

        sr = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        // La dirección en sí no la magnitud, para el dash
        float xRaw = Input.GetAxisRaw("Horizontal");
        Vector2 dir = new Vector2(x, y);

        animator.SetFloat("speed", Mathf.Abs(dir.x));
       if (dir.x < 0) {
            transform.localScale = new Vector3(-1, 1, 1);
            sr.flipX = true;
        }
        else if(dir.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            sr.flipX = false;
        }

        // Lógica del movimiento
        if (!isDashing)
        {
            Walk(dir);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing && xRaw != 0) 
        {
            animator.SetBool("isDashing", true);
            dashParticles.SetActive(true);
            Dash(xRaw);
        }
    }

    private void Walk(Vector2 dir) 
    {
        rb.velocity = (new Vector2(dir.x * speed, rb.velocity.y));
    }

    private void Dash(float x) 
    {
        isDashing = true;
        rb.velocity = Vector2.zero;
        Vector2 dash = new Vector2(x, 0);
        
        rb.velocity += dash.normalized * dashSpeed;
        rb.drag = 14;
        rb.gravityScale = 0;

        CameraShake.Instance.ShakeCamera(5f, 0.5f);

        //Debug.Log("dash");
        StartCoroutine("DashWait"); // Parecido a un timer
    }

    IEnumerator DashWait() // Función que no se ejecuta en cada frame
    {
        for (float i = 6; i >= 0; i-- ) {
            rb.drag -= 1;
            yield return new WaitForSeconds(.05f);  // Tiempo que se espera en cada frame para volver a la ejecución de la función
        }

        isDashing = false;

        rb.gravityScale = 1;
        rb.drag = 0;

        //Debug.Log("STOP dash");
        // Deactivate Dash
        animator.SetBool("isDashing", false);
        dashParticles.SetActive(false);
    }
}
