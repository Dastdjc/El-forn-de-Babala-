using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stop : MonoBehaviour
{
    public void StopTrigger() { gameObject.GetComponent<Animator>().ResetTrigger("ActivateAnim"); gameObject.GetComponent<Animator>().enabled = false; }
}
