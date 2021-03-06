﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutter_Collide : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D other)
    {
        PlayerController controller = other.GetComponent<PlayerController>();
        // Only give health to the controller which is Ruby (since other objects do not have RubyController)
        if (controller != null)
        {
            if (Input.GetKey(KeyCode.E) && controller.colliders.Count > 1)
            {
                int cellsSacrificed = 0;
                for (int i = 0; i < controller.transform.childCount; i++)
                {
                    if (controller.transform.GetChild(i).GetComponent<Cell>() != null) {
                        controller.transform.GetChild(i).GetComponent<Cell>().death();
                        Destroy(controller.transform.GetChild(i).gameObject);
                        controller.colliders.RemoveAt(1);
                        cellsSacrificed++;
                    }

                    if (i == 0)
                    {
                        // Audio
                        FindObjectOfType<AudioManager>().Play("Altar");
                    }
                }

                LevelHandler.instance.UpdateAltars(1, cellsSacrificed);

                Destroy(transform.GetChild(0).gameObject);
                GetComponent<PolygonCollider2D>().enabled = false;
            }
        }
    }
}
