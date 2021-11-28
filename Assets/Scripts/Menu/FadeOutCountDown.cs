using UnityEngine;
using System.Collections;

public class FadeOutCountDown : MonoBehaviour
{

    public float targetTime = 60.0f;

    void Update()
    {

        targetTime -= Time.deltaTime;

        if (targetTime <= 0.0f)
        {
            timerEnded();
        }

    }
    void timerEnded()
    {
        Animator anim = GetComponent<Animator>();
        anim.SetTrigger("Disapear");
    }


}
