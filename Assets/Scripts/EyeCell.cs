using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeCell : Cell
{
    public override void OnJoin() {
        EyeManager.Enable();
    }

    public void OnDestroy() {
        //Check to see if there are no more eye blocks remaining
        /*
        foreach (BoxCollider2D collider in player.gameObject.GetComponents<BoxCollider2D>()) {
            if (collider.GetComponent<EyeCell>() != null) {
                return;
            }
        }
        */
        //Disable the eye blocks if so
        EyeManager.Disable();
    }
}
