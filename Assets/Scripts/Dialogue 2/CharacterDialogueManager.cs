using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// SCRIPT PARA CADA PERSONAJE QUE VAYA A TENER DIALOGO
public class CharacterDialogueManager : MonoBehaviour
{
    public Transform player;
    public Conversation conversation;
    public float detectionRange = 10;
    public DialogueManager dm;

    private bool closeEnough = false;

    // Update is called once per frame
    void Update()
    {
       
        if (closeEnough)
        {
            transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                dm.NPC = transform;
                dm.conversation = conversation;
                dm.inConversation = true;
            }
        }
        else {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }

        if (Vector2.Distance(player.position, transform.position) <= detectionRange) {
            closeEnough = true;
        }
        else closeEnough = false;
    }
}
