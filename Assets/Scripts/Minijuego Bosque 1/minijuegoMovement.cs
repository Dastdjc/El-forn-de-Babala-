using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minijuegoMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D coll;

    public float speedInterpolate;

    private Vector2 position = new Vector2 (0f, 0f);
    private Vector3 mousePosition;

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

        if (aturdido)
        {
            StartCoroutine("Aturdir");

        }
        else {
            mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

            position = Vector2.Lerp(position, mousePosition, speedInterpolate);
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(position);
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

    IEnumerator Aturdir()
    {
        yield return new WaitForSeconds(2);
        rb.bodyType = RigidbodyType2D.Dynamic;
        coll.enabled = true;
        aturdido = false;
    }
}
