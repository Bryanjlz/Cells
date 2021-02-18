using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    public Vector2 direction;

    public LayerMask worldLayer;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.name == "Player") {
            Collider2D player = collision.gameObject.GetComponent<Collider2D>();
            RaycastHit2D hit = Physics2D.BoxCast(player.bounds.center, player.bounds.size, 0, -direction, 0.2f, worldLayer);
            if (hit.collider != null && hit.collider.gameObject.GetComponent<ShieldCell>() != null) {
                return;
            }
            collision.gameObject.GetComponent<PlayerController>().Die();
        }
    }
}
