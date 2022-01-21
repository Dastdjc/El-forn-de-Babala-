using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCables : MonoBehaviour
{
    public GameObject task;
    public bool back;
    public GameObject player;
    private void OnMouseDown()
    {
        task.transform.position = new Vector3(player.transform.position.x, 0, 0);
        task.SetActive(!back);
        Time.timeScale = back ? 1:0;
    }

}
