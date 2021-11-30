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
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!JustClick) SceneManager.LoadScene(SceneToJump);
    }
}
