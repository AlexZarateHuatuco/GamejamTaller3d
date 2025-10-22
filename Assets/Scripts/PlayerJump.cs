using UnityEngine;
using System.Collections;
public class PlayerJump : MonoBehaviour
{
    Rigidbody2D rb;
    float jumpPower = 10f;
    private float ignoreTime = 0.2f;
    private bool isDropping = false;
    private Collider2D playerCollider;
    void Start()
    {
        playerCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.S) && Input.GetKeyDown(KeyCode.Space))
        {
            //StartCoroutine(Jump());
        }
    }
    /*IEnumerator Jump()
    {
        isDropping = true;
        playerCollider.enabled = !playerCollider.enabled;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 1f;
        rb.linearVelocity = new Vector2(transform.localScale.y * jumpPower, 0f);
        isDropping = false;
    }*/
}
