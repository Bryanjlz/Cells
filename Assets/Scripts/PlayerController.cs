using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed = 10.0f;
    
    public float jumpHeight = 10f;
    public float jumpTime = 3f;

    float jumpVelocity = 100f;

    public LayerMask platformLayer;
    Transform t;
    Rigidbody2D rigidBody2d;
    BoxCollider2D collider;

    private Vector2 accel = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        t = transform;
        rigidBody2d = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded()) {

            jumpVelocity = 2 * jumpHeight / jumpTime;
            rigidBody2d.gravityScale = jumpVelocity / jumpTime;
            rigidBody2d.AddForce(Vector2.up * jumpVelocity * 60);
        }

    }

    void FixedUpdate() {
        Vector2 horizontalMove = new Vector2(Input.GetAxis("Horizontal") * 60 * speed * Time.deltaTime, rigidBody2d.velocity.y);
        

        rigidBody2d.velocity = Vector2.SmoothDamp(rigidBody2d.velocity, horizontalMove, ref accel, 0.05f);
    }

    bool IsGrounded() {
        RaycastHit2D hit = Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0, Vector2.down, 0.01f, platformLayer);
        return hit.collider != null;
    }
}
