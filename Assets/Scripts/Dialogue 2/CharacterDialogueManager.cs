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
    public Conversation reacciones;
    public Conversation final;
    public float detectionRange = 10;
    public DialogueManager dm;

    private bool closeEnough = false;
    public int recipeNumber;
    private bool spokenTo = false;
    private Conversation conversationInstace;
    private SpriteRenderer sr;
    private Material material;
    private void OnEnable()
    {
        dm = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
    }
    private void Start()
    {

        //recipeNumber = Random.Range(0, GameManager.Instance.maxIndexRecipe); //Min inclusivo y max exclusivo
        //sr = GetComponent<SpriteRenderer>();
        //material = sr.material;
        player = GameObject.Find("Dore_player").transform;
    }
    public Conversation CrearConversacion(int indiceReceta) 
    {
        conversationInstace = ScriptableObject.CreateInstance("Conversation") as Conversation;
        conversationInstace.lines.AddRange(conversation.lines);
        // Chose recipe
        recipeNumber = indiceReceta; //Min inclusivo y max exclusivo
        //recipeNumber = 0;
        //Debug.Log(recipeNumber);

        // Add the recipe dialogue
        conversationInstace.lines.Add(recipeOptions.lines[recipeNumber]);
        // Add conversation continuation
        conversationInstace.lines.AddRange(continuation.lines);
        return conversationInstace;
    }
    void Update()
    {
       
        /*if (closeEnough)
        {
            if (!dm.inConversation)
                material.SetFloat("Thickness", 0.02f);
            else
                material.SetFloat("Thickness", 0f);
            //transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
            if (player.position.x > this.transform.position.x)
                sr.flipX = true;
            else sr.flipX = false;

            if (Input.GetKeyDown(KeyCode.E) && !dm.inConversation && !spokenTo)
            {
                dm.NPC = transform;
                dm.conversation = conversationInstace;
                dm.inConversation = true;
            }
        }
        else
        {
            material.SetFloat("Thickness", 0f);
        }

        if (Vector2.Distance(player.position, transform.position) <= detectionRange) {
            closeEnough = true;
        }
        else closeEnough = false;*/
    }
}
