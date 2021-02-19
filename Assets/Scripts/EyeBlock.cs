using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeBlock : MonoBehaviour
{   
    public Sprite enabledSprite;
    public Sprite disabledSprite;

    public int enabledAlpha = 255;
    public int disabledAlpha = 100;

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
            block.spriteRenderer.color = new Color(block.spriteRenderer.color.r, block.spriteRenderer.color.g, block.spriteRenderer.color.b, block.enabledAlpha/255.0f);
        }
    }

    public static void Disable() {
        //Set layer to 'Ignore Raycast'
        foreach (EyeBlock block in blocks) {
            block.gameObject.layer = 2;
            block.spriteRenderer.sprite = block.disabledSprite;
            block.spriteRenderer.color = new Color(block.spriteRenderer.color.r, block.spriteRenderer.color.g, block.spriteRenderer.color.b, block.disabledAlpha/255.0f);
        }
    }
}