using System.Collections;
using Cinemachine;
using UnityEngine;
public class MouseZoom : MonoBehaviour
{
    float minOrthScale;
    float maxOrthScale;

    float orthScale = 18;

    public float sensitivity = 10f;
    public CinemachineVirtualCamera cam;


    // Update is called once per frame
    void Update()
    {
        orthScale = cam.m_Lens.OrthographicSize;
        orthScale += Input.GetAxis("Mouse ScrollWheel") * sensitivity;
        orthScale = Mathf.Clamp(orthScale, minOrthScale, maxOrthScale);
        cam.m_Lens.OrthographicSize = orthScale;
    }

    IEnumerator SmoothScale(float start, float end, float duration)
    {
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            orthScale = Mathf.Lerp(start, end, elapsed / duration);
            yield return null;
        }
        orthScale = end;
    }

}

