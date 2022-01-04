using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minijuegoMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D coll;

    public float speedInterpolate;

    public GameObject particulasBien;
    public GameObject particulasAturdir;
    private GameObject particula;

    private Vector2 position = new Vector2 (0f, 0f);
    private Vector3 mousePosition;

    private bool aturdido = false;

    //Variables globales para los recursos
    [HideInInspector]
    public int calabaza, almendra, limon, boniato;
    [HideInInspector]
    //public Items Calabaza, Almendra, Limon, Boniato;
    //public Inventory Inventario;

    void Start()
    {
        //Inventario = FindObjectOfType<Inventory>();
        rb = GetComponent<Rigidbody2D>(); 
        coll = GetComponent<Collider2D>();
        calabaza = 0;
        almendra = 0;
        limon = 0;
        boniato = 0;
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
            case "Almendra":
                particula = Instantiate(particulasBien, gameObject.transform.position, Quaternion.identity);
                particula.transform.parent = gameObject.transform;
                almendra += 1;
                //Almendra.amount += 1;
                break;
            case "Limon":
                particula = Instantiate(particulasBien, gameObject.transform.position, Quaternion.identity);
                particula.transform.parent = gameObject.transform;
                limon += 1;
                //Limon.amount += 1;
                break;
            case "Boniato":
                particula = Instantiate(particulasBien, gameObject.transform.position, Quaternion.identity);
                particula.transform.parent = gameObject.transform;
                boniato += 1;
                //Boniato.amount += 1;
                break;
            case "Calabaza":
                particula = Instantiate(particulasBien, gameObject.transform.position, Quaternion.identity);
                particula.transform.parent = gameObject.transform;
                calabaza += 1;
                //Calabaza.amount += 1;
                break;
            case "Piedra":
                particula = Instantiate(particulasAturdir, gameObject.transform.position, Quaternion.identity);
                particula.transform.parent = gameObject.transform;
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
        yield return new WaitForSeconds(3);
        rb.bodyType = RigidbodyType2D.Dynamic;
        coll.enabled = true;
        aturdido = false;
    }
}
