using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldCell : MonoBehaviour
{
    [SerializeField] Vector2 startingPos;
    [SerializeField] bool canJoin;
    [SerializeField] Rigidbody2D player;
    [SerializeField] Rigidbody2D myRigidbody;
    [SerializeField] BoxCollider2D myCollider;

    private float rectification = 0.02f;

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
        if (canJoin && Input.GetKeyDown(KeyCode.J) ) {
            //Snap player to new cell
            if (Mathf.Abs(player.position.y - myRigidbody.position.y) > Mathf.Abs(player.position.x - myRigidbody.position.x)) {
                //vertical connection

                //account for hitbox being smaller than sprite
                float correction = 0f;
                if (player.position.y - myRigidbody.position.y > 0) {
                    correction = rectification;
                } else {
                    correction = -rectification;
                }

                player.transform.position = new Vector2(myRigidbody.position.x, player.position.y + correction);

            } else if (Mathf.Abs(player.position.y - myRigidbody.position.y) < Mathf.Abs(player.position.x - myRigidbody.position.x)) {
                //horizontal connection

                //account for hitbox being smaller than sprite
                float correction = 0f;
                if (player.position.x - myRigidbody.position.x > 0) {
                    correction = rectification;
                } else {
                    correction = -rectification;
                }

                player.transform.position = new Vector2(player.position.x + correction, myRigidbody.position.y);

            } else {
                //if equal, give up
                return;
            }

            myRigidbody.transform.SetParent(player.transform);
            Destroy(myRigidbody);

            myCollider.size = new Vector2(0.92f, 0.92f);
            player.gameObject.GetComponent<PlayerController>().colliders.Add(myCollider);
            gameObject.layer = 8;
            canJoin = false;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.name == "Player"   && (player.position - myRigidbody.position).magnitude < 1.2) {
            canJoin = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.name == "Player") {
            canJoin = false;
        }
    }
}
