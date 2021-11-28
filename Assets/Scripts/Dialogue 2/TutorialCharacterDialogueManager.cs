using UnityEngine;

// SCRIPT PARA CADA PERSONAJE QUE VAYA A TENER DIALOGO
public class TutorialCharacterDialogueManager : MonoBehaviour
{
    public Transform player;
    public Conversation conversation;
    public DialogueManager dm;
    public bool tutorialActivated = false;


    // Update is called once per frame
    void Update()
    {

        if (tutorialActivated)
        {
            dm.NPC = transform;
            dm.conversation = conversation;
            dm.inConversation = true;
            tutorialActivated = false;
        }

    }
}
