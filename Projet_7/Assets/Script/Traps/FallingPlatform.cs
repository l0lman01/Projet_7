using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    private Animator animation;

    public Rigidbody2D rb2d;
    public float fallDelay;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animation = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            animation.Play("FallingPlatform");
            FallPlatManager.Instance.StartCoroutine("SpawnPlatform", new Vector2(transform.position.x, transform.position.y));
            StartCoroutine(Fall());
            Destroy(gameObject, fallDelay);
        }
    }

    IEnumerator Fall()
    {
        yield return new WaitForSeconds(fallDelay);
        rb2d.isKinematic = false;
        yield return 0;
    }
}
