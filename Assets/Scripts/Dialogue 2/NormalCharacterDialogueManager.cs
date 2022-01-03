using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// SCRIPT PARA CADA PERSONAJE QUE VAYA A TENER DIALOGO
public class NormalCharacterDialogueManager : MonoBehaviour
{
    public Transform player;
    public Conversation conversation;
    public float detectionRange = 10;
    public DialogueManager dm;
    public GameObject[] sprites;

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
                transform.localScale = new Vector3(-1.5f, 1.5f, 1f);
            else transform.localScale = new Vector3(1.5f, 1.5f, 1f);

            if (Input.GetKeyDown(KeyCode.E) && !dm.inConversation && !spokenTo)
            {
                dm.NPC = transform;
                dm.conversation = conversation;
                dm.inConversation = true;
            }
        }
        else
        {
            SetThickness(0f);
        }

        if (Vector2.Distance(player.position, transform.position) <= detectionRange)
        {
            closeEnough = true;
        }
        else closeEnough = false;
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
