using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    public Vector2 direction;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.name == "Player") {
            //Check if there is a shield cell in direction laser is coming from
            Collider2D player = collision.gameObject.GetComponent<Collider2D>();
            RaycastHit2D hit = Physics2D.BoxCast(player.bounds.center, player.bounds.size, 0, -direction, 0.2f, 1 << 10);
            if (hit.collider != null) {
                return;
            }
            collision.gameObject.GetComponent<PlayerController>().Die();
        }
    }
}
