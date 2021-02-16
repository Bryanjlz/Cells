using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed = 10.0f;
    
    public float jumpHeight = 10f;
    public float jumpTime = 0.5f;

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
            rigidBody2d.velocity += Vector2.up * jumpVelocity;
            print(rigidBody2d.velocity);
        }

        if (t.position.y < 0) {
            t.position = new Vector3(t.position.x, 0, t.position.z);
            rigidBody2d.velocity = new Vector2(0, 0);
        }

        t.position += new Vector3(horizontalMove, 0, 0);

    }
}
