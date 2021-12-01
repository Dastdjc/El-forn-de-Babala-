using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using Cinemachine;

public class DialogueManager : MonoBehaviour
{
    public Image portrait;
    public Conversation conversation;
    public Transform NPC;
    public TMP_Animated TextPro;
    public Animator boxAnimation;
    public GameObject dialogueBox;
    public GameObject nextIndicator;
    public bool inConversation = false;
    public Cinemachine.CinemachineVirtualCamera dialogueCamera;
    public Cinemachine.CinemachineTargetGroup TargetGroup;

    [System.Serializable] public class ConversationFinishEvent : UnityEvent { }
    public ConversationFinishEvent onConversationFinish;

    private int conversationIndex = 0;
    private bool conversationStarted = false;
    private bool nextDialogue = false;


    // Start is called before the first frame update
    void Start()
    {
        //TextPro.text = conversation.lines[0].text;
        TextPro.onDialogueFinish.AddListener(canSkip);
        dialogueBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (inConversation)
        {
            if (conversationIndex >= conversation.lines.Count)
            {
                SetDownConversation();
            }
            else
            {
                if (!conversationStarted)
                    setUpConversation();

                conversationStarted = true;
                if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && nextDialogue)
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
    }
    void setUpConversation() 
    {
        TargetGroup.m_Targets[1].target = NPC;
        dialogueCamera.Priority = 11;
        dialogueBox.SetActive(true);
        nextIndicator.SetActive(false);
        boxAnimation.SetBool("Cartel", true);

        Invoke("showDialogue", 0.1f);
    }
    public void SetDownConversation() 
    {
        boxAnimation.SetBool("Cartel", false);
        inConversation = false;
        conversationStarted = false;
        dialogueCamera.Priority = 1;
        conversationIndex = 0;
        onConversationFinish.Invoke();
    }
    void showDialogue() {
        nextIndicator.SetActive(false);
        ChangeTMPText();
        TextPro.ReadText(TextPro.text);
    }
    void ChangeTMPText() 
    {
        TextPro.text = conversation.lines[conversationIndex].text;
        Line.Mood lineMood = conversation.lines[conversationIndex].Emotion;
        Character character = conversation.lines[conversationIndex].character;
        portrait.sprite = GetCharacterPortrait(lineMood, character);
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

    void canSkip() {
        nextIndicator.SetActive(true);
        nextDialogue = true;
    }
}
