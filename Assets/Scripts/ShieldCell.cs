﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldCell : Cell
{
    [SerializeField] Vector2 startingPos;
    [SerializeField] bool canJoin;
    [SerializeField] Rigidbody2D player;
    [SerializeField] Rigidbody2D myRigidbody;
    [SerializeField] BoxCollider2D myCollider;

    private const float TILE_SIZE = 1;
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
                if (player.position.y - myRigidbody.position.y > 0) {
                    player.transform.position = new Vector2(myRigidbody.position.x, myRigidbody.position.y + TILE_SIZE);

                } else {
                    player.transform.position = new Vector2(myRigidbody.position.x, myRigidbody.position.y - TILE_SIZE);
                }

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

            }
            else
            {
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

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.name == "Player" && (player.position - myRigidbody.position).magnitude < 1.5) {
            canJoin = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.name == "Player") {
            canJoin = false;
        }
    }
    //private void OnCollisionEnter2D(Collision2D collision) {
    //    if (collision.gameObject.name == "Player" && (player.position - myRigidbody.position).magnitude < 1.5) {
    //        canJoin = true;
    //    }
    //}

    //private void OnCollisionExit2D(Collision2D collision) {
    //    if (collision.gameObject.name == "Player") {
    //        canJoin = false;
    //    }
    //}
}
