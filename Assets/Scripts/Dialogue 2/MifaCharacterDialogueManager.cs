using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// SCRIPT PARA CADA PERSONAJE QUE VAYA A TENER DIALOGO
public class MifaCharacterDialogueManager : MonoBehaviour
{
    public Transform player;
    public Conversation[] conversation;
    public float detectionRange = 10;
    public DialogueManager dm;

    private bool closeEnough = false;
    private bool spokenTo = false;
    private SpriteRenderer sr;
    public int conversationIndex { get; set; }

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        dm.onConversationFinish.AddListener(nextConversation);
    }
    // Update is called once per frame
    void Update()
    {

        if (closeEnough)
        {
            transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
            if (player.position.x > this.transform.position.x)
                sr.flipX = true;
            else sr.flipX = false;

            if (Input.GetKeyDown(KeyCode.Space) && !dm.inConversation && !spokenTo)
            {
                dm.NPC = transform;
                dm.conversation = conversation[conversationIndex];
                dm.inConversation = true;
            }
        }
        else
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }

        if (Vector2.Distance(player.position, transform.position) <= detectionRange)
        {
            closeEnough = true;
        }
        else closeEnough = false;
    }

    void nextConversation() 
    {
        conversationIndex++;
        if (conversationIndex == 1)
            GameManager.Instance.UpdateGameState(GameManager.GameState.AnimacionPanaderia);
    }
}
