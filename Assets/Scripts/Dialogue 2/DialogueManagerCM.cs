using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cinemachine;

public class DialogueManagerCM : MonoBehaviour
{
    public Image portrait;
    public Conversation conversation;
    public Transform NPC;
    public TMP_Animated TextPro;
    public Animator boxAnimation;
    public GameObject dialogueBox;
    public bool inConversation = false;
    public Cinemachine.CinemachineVirtualCamera dialogueCamera;
    public Cinemachine.CinemachineTargetGroup TargetGroup;
    [HideInInspector] public int index;

    private int conversationIndex = 0;
    private bool conversationStarted = false;
    private bool nextDialogue = false;
   

    // Start is called before the first frame update
    void Start()
    {
        //TextPro.text = conversation.lines[0].text;
        TextPro.onDialogueFinish.AddListener(canSkip);
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (inConversation)
        {
            if (conversationIndex >= 1)
            {
                boxAnimation.SetBool("Cartel", false);
                inConversation = false;
                conversationStarted = false;
                dialogueCamera.Priority = 1;
                conversationIndex = 0;
            }
            else
            {
                if (!conversationStarted)
                {
                    setUpConversation();
                    showDialogue(index);
                }
                conversationStarted = true;
                if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && nextDialogue)
                {
                    conversationIndex++;
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
        boxAnimation.SetBool("Cartel", true);

        //Invoke("showDialogue", 0.1f);


    }

    void showDialogue(int a)
    {
        ChangeTMPText(a);
        TextPro.ReadText(TextPro.text);
    }
    void ChangeTMPText(int a)
    {
        TextPro.text = conversation.lines[a].text;
        Line.Mood lineMood = conversation.lines[a].Emotion;
        Character character = conversation.lines[a].character;
        portrait.sprite = GetCharacterPortrait(lineMood, character);
    }

    Sprite GetCharacterPortrait(Line.Mood lineMood, Character ch)
    {
        for (int i = 0; i < ch.mood.Length; i++)
        {

            if (ch.mood[i].emotion.ToString() == lineMood.ToString())
            {
                return ch.mood[i].graphic;
            }
        }
        return ch.mood[0].graphic;
    }

    void canSkip()
    {
        nextDialogue = true;
    }
}
