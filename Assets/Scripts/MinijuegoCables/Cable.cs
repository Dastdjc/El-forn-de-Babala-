using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cable : MonoBehaviour
{

    public SpriteRenderer finalCable;
    public GameObject luz;
    static private int cables = 8;

    private Vector2 posicionOriginal;
    private Vector2 tamañoOriginal;

    // Start is called before the first frame update
    void Start()
    {
        posicionOriginal = transform.position;
        tamañoOriginal = finalCable.size;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
            Reiniciar();

    }

    private void OnMouseDrag()
    {
        ActualizarPosicion();
        Conexiones();
        ActualizarRotacion();
        ActualizarTamaño();
    }

    private void ActualizarPosicion()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePosition.x, mousePosition.y);
    }

    private void ActualizarRotacion()
    {
        Vector2 posicionActual = transform.position;
        Vector2 puntoOrigen = transform.parent.position;

        Vector2 direccion = posicionActual - puntoOrigen;

        float angulo = Vector2.SignedAngle(Vector2.right * transform.lossyScale, direccion);

        transform.rotation = Quaternion.Euler(0, 0, angulo);
    }

    private void ActualizarTamaño()
    {
        Vector2 posicionActual = transform.position;
        Vector2 puntoOrigen = transform.parent.position;

        float distancia = Vector2.Distance(posicionActual, puntoOrigen);

        finalCable.size = new Vector2(distancia, finalCable.size.y);
    }

    private void Reiniciar()
    {
        transform.position = posicionOriginal;
        transform.rotation = Quaternion.identity;
        finalCable.size = new Vector2(tamañoOriginal.x, tamañoOriginal.y);
    }

    private void Conexiones()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.2f);

        foreach (Collider2D col in colliders)
        {
            //Para que no nos salga el mismo cable en el que estamos

            if (col.gameObject != gameObject)
            {
                transform.position = col.transform.position;
                Cable otroCable = col.gameObject.GetComponent<Cable>();

                if (finalCable.color == otroCable.finalCable.color)
                {
                    //conexion correcta
                    Conectar();
                    otroCable.Conectar();
                }

            }
        }
            
    }

    public void Conectar()
    {
        luz.SetActive(true);
        cables--;
        //if(cables == 0) { KilnController.ReturnElec(); }
        Destroy(this);
    }
       
}
