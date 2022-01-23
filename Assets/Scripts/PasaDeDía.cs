using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PasaDeDía : MonoBehaviour
{
    public int sceneIndex;
    public AudioSource BG_music;
    public Animator transition;
    public TextMeshProUGUI text;
    private bool clicked = false;

    // Start is called before the first frame update
    void Start()
    {
        text.text = "Día: " + GameManager.Instance.numDia;
    }

    
    public void endCutscene()
    {
        if (!clicked)
        {
            clicked = true;
            text.text = "Día: " + ++GameManager.Instance.numDia;
            StartCoroutine(AudioFadeOut.FadeOut(BG_music, 1f));
            ChangeScene(sceneIndex);
        }
    }
    public void ChangeScene(int index)
    {
        Debug.Log("Change");
        Time.timeScale = 1;
        StartCoroutine(LoadLevel(index));
    }
    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(levelIndex);
    }
}
