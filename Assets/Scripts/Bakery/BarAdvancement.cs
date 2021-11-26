using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarAdvancement : MonoBehaviour
{
    public SpriteRenderer ColorBar;
    public Transform Mask;
    public int speed = 10;

    private void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale == 1)
        {

        }
        if(Mask != null && Time.timeScale == 1)
        {
            
        }
    }
    private void OnMouseDown()
    {
        Destroy(gameObject);
    }
}
