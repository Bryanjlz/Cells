using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed = 10.0f;
    
    public float jumpHeight = 10f;
    public float jumpTime = 3f;

    float jumpVelocity = 100f;


    Transform t;
    Rigidbody2D rigidBody2d;

    // Start is called before the first frame update
    void Start()
    {
        t = transform;
        rigidBody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalMove = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space)) {

            jumpVelocity = 2 * jumpHeight / jumpTime;
            rigidBody2d.gravityScale = jumpVelocity / jumpTime;
            rigidBody2d.velocity = Vector2.up * jumpVelocity;
        }

        rigidBody2d.position += new Vector2(horizontalMove, 0);

    }
}
