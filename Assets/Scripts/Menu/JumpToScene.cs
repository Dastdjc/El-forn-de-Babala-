using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JumpToScene : MonoBehaviour
{
    public int SceneToJump;
    public bool JustClick;
    private void OnMouseDown()
    {
        if (JustClick) SceneManager.LoadScene(SceneToJump);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!JustClick) SceneManager.LoadScene(SceneToJump);
    }
}
