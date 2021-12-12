using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnBowlScene : MonoBehaviour
{
    //[HideInInspector] public Rigidbody2D rb;
    //[HideInInspector] public GameObject currentTask;
    public Transform camara;
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
                task.transform.position = new Vector3(camara.position.x, -2.5f, 0);
                GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>().touchingTable = true;
            }
            else
            {
                Time.timeScale = 1;
                GameObject.FindGameObjectWithTag("Bol").GetComponent<BowlController>().BackIgredients();
                task.SetActive(false);
                GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>().touchingTable = false;
                FoodBar.BarVisibility();
            }
            
        }
        if (task.activeSelf) { Time.timeScale = 0; }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        touchingPlayer = true;
        Texto.SetActive(true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        touchingPlayer = false;
        Texto.SetActive(false);
    }
}
