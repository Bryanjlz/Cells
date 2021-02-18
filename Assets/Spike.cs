using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.name == "Player") {
            collision.gameObject.GetComponent<PlayerController>().Die();
        } else if (collision.gameObject.GetComponent<ShieldCell>() != null)
        {
            // Protected
        } else if (collision.gameObject.GetComponent<Cell>() != null) {
            collision.gameObject.GetComponent<Cell>().GetComponentInParent<PlayerController>().Die();
        }
    }
}
