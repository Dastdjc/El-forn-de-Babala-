using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    double timeInstantiated;
    public float assignedTime;

    void Start()
    {
        //timeInstantiated = SongManager.GetAudioSourceTime();
        timeInstantiated = assignedTime - SongManager.Instance.noteTime;
    }

    // Update is called once per frame
    void Update()
    {
        double timeSinceInstantiated = SongManager.GetAudioSourceTime() - timeInstantiated;

        float t = (float)(timeSinceInstantiated / (SongManager.Instance.noteTime * 2)); //assignedTime + SongManager.Instance.marginOfError);

        if (t > 1)
        {
            Destroy(gameObject);
        }
        else if (t > 0.5)
        {
            //transform.localPosition = Vector3.Lerp(Vector3.up * SongManager.Instance.noteSpawnScale, Vector3.up * SongManager.Instance.noteDespawnScale, t);
            //transform.localScale = Vector3.Lerp(new Vector3(1, 1, 0) * SongManager.Instance.noteSpawnScale, new Vector3(1, 1, 0) * SongManager.Instance.noteDespawnScale, t); // unitario * SongManager.Instance.noteSpawnScale, unitario * SongManager.Instance.
        }
        else 
        {
            transform.localScale = Vector3.Lerp(new Vector3(1, 1, 0) * SongManager.Instance.noteSpawnScale, new Vector3(1, 1, 0) * SongManager.Instance.noteTapScale, t*2); // unitario * SongManager.Instance.noteSpawnScale, unitario * SongManager.Instance.noteDespawnScale
            GetComponent<SpriteRenderer>().enabled = true;
        }
    }
}
