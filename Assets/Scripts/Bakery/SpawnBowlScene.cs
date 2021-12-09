using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnBowlScene : MonoBehaviour
{
    //[HideInInspector] public Rigidbody2D rb;
    //[HideInInspector] public GameObject currentTask;
    public GameObject camara;
    public GameObject task;
    public GameObject Texto;
    private bool touchingPlayer;
    // Start is called before the first frame update
    void Start()
    {
        Texto.GetComponent<TextMeshPro>().text = "Pulsa E para poner ingredientes";
        Texto.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && touchingPlayer)
        {
            if(Time.timeScale == 1)
            {
                Time.timeScale = 0;
                task.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                task.SetActive(false);
                //FoodBar.DestroyBar();
            }
            
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        touchingPlayer = true;
        Texto.SetActive(true);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        touchingPlayer = false;
        Texto.SetActive(false);
    }
}
