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
    public void Quit(){Application.Quit();}
    public void SetMenuVisible(int index)
    {
        foreach (Transform child in this.transform)
        {
            child.gameObject.SetActive(false);
        }
        canvas.transform.GetChild(index).gameObject.SetActive(true);
    }
    public void ChangeScene(int index) { Time.timeScale = 1; SceneManager.LoadScene(index);}
    public void SetSoundVol(float num) { SoundVol = num; }
    public void ResumeGame() { Start(); }
}
