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
    private Material material;
    public int conversationIndex { get; set; }

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        dm.onConversationFinish.AddListener(nextConversation);
        material = sr.material;
    }
    // Update is called once per frame
    void Update()
    {

        if (closeEnough)
        {
            // Outline
            if (!dm.inConversation)
                material.SetFloat("Thickness",0.02f);
            else 
                material.SetFloat("Thickness", 0f);

            // character Orientation
            if (player.position.x > this.transform.position.x)
                sr.flipX = true;
            else sr.flipX = false;

            // interactivity
            if (Input.GetKeyDown(KeyCode.E) && !dm.inConversation && !spokenTo)
            {
                dm.NPC = transform;
                dm.conversation = conversation[conversationIndex];
                dm.inConversation = true;
            }
        }
        else
        {
            material.SetFloat("Thickness", 0f);
        }

        if (Vector2.Distance(player.position, transform.position) <= detectionRange)
        {
            closeEnough = true;
        }
        else closeEnough = false;
    }

    void nextConversation() 
    {
        if (conversationIndex == 0) // Acabado dialogo inicial
            GameManager.Instance.UpdateGameState(GameManager.GameState.AnimacionPanaderia);
        else if (conversationIndex == 1) // Acabado segundo dialogo (empieza tutorial)
            GameManager.Instance.UpdateGameState(GameManager.GameState.Tutorial);
        if (conversationIndex!= 2 && conversationIndex != 3)
            conversationIndex++;
    }
}
