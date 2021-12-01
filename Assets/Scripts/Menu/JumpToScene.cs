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

    private void Start()
    {
        BG_music = GameObject.Find("BG_Music").GetComponent<AudioSource>();
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
        if (!JustClick) SceneManager.LoadScene(SceneToJump);
    }
    public void ChangeScene(int index)
    {
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


