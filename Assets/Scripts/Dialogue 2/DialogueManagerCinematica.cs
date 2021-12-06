using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;


public class DialogueManagerCinematica: MonoBehaviour
{
    public float skipTime = 2f;
    public Conversation conversation;
    public TMP_Animated TextPro;
    public Animator transition;
    public int sceneIndex = 9;

    private int conversationIndex = 0;
    private bool conversationStarted = false;
    private bool nextDialogue = false;
    private AudioSource BG_music;

    // Start is called before the first frame update
    void Start()
    {
        TextPro.onDialogueFinish.AddListener(skip);
        BG_music = FindObjectOfType<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
            if (conversationIndex >= conversation.lines.Count)  // Conversation END
            {
                //conversationStarted = false;
                conversationIndex = 0;
                endCutscene();
            }
            else
            {
                if (!conversationStarted)
                    setUpConversation();
                conversationStarted = true;
                if (nextDialogue)
                {
                    conversationIndex++;
                    if (conversationIndex < conversation.lines.Count)
                    {
                        showDialogue();
                    }
                    nextDialogue = false;
                }
            }
    }
    void setUpConversation() 
    {
        Invoke("showDialogue", 0.2f);
    }

    void showDialogue() {
        ChangeTMPText();
        TextPro.ReadText(TextPro.text);
    }
    void ChangeTMPText() 
    {
        TextPro.text = conversation.lines[conversationIndex].text;
    }

    Sprite GetCharacterPortrait(Line.Mood lineMood, Character ch) 
    {
        for (int i = 0; i < ch.mood.Length; i++) {

            if (ch.mood[i].emotion.ToString() == lineMood.ToString())
            {
                return ch.mood[i].graphic;
            }
        }
        return ch.mood[0].graphic;
    }

    void skip() {
        StartCoroutine("canSkip");
    }
    IEnumerator canSkip() {
        yield return new WaitForSeconds(skipTime);
        nextDialogue = true;
    }

    void endCutscene() {
        StartCoroutine(AudioFadeOut.FadeOut(BG_music, 1f));
        ChangeScene(sceneIndex);
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
