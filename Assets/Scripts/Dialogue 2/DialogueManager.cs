using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DialogueManager : MonoBehaviour
{
    public Image portrait;
    public Text fullName;
    public Conversation conversation;
    public TextMeshProUGUI TextPro;
    public Animator boxAnimation;
    public GameObject dialogueBox;
    public bool inConversation = false;

    private int conversationIndex = 0;
    private bool startedConversation = false;
    // Start is called before the first frame update
    void Start()
    {
        TextPro.text = conversation.lines[0].text;
    }

    // Update is called once per frame
    void Update()
    {
        if (inConversation)
        {
            startedConversation = true;
            if (startedConversation) 
            {
                boxAnimation.SetBool("Cartel", true);
                dialogueBox.SetActive(true);
                startedConversation = false;
            }
            
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                conversationIndex++;
                ChangeTMPText();
            }
        }
    }

    void ChangeTMPText() 
    {
        TextPro.text = conversation.lines[conversationIndex].text;
        Line.Mood lineMood = conversation.lines[conversationIndex].Emotion;
        Character character = conversation.lines[conversationIndex].character;
        portrait.sprite = GetCharacterPortrait(lineMood, character);
    }

    Sprite GetCharacterPortrait(Line.Mood lineMood, Character ch) {
        Debug.Log(ch.name);
        for (int i = 0; i < ch.mood.Length; i++) {
            Debug.Log(ch.mood[i].ToString());
            Debug.Log(lineMood.ToString());
            if (ch.mood[i].emotion.ToString() == lineMood.ToString())
            {
                Debug.Log(ch.mood[i].ToString());
                Debug.Log(lineMood.ToString());
                return ch.mood[i].graphic;
            }
        }
        return ch.mood[0].graphic;
    }
}
