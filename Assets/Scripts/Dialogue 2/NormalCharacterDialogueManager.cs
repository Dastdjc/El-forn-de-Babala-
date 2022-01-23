using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// SCRIPT PARA CADA PERSONAJE QUE VAYA A TENER DIALOGO
public class NormalCharacterDialogueManager : MonoBehaviour
{
    public Transform player;
    public Conversation[] conversation;
    public float detectionRange = 10;
    public DialogueManager dm;
    public GameObject[] sprites;
    public DialogueAudio audioDialogue;

    private bool closeEnough = false;
    private bool spokenTo = false;
    private SpriteRenderer sr;
    private Material material;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    {

        if (closeEnough)
        {

            if (!dm.inConversation)
                SetThickness(0.005f);
            else
                SetThickness(0f);
            
           if (player.position.x > this.transform.position.x)
                transform.localScale = new Vector3(-1f, 1f, 1f);
            else transform.localScale = new Vector3(1f, 1f, 1f);

            if (Input.GetKeyDown(KeyCode.E) && !dm.inConversation && !spokenTo)
            {
                dm.NPC = transform;
                dm.conversation = conversation[Random.Range(0, conversation.Length)];
                dm.inConversation = true;
            }
        }
        else
        {
            SetThickness(0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        dm = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
        player = GameObject.Find("Dore_player").transform;
        audioDialogue.Start();
        closeEnough = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        closeEnough = false;
    }
    void SetThickness(float thick)
    {
        foreach (GameObject sprite in sprites)
        {
            material = sprite.GetComponent<SpriteRenderer>().material;
            material.SetFloat("Thickness", thick);
        }
        return;
    }
}
