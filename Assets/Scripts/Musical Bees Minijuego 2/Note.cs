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

        if (t > 0.7)
        {
            Destroy(gameObject);
        }
        else if (t < 0.5)
        {
            transform.localScale = Vector3.Lerp(new Vector3(1, 1, 0) * SongManager.Instance.noteSpawnScale, new Vector3(1, 1, 0) * SongManager.Instance.noteTapScale, t*2); // unitario * SongManager.Instance.noteSpawnScale, unitario * SongManager.Instance.noteDespawnScale
            GetComponent<SpriteRenderer>().enabled = true;
        }
    }
}
