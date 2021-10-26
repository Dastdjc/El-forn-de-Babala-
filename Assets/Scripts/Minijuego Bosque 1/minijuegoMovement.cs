using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minijuegoMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D coll;
    private bool isDashing = false;

    public float speed = 5;
    public float dashSpeed = 10;

    private bool aturdido = false;

    //Variables globales para los recursos
    [HideInInspector]
    public int calabaza, huevos, harina, pan;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
        coll = GetComponent<Collider2D>();
    }

    void Update()
    {
        Debug.Log(aturdido);
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        // La direcci�n en s� no la magnitud, para el dash
        float xRaw = Input.GetAxisRaw("Horizontal");
        Vector2 dir = new Vector2(x, y);

        // L�gica del movimiento
        if (!isDashing)
        {
            Walk(dir);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing && xRaw != 0)
        {
            Dash(xRaw);
        }

        if (aturdido)
        {
            StartCoroutine("Aturdir");
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) //deteccion de los ingredientes
    {
        switch (collision.gameObject.tag)
        {
            case "Huevo":
                huevos += 1;
                break;
            case "Harina":
                harina += 1;
                break;
            case "Pan":
                pan += 1;
                break;
            case "Calabaza":
                calabaza += 1;
                break;
            case "Piedra":
                rb.bodyType = RigidbodyType2D.Static;
                coll.enabled = false;
                aturdido = true;
                break;
            default:
                break;
        }

        Destroy(collision.gameObject);

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

        Debug.Log("dash");
        StartCoroutine("DashWait"); // Parecido a un timer
    }

    IEnumerator DashWait() // Funci�n que no se ejecuta en cada frame
    {
        for (float i = 6; i >= 0; i--)
        {
            rb.drag -= 1;
            yield return new WaitForSeconds(.005f);  // Tiempo que se espera en cada frame para volver a la ejecuci�n de la funci�n
        }

        isDashing = false;

        rb.gravityScale = 1;
        rb.drag = 0;

        Debug.Log("STOP dash");
    }


    IEnumerator Aturdir()
    {
        yield return new WaitForSeconds(2);
        rb.bodyType = RigidbodyType2D.Dynamic;
        coll.enabled = true;
        aturdido = false;
    }
}
