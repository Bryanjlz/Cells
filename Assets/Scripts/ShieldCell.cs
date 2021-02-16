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

    // Start is called before the first frame update
    void Start()
    {
        canJoin = false;
        myRigidbody.isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canJoin && Input.GetKeyDown(KeyCode.J) ) {
            if (Mathf.Abs(player.position.y - myRigidbody.position.y) > Mathf.Abs(player.position.x - myRigidbody.position.x)) {
                myRigidbody.transform.position = new Vector2(player.position.x, myRigidbody.position.y);
            } else {
                myRigidbody.transform.position = new Vector2(myRigidbody.position.x, player.position.y);
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
