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


    public void Start() {
        LevelHandler.instance = this;

        altarCounterGUI.text = string.Format("X{0}", numAltars);
        cellCounterGUI.text = string.Format("X{0}", numCells);
    }

    public void UpdateAltars(int altarsDeactivated, int cellsConsumed) {
        numAltars -= altarsDeactivated;
        numCells -= cellsConsumed;

        altarCounterGUI.text = string.Format("X{0}", numAltars);
        cellCounterGUI.text = string.Format("X{0}", numCells);

        if (numCells == 0) {
            //Win thing
            print("Level Win!");
        }
    }
}