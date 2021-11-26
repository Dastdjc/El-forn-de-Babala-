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
    private void OnMouseDown()
    {
        if (JustClick) ChangeScene(SceneToJump);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!JustClick) ChangeScene(SceneToJump);
    }

    public void ChangeScene(int index)
    {

        Time.timeScale = 1;
        StartCoroutine(LoadLevel(index));

    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }
}
