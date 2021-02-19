using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float X_VELOCITY_INSPECTION = 0;
    public float X_ACCELERATION_INSPECTION = 0;
    public List<Collider2D> blockingColliders; 

    public float speed = 10.0f;
    
    public float jumpHeight = 10f;
    public float jumpTime = 3f;

    float jumpVelocity = 100f;
    float minVelocity = 5f;

    private LayerMask worldCollisionMask = 1 << 9 | 1 << 10;
    Transform t;
    public Rigidbody2D rigidBody2d;
    public List<Collider2D> colliders;

    private Vector2 accel = Vector2.zero;

    private Vector2 direction;

    [SerializeField] Pause pause;
    bool isJumping = false;

    int extra_jumps = 0;

    // Ramp to enable the death sequence
    bool dying = false;

    Bounds bound;

    // Start is called before the first frame update
    void Start()
    {
        direction = Vector2.down;
        t = transform;
        rigidBody2d = GetComponent<Rigidbody2D>();
        colliders = new List<Collider2D>();
        colliders.Add(GetComponent<BoxCollider2D>());
        jumpVelocity = 2 * jumpHeight / jumpTime;
        rigidBody2d.gravityScale = jumpVelocity / jumpTime;
        
        bound = GameObject.Find("Grid/Tilemap").GetComponent<Renderer>().bounds;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Pause.isPaused) {
            if (Input.GetKeyDown(KeyCode.Space) && HasGravity() && (IsGrounded() || extra_jumps > 0))
            {
                if (!IsGrounded())
                {
                    extra_jumps--;
                }
                rigidBody2d.gravityScale = -rigidBody2d.gravityScale;
                if (direction.Equals(Vector2.down)) {
                    direction = Vector2.up;
                } else {
                    direction = Vector2.down;
                }
            } else if (Input.GetKeyDown(KeyCode.Space) && (IsGrounded() || extra_jumps > 0)) {
                if (!IsGrounded())
                {
                    extra_jumps--;
                }
                isJumping = true;
                rigidBody2d.velocity = new Vector2(rigidBody2d.velocity.x, 0f);
                rigidBody2d.gravityScale = jumpVelocity / jumpTime;
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

            if (IsGrounded())
            {
                extra_jumps = SlimeBoosts();
            }

            if (rigidBody2d.gravityScale < 0 && !HasGravity())
            {
                rigidBody2d.gravityScale = -rigidBody2d.gravityScale;
            }
        }

        if (!bound.Contains(transform.position)) {
            Die();
        }
    }

    void FixedUpdate() {
        Vector2 horizontalMove = new Vector2(Input.GetAxis("Horizontal") * speed, rigidBody2d.velocity.y);
        if (!dying)
        {   
            
            /* This doesn't work for some reason
            blockingColliders = new List<Collider2D>();
            //Prevent velocity build up
            bool isLeftBlocked = false;
            bool isRightBlocked = false;
            bool onGround = IsGrounded();
            foreach (BoxCollider2D collider in colliders) {
                RaycastHit2D leftHit = Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0, Vector2.left, 0.05f, worldCollisionMask);
                RaycastHit2D rightHit = Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0, Vector2.right, 0.05f, worldCollisionMask);
                if (leftHit.collider != null) {
                    isLeftBlocked = true;
                    blockingColliders.Add(leftHit.collider);
                }
                if (rightHit.collider != null) {
                    isRightBlocked = true;
                    blockingColliders.Add(rightHit.collider);
                }
            }
            
            if (isLeftBlocked && horizontalMove.x < 0) {
                horizontalMove = new Vector2(0, horizontalMove.y);
            }
            if (isRightBlocked && horizontalMove.x > 0) {
                horizontalMove = new Vector2(0, horizontalMove.y);
            }

            Vector2 old = rigidBody2d.velocity;
            */
            Vector2 old = rigidBody2d.velocity;
            rigidBody2d.velocity = Vector2.SmoothDamp(rigidBody2d.velocity, horizontalMove, ref accel, 0.05f);
            
            //Debug values (also do not make sense)
            X_VELOCITY_INSPECTION = rigidBody2d.velocity.x;
            X_ACCELERATION_INSPECTION = old.x - rigidBody2d.velocity.x;
        }
    }

    bool IsGrounded() {
        foreach (BoxCollider2D collider in colliders) {
            RaycastHit2D hit = Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0, direction, 0.05f, worldCollisionMask);

            bool yes = true;
            if (hit.collider == null) {
                yes = false;
            }

            //yes
            foreach (BoxCollider2D collider2 in colliders) {
                if ((hit.collider != null && hit.collider.gameObject.Equals(collider2.gameObject))) {
                    yes = false;
                }
            }

            //yes
            if (yes) {
                return yes;
            }
        }
        return false;
    }

    // for gravity square
    bool HasGravity()
    {
        if (!dying) {
            for (int i = 0; i < colliders.Count; i++)
            {
                if (transform.GetChild(i).GetComponent<GravityCell>() != null)
                {

                    return true;
                }
            }
            
        }
        return false;
    }

    int SlimeBoosts()
    {
        int count = 0;
        if (!dying)
        {
            for (int i = 0; i < colliders.Count; i++)
            {
                if (transform.GetChild(i).GetComponent<SlimeCell>() != null)
                {
                    count++;
                }
            }

        }
        return count;
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
        if (!dying) {
            Instantiate(transform.GetChild(0).GetComponent<ParticleSystem>(), transform.position, Quaternion.identity).GetComponent<Particle_Death>().Action();

            // kill all connecting boxes as well
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).GetComponent<Cell>() != null)
                {
                    transform.GetChild(i).gameObject.GetComponent<Cell>().death();
                    Destroy(transform.GetChild(i).gameObject);
                    colliders.RemoveAt(1);
                }
            }

            Destroy(transform.GetChild(0).gameObject);
            StartCoroutine(MyCoroutine());
        }

    }

}
