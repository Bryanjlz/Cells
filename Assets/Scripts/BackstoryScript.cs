using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BackstoryScript : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    [SerializeField] SpriteRenderer image;
    [SerializeField] GameObject pause;

    GameObject largeText;
    GameObject pauseScreen;

    List<string> dialogue;
    int currentDialogue;
    bool isFadeOut;
    bool isFadeIn;

    // Start is called before the first frame update
    void Start() {
        dialogue = new List<string>();
        currentDialogue = 0;
        dialogue.Add("Once upon a time, there was a god capable of mystical feats. Loved by all, they would share their power with the creatures below, however, not all were pleased by this.".ToUpperInvariant());
        dialogue.Add("Jealous of the god, heretics and non-believers banded together to bring ruin to them. A powerful spell divided the god into cells until only their soul remained.".ToUpperInvariant());
        dialogue.Add("Clinging onto hope, the god created you, the one cell to bring them back. You have been tasked to gather their cells once again.".ToUpperInvariant());
        dialogue.Add("They wish you good luck as you begin your adventure...".ToUpperInvariant());


        text.color = new Color(1, 1, 1, 0f);
        text.text = dialogue[currentDialogue];

        isFadeIn = true;
        isFadeOut = false;

        largeText = GameObject.Find("Large Text");
        pauseScreen = largeText.transform.parent.gameObject;
        pauseScreen.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (!Pause.isPaused && Input.anyKey && !isFadeOut && !isFadeIn) {
            NextDialogue();
        }

        if (isFadeOut) {
            if (currentDialogue == dialogue.Count) {
                image.color = new Color(1, 1, 1, image.color.a - 4f / 255f);
            }
            text.color = new Color(1, 1, 1, text.color.a - 4f / 255f);
            if (text.color.a <= 0) {

                if (currentDialogue == dialogue.Count) {
                    //Edit pause text to say complete
                    Rect currentRect = largeText.GetComponent<RectTransform>().rect;
                    largeText.GetComponent<RectTransform>().sizeDelta = new Vector2(890f, currentRect.height);
                    largeText.GetComponentInChildren<TMP_Text>().text = "COMPLETE";

                    //Remove resume button
                    pauseScreen.transform.GetChild(3).gameObject.SetActive(false);

                    //Add next level button
                    pauseScreen.transform.GetChild(4).gameObject.SetActive(true);

                    //Stop things and show canvas
                    Pause.isPaused = true;

                    //Win thing
                    FindObjectOfType<AudioManager>().Play("Win");
                    pauseScreen.gameObject.SetActive(true);
                    isFadeOut = false;
                } else {
                    isFadeOut = false;
                    isFadeIn = true;
                    text.text = dialogue[currentDialogue];

                    if (currentDialogue == 2) {
                        image.gameObject.SetActive(true);
                        image.color = new Color(1, 1, 1, 0);
                    }
                }
            }
        }
        if (isFadeIn) {
            image.color = new Color(1, 1, 1, image.color.a + 4f / 255f);
            text.color = new Color(1, 1, 1, text.color.a + 4f / 255f);
            if (text.color.a >= 1) {
                isFadeIn = false;
                image.color = new Color(1, 1, 1, 1);
                text.color = new Color(1, 1, 1, 1);
            }
        }
    }

    public void NextDialogue() {
        isFadeOut = true;
        currentDialogue++;
    }
}
