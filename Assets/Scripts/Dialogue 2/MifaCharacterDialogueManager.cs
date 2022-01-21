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
    public GameObject[] sprites;

    private bool closeEnough = false;
    private bool spokenTo = false;
    //private SpriteRenderer sr;
    private Material material;
    public int conversationIndex { get; set; }

    private bool firstTime = true;
    /*public GameObject pulsaE;
    private Animator pulsaEAnim;*/

    private void Start()
    {
        //sr = GameObject.Find("/Characters/mifa/Mifa 1/BackSombrero").GetComponent<SpriteRenderer>();
        dm.onConversationFinish.AddListener(nextConversation);
        //material = sr.material;

        /*pulsaE = GameObject.Find("Pulsa E");
        pulsaEAnim = pulsaE.GetComponent<Animator>();*/
    }
    // Update is called once per frame
    void Update()
    {

        if (closeEnough)
        {
            // Outline
            if (!dm.inConversation)
                SetThickness(0.005f);
            else
                SetThickness(0f);

            // character Orientation
            if (player.position.x > this.transform.position.x)
                transform.localScale = new Vector3(-1.5f, 1.5f, 1f);
            else transform.localScale = new Vector3(1.5f, 1.5f, 1f); ;

            // interactivity
            if (Input.GetKeyDown(KeyCode.E) && !dm.inConversation && !spokenTo)
            {
                dm.NPC = transform;
                dm.conversation = conversation[conversationIndex];
                dm.inConversation = true;

                /*if (firstTime) 
                {
                    pulsaEAnim.SetTrigger("PulsaE");
                    firstTime = false;
                }*/
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

    void nextConversation() 
    {
        if (conversationIndex == 0) // Acabado dialogo inicial
            GameManager.Instance.UpdateGameState(GameManager.GameState.AnimacionPanaderia);
        else if (conversationIndex == 1) // Acabado segundo dialogo (empieza tutorial)
            GameManager.Instance.UpdateGameState(GameManager.GameState.Tutorial);
        else if (conversationIndex == conversation.Length-1)
            GameManager.Instance.UpdateGameState(GameManager.GameState.CortarCuerda);
        if (conversationIndex == 4)
            GameManager.Instance.dia = true;
        if (conversationIndex!= 2 && conversationIndex != 3)
            conversationIndex++;
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
   /* void EliminarPulsaE() 
    {
        pulsaE.SetActive(false);
    }*/
}
