using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinijuegoBee : MonoBehaviour
{
    public GameObject task;
    public GameObject Texto;

    private GameObject currentTask;
    private bool DONDESEA;
    public GameObject Player;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = Player.GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && DONDESEA == true)
        {
            if(currentTask == null)
            {
                currentTask = Instantiate(task, Player.transform);
                rb.bodyType = RigidbodyType2D.Static;
            }
            //SceneManager.LoadScene("Musical Bees", LoadSceneMode.Additive);
            
            else
            {
                Destroy(currentTask);
            }


        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Texto.SetActive(true);
            DONDESEA = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Texto.SetActive(false);
            DONDESEA = false;
        }
    }
}
