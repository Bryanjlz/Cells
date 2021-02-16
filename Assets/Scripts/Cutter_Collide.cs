using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutter_Collide : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController controller = other.GetComponent<PlayerController>();

        // Only give health to the controller which is Ruby (since other objects do not have RubyController)
        if (controller != null)
        {
            print(controller);
        }
    }
}
