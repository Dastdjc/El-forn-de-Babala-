using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    static public float SoundVol;
    public Animator transition;
    public float transitionTime = 1f;
    public GameObject canvas;

    private AudioSource BG_music;

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

        BG_music = FindObjectOfType<AudioSource>();
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
    public void ChangeScene(int index) 
    {
        if (index == 0) 
        {
            Destroy(GameManager.Instance);
            Destroy(GameObject.Find("GAME MANAGER"));
            Destroy(GameObject.Find("SERVICIOS"));
        }
        StartCoroutine(AudioFadeOut.FadeOut(BG_music, 1f));
        Time.timeScale = 1; 
        StartCoroutine(LoadLevel(index));

    }

    IEnumerator LoadLevel(int levelIndex) 
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }
    public void SetSoundVol(float num) { SoundVol = num; }
    public void ResumeGame() { Start(); }
}
