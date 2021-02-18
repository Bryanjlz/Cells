using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed = 10.0f;
    
    public float jumpHeight = 10f;
    public float jumpTime = 3f;

    float jumpVelocity = 100f;
    float minVelocity = 7f;

    public LayerMask platformLayer;
    Transform t;
    Rigidbody2D rigidBody2d;
    public List<BoxCollider2D> colliders;

    private Vector2 accel = Vector2.zero;

    [SerializeField] Pause pause;
    bool isJumping = false;

    // Ramp to enable the death sequence
    bool dying = false;

    // Start is called before the first frame update
    void Start()
    {
        t = transform;
        rigidBody2d = GetComponent<Rigidbody2D>();
        colliders = new List<BoxCollider2D>();
        colliders.Add(GetComponent<BoxCollider2D>());
    }

    // Update is called once per frame
    void Update()
    {
        if (!Pause.isPaused) {
            if (Input.GetKeyDown(KeyCode.Space) && IsGrounded()) {
                jumpVelocity = 2 * jumpHeight / jumpTime;
                rigidBody2d.gravityScale = jumpVelocity / jumpTime;
                isJumping = true;
                rigidBody2d.AddForce(Vector2.up * jumpVelocity * 60);
            }
            if (isJumping) {
                if (Input.GetKeyUp(KeyCode.Space)) {
                    isJumping = false;
                    if (rigidBody2d.velocity.y > minVelocity) {
                        rigidBody2d.velocity = new Vector2(rigidBody2d.velocity.x, minVelocity);
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.K) && IsGrounded())
            {
                rigidBody2d.gravityScale = -rigidBody2d.gravityScale;
            }
        }
    }

    void FixedUpdate() {
        Vector2 horizontalMove = new Vector2(Input.GetAxis("Horizontal") * speed, rigidBody2d.velocity.y);
        if (!dying)
        {
            rigidBody2d.velocity = Vector2.SmoothDamp(rigidBody2d.velocity, horizontalMove, ref accel, 0.05f);
        }
    }

    bool IsGrounded() {
        foreach (BoxCollider2D collider in colliders) {
            RaycastHit2D hit = Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0, Vector2.down, 0.05f, platformLayer);
            RaycastHit2D other_hit = Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0, Vector2.up, 0.05f, platformLayer);
            if (hit.collider != null || other_hit.collider != null) {
                return true;
            }
        }
        return false;
    }
    IEnumerator MyCoroutine()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().isKinematic = true;
        dying = true;
        yield return new WaitForSeconds(1);
        pause.Restart();
    }
    public void Die () {
        Instantiate(transform.GetChild(0).GetComponent<ParticleSystem>(), transform.position, Quaternion.identity).GetComponent<Particle_Death>().Action();

        StartCoroutine(MyCoroutine());
        
    }

}
