using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;

    private float startTime;
    private bool finished = false;
    void Start()
    {
        startTime = Time.time;

    }

    // Update is called once per frame
    void Update()
    {
        if (!finished)
        {
            float t = Time.time - startTime;


            string seconds = (t % 60).ToString("f2");
            if (seconds == "30,00")
            {
                Finish();
            }

            timerText.text = seconds;
        }
        else if (finished)
        {
            
        }
    }

    void Finish()
    {
        finished = true;
        timerText.color = Color.red;
    }
}
