using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// SCRIPT PARA CADA PERSONAJE QUE VAYA A TENER DIALOGO
public class CharacterDialogueManager : MonoBehaviour
{
    public Transform player;
    public Conversation conversation;
    public Conversation recipeOptions;
    public Conversation continuation;
    public float detectionRange = 10;
    public DialogueManager dm;

    private bool closeEnough = false;
    private int recipeNumber;
    private bool spokenTo = false;
    private Conversation conversationInstace;

    private void Start()
    {
        conversationInstace = ScriptableObject.CreateInstance("Conversation") as Conversation;
        conversationInstace.lines.AddRange(conversation.lines);
        // Chose recipe
        recipeNumber = Random.Range(0, recipeOptions.lines.Count); //Min inclusivo y max exclusivo
        //recipeNumber = 0;
        Debug.Log(recipeNumber);
        // Add the recipe dialogue
        conversationInstace.lines.Add(recipeOptions.lines[recipeNumber]);
        // Add conversation continuation
        conversationInstace.lines.AddRange(continuation.lines);
    }
    // Update is called once per frame
    void Update()
    {
       
        if (closeEnough)
        {
            transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
            if (Input.GetKeyDown(KeyCode.Space) && !dm.inConversation && !spokenTo)
            {
                dm.NPC = transform;
                dm.conversation = conversationInstace;
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
