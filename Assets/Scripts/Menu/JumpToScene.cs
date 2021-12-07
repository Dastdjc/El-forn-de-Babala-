using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JumpToScene : MonoBehaviour
{
    public int SceneToJump;
    public bool JustClick;

    public Animator transition;
    public float transitionTime = 1f;
    private AudioSource BG_music;

    private bool trigger = false;
    private void Start()
    {
        BG_music = GameObject.Find("BG_Music").GetComponent<AudioSource>(); 
    }
    private void Update()
    {
        if (trigger && Input.GetKeyDown(KeyCode.E)) 
        {
            ChangeScene(SceneToJump);
        }
    }
    private void OnMouseDown()
    {
        if (JustClick) ChangeScene(SceneToJump);
    }
    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!JustClick) ChangeScene(SceneToJump);
    }
    private void OnMouseDown()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "Bakery":
                SpawnCustomers.PauseScene();
                break;
            case "Cables-minijuego":
                SpawnCustomers.PauseScene();
                break;
        }
        if (JustClick) SceneManager.LoadScene(SceneToJump);
    }*/
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!JustClick) ChangeScene(SceneToJump);
        //else if (!JustClick && panadería && Input.GetKeyDown(KeyCode.E)) ChangeScene(SceneToJump);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger");
        if (!JustClick) trigger = true;
        transform.GetChild(0).gameObject.SetActive(true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!JustClick) trigger = false;
        transform.GetChild(0).gameObject.SetActive(false);
    }
    public void ChangeScene(int index)
    {
        // Changing Scene... 
        Debug.Log("Changing scene...");
        StartCoroutine(AudioFadeOut.FadeOut(BG_music, 1f));
        Time.timeScale = 1;
        if (index == 5)  // Si va al bosque, actualizar gameState
            GameManager.Instance.UpdateGameState(GameManager.GameState.Bosque);
        else if (index == 10)
            GameManager.Instance.UpdateGameState(GameManager.GameState.Pueblo);
        StartCoroutine(LoadLevel(index));
    }
    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }
}


