using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public float yVelocity = 10;

    private float yRange = 8;
    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = new Vector3(this.transform.position.x, Random.Range(-yRange, yRange), this.transform.position.z);
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(20f, yVelocity);

        StartCoroutine(MoverAbeja());
    }
    IEnumerator MoverAbeja()
    {
        for (int i = 0; i < 60; i++)
        {
            rb.velocity = new Vector2(rb.velocity.x, yVelocity * Mathf.Sin((i + Time.time)/2));
            yield return new WaitForSeconds(0.1f);
        }
        Destroy(this.gameObject);
    }
}
