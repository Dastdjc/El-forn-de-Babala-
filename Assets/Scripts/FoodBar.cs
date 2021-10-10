using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodBar : MonoBehaviour
{
    public Animator Controller;
    private void OnMouseDown()
    {
        Controller.SetTrigger("MouseTouch");
        bool dir = Controller.GetBool("Up");
        dir = !dir;
        Controller.SetBool("Up", dir);
    }
    public void ResetTrigger()
    {
        Controller.ResetTrigger("MouseTouch");
    }
}
