using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelHandler: MonoBehaviour 
{
    [Tooltip("The number of altars in this level.")]
    public int numAltars;
    [Tooltip("The number of cells in this level.")]
    public int numCells;

    public TextMeshProUGUI cellCounterGUI;
    public TextMeshProUGUI altarCounterGUI;

    public static LevelHandler instance;

    GameObject largeText;
    GameObject pauseScreen;

    GameObject lasers;

    public void Start() {
        instance = this;

        altarCounterGUI.text = string.Format("X{0}", numAltars);
        cellCounterGUI.text = string.Format("X{0}", numCells);

        largeText = GameObject.Find("Large Text");
        pauseScreen = largeText.transform.parent.gameObject;
        pauseScreen.SetActive(false);

        lasers = GameObject.Find("Lasers");
    }

    public void UpdateAltars(int altarsDeactivated, int cellsConsumed) {
        numAltars -= altarsDeactivated;
        numCells -= cellsConsumed;

        altarCounterGUI.text = string.Format("X{0}", numAltars);
        cellCounterGUI.text = string.Format("X{0}", numCells);

        if (numCells == 0) {
            //Win thing

            //Edit pause text to say complete
            Rect currentRect = largeText.GetComponent<RectTransform>().rect;
            largeText.GetComponent<RectTransform>().sizeDelta = new Vector2(890f, currentRect.height);
            largeText.GetComponentInChildren<TMP_Text>().text = "COMPLETE";

            //Remove resume button
            pauseScreen.transform.GetChild(3).gameObject.SetActive(false);

            //Add next level button
            pauseScreen.transform.GetChild(4).gameObject.SetActive(true);

            //Turn off all lasers
            foreach (Laser laser in lasers.GetComponentsInChildren<Laser>()) {
                laser.isOn = false;
            }

            //Delay stuff
            StartCoroutine(MyCoroutine());
        }
    }

    IEnumerator MyCoroutine() {
        yield return new WaitForSeconds(1);
        //Stop things and show canvas
        Pause.isPaused = true;
        Time.timeScale = 0;
        pauseScreen.gameObject.SetActive(true);
    }
}