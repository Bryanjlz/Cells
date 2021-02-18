using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeBlock : MonoBehaviour
{   
    public Sprite enabledSprite;
    public Sprite disabledSprite;

    [HideInInspector]
    public SpriteRenderer spriteRenderer;

    public void Awake() {
        EyeManager.Add(this);

        spriteRenderer = GetComponent<SpriteRenderer>();
        gameObject.layer = 2;
        spriteRenderer.sprite = disabledSprite;
    }

    public void OnDestroy() {
        EyeManager.Remove(this);
    }
} 

public static class EyeManager {

    static List<EyeBlock> blocks = new List<EyeBlock>();

    public static void Add(EyeBlock block) {
        blocks.Add(block);
    }

    public static void Remove(EyeBlock block) {
        blocks.Remove(block);
    }

    public static void Enable() {
        //Set layer to 'world'
        foreach (EyeBlock block in blocks) {
            block.gameObject.layer = 9;
            block.spriteRenderer.sprite = block.enabledSprite;
        }
    }

    public static void Disable() {
        //Set layer to 'Ignore Raycast'
        foreach (EyeBlock block in blocks) {
            block.gameObject.layer = 2;
            block.spriteRenderer.sprite = block.disabledSprite;
        }
    }
}