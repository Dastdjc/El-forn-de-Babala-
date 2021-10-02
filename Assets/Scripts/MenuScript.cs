using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    static public float SoundVol;
    public GameObject canvas;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("El menú para la escena 0 es para jugar y cargar otra escena, la del juego" +
            "En cualquier escena que no sea la 0 aparecerá el menú de pausa");
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            SetMenuVisible(0); 
        }
        else
        {
            SetMenuVisible(1);
            canvas.SetActive(false);
        }
        
    }
    public void Quit()
    {
        Application.Quit();
    }
    //En el menú de opciones la tecla back si estamos en la escena 0 irá a StartMenu pero con éste método también lo hará en el juego
    public void SetMenuVisible(int index)
    {
        foreach (Transform child in this.transform)
        {
            child.gameObject.SetActive(false);
        }
        canvas.transform.GetChild(index).gameObject.SetActive(true);
    }
    //Si le indicamos que no estamos en la escena 0 mediante el bool volveremos al PauseMenu
    public void SetMenuVisible(int index, bool backControl)
    {
        foreach (Transform child in this.transform)
        {
            child.gameObject.SetActive(false);
        }
        if (backControl && SceneManager.GetActiveScene().buildIndex != 0) { index++; }
        canvas.transform.GetChild(index).gameObject.SetActive(true);
    }
    public void ChangeScene(int index) {SceneManager.LoadScene(index);}
    public void SetSoundVol(float num) { SoundVol = num; }
    public void ResumeGame() { canvas.SetActive(false); Time.timeScale = 1; }
    /*public void Update()//Para activar el menu será necesario hacerlo desde el jugador
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            if (Active)
            {
                SetMenuVisible(0, true);
                canvas.SetActive(false);
                Active = false;
            }
            else
            {
                canvas.SetActive(true);
                Active = true;
            }
        }
    }*/
}
