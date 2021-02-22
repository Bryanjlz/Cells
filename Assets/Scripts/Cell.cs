using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] Vector2 startingPos;
    [SerializeField] bool canJoin;
    [SerializeField] protected Rigidbody2D player;
    [SerializeField] Rigidbody2D myRigidbody;
    [SerializeField] BoxCollider2D myCollider;

    private const float TILE_SIZE = 1;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        canJoin = false;
        myRigidbody.isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Pause.isPaused)
        {
            if (canJoin && Input.GetKeyDown(KeyCode.E))
            {
                //Snap player to new cell

                //If close to equal, give up
                if (Mathf.Abs(Mathf.Abs(player.position.y - myRigidbody.position.y) - Mathf.Abs(player.position.x - myRigidbody.position.x)) < 0.1)
                {
                    print(Mathf.Abs(Mathf.Abs(player.position.y - myRigidbody.position.y) - Mathf.Abs(player.position.x - myRigidbody.position.x)));
                    return;

                }
                else if (Mathf.Abs(player.position.y - myRigidbody.position.y) > Mathf.Abs(player.position.x - myRigidbody.position.x))
                {
                    //vertical connection

                    //account for hitbox being smaller than sprite
                    if (player.position.y - myRigidbody.position.y > 0)
                    {
                        player.transform.position = new Vector2(myRigidbody.position.x, myRigidbody.position.y + TILE_SIZE);

                    }
                    else
                    {
                        player.transform.position = new Vector2(myRigidbody.position.x, myRigidbody.position.y - TILE_SIZE);
                    }

                    FindObjectOfType<AudioManager>().Play("Connect");

                }
                else if (Mathf.Abs(player.position.y - myRigidbody.position.y) < Mathf.Abs(player.position.x - myRigidbody.position.x))
                {
                    //horizontal connection

                    //account for hitbox being smaller than sprite
                    if (player.position.x - myRigidbody.position.x > 0)
                    {
                        player.transform.position = new Vector2(myRigidbody.position.x + TILE_SIZE, myRigidbody.position.y);
                    }
                    else
                    {
                        player.transform.position = new Vector2(myRigidbody.position.x - TILE_SIZE, myRigidbody.position.y);
                    }

                    FindObjectOfType<AudioManager>().Play("Connect");

                }
                else
                {
                    //if equal, give up
                    return;
                }

                myRigidbody.transform.SetParent(player.transform);
                Destroy(myRigidbody);
                BoxCollider2D[] colliders = gameObject.GetComponents<BoxCollider2D>();
                colliders[1].size = new Vector2(1f, 1f);

                myCollider.size = new Vector2(0.90f, 0.90f);
                myCollider.edgeRadius = 0.02f;
                player.gameObject.GetComponent<PlayerController>().colliders.Add(myCollider);
                canJoin = false;
                OnJoin();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" && (player.position - myRigidbody.position).magnitude < 1.5)
        {
            canJoin = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            canJoin = false;
        }
    }

    //Called when the cell is added
    public virtual void OnJoin() {

    }

    public void death()
    {
        Instantiate(transform.GetChild(0).GetComponent<ParticleSystem>(), transform.position, Quaternion.identity).GetComponent<Particle_Death>().Action();
    }
}
