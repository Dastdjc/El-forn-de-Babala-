using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipDialogue : MonoBehaviour
{
    public DialogueManager dm;
    public Animator transition;
    public GameObject skip;
    
    // Start is called before the first frame update
    void Start()
    {
        //gameObject.SetActive(false);
        //skip = GameObject.Find("/Dialogue/Skip");
        //dm = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
        //transition = GameObject.Find("/Canvas/CircleTransition").GetComponent<Animator>();

        skip.SetActive(false);
    }

    void Update()
    {
        if (dm.inConversation) 
        {
            skip.SetActive(true);
        }
        else
        skip.SetActive(false);
    }

    public void EndConversation() 
    {
        transition.SetTrigger("Start");
        dm.SetDownConversation();
        Invoke("ExitTransition", 1f);
    }

    void ExitTransition() 
    {
        transition.SetTrigger("End");
    }
}
