using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHandler: MonoBehaviour 
{
    [Tooltip("The number of altars in this level.")]
    public int numAltars;
    [Tooltip("The number of cells in this level.")]
    public int numCells;

    public static LevelHandler instance;

    public void Start() {
        LevelHandler.instance = this;
    }

    public void UpdateAltars(int altarsDeactivated, int cellsConsumed) {
        numAltars -= altarsDeactivated;
        numCells -= cellsConsumed;

        if (numCells == 0) {
            //Win thing
            print("Level Win!");
        }
    }
}