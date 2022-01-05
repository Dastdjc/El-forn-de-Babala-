using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public float yVelocity = 10;
    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = new Vector3(this.transform.position.x, Random.Range(-5, 5), this.transform.position.z);
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(20f, yVelocity);

        StartCoroutine(MoverAbeja());
    }
    IEnumerator MoverAbeja()
    {
        for (int i = 0; i < 60; i++)
        {
            Debug.Log(Mathf.Sin(i));
            rb.velocity = new Vector2(rb.velocity.x, yVelocity * Mathf.Sin(i + Time.time));
            yield return new WaitForSeconds(0.1f);
        }
        Destroy(this.gameObject);
    }
}
