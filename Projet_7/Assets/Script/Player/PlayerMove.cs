using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private LayerMask GroundLayer;
    private float Force = 10f;

    private float h;
    private Rigidbody2D rb2d;
    private SpriteRenderer spriteRenderer;

    public bool isGrounded = false;
    // Start is called before the first frame update
    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Z) && isGrounded)
        {
            rb2d.AddForce(new Vector2(0f, 10) * Force);
        }
    }
    void FixedUpdate()
    {
        rb2d.AddForce(new Vector2(h, 0f) * Force);

        if (rb2d.velocity.x > 0.1f)
            spriteRenderer.flipX = true;
        else if (rb2d.velocity.x < -0.1f)
            spriteRenderer.flipX = false;

        Vector3 rayCastDir = spriteRenderer.flipY ? Vector3.up : -Vector3.up;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, rayCastDir, 0.5f, GroundLayer.value);
        isGrounded = hit.collider != null;
    }
}
